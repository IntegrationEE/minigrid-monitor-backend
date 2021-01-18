#region used namespaces
using NLog;
using System;
using System.IO;
using AutoMapper;
using System.Net;
using IdentityServer4;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Monitor.Domain;
using Monitor.Common;
using Monitor.HostedServices;
using Monitor.WebApi.Filters;
using Monitor.Infrastructure;
using Monitor.IdentityServer;
#endregion

namespace Monitor.WebApi
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly string _version;
        private readonly string _authority;
        private readonly bool _requireHttps;
        private readonly string _identityServerUrl;
        private readonly string _connectionString;
        private readonly string[] _allowedOrigins;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _version = configuration[Constants.CONFIG_VERSION];
            _authority = configuration[Constants.CONFIG_AUTHORITY];
            _connectionString = configuration[Constants.CONFIG_CONNECTION_STRING];
            _identityServerUrl = configuration[Constants.CONFIG_IDENTITY_SERVER];
            _requireHttps = bool.Parse(configuration[Constants.CONFIG_REQUIRE_HTTPS]);
            _allowedOrigins = configuration.GetSection(Constants.CONFIG_ALLOWED_ORIGINS).Get<string[]>();
        }
        /// <summary>
        /// Configure Service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MinigridDbContext>(options => options.UseNpgsql(_connectionString, o => o.UseNetTopologySuite()));

            services.AddSingleton(_configuration);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            services.AddServicesAndRepositories();

            #region AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GlobalProfile>();
            });

            services.AddSingleton(config.CreateMapper());
            #endregion

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = _authority;
                    options.RequireHttpsMetadata = _requireHttps;
                    options.Audience = IdentityConfig.AUDIENCE;
                })
                .AddOpenIdConnect(o =>
                {
                    o.ClientId = IdentityConfig.SWAGGER_CLIENT;
                    o.ClientSecret = IdentityConfig.AUDIENCE;
                    o.Authority = string.Format(IdentityServerConstants.AccessTokenAudience, _identityServerUrl);
                    o.ResponseType = OpenIdConnectResponseType.Token;
                    o.RequireHttpsMetadata = _requireHttps;
                    o.GetClaimsFromUserInfoEndpoint = true;

                    o.Scope.Add(IdentityServerConstants.StandardScopes.OfflineAccess);
                });

            services.AddAuthorization();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddCustomUserStore();

            services.AddControllers()
                .AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{_version}", new OpenApiInfo { Version = $"v{_version}", Title = "Mini-Grid Monitor API", });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.SchemaFilter<SwaggerSchemaFilter>();
                c.SchemaFilter<EnumSchemaFilter>();

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
        }
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="dbContext"></param>
        public void Configure(IApplicationBuilder app, MinigridDbContext dbContext)
        {
            try
            {
                dbContext.Database.Migrate();
                DatabaseInitializer.Seed(dbContext);

                var integrationService = app.ApplicationServices.GetRequiredService<IIntegrationHostedService>();
                integrationService.Start().Wait();
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
            }

            if (_requireHttps)
            {
                app.UseHttpsRedirection();
            }

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ExceptionMiddleware().Invoke
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.AccessControlAllowOrigin] = "*";
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={TimeSpan.FromDays(1).TotalSeconds}";
                },
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Assets/Icons")),
                RequestPath = "/Icons",
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{_version}/swagger.json", $"Mini-Grid Monitor API v{_version}");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseIdentityServer();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins(_allowedOrigins)
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
