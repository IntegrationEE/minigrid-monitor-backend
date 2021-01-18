using NLog;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Monitor.Common;

namespace Monitor.WebApi
{
    /// <summary>
    /// Exception Middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex == null) return;

            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(ex);

            context.Response.ContentType = "application/json";
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            ErrorResponse error = null;

            if (ex is DbUpdateException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                if (ex.InnerException?.Message.StartsWith("Cannot insert duplicate key row in object") == true)
                {
                    var messages = ex.InnerException.Message.Split("'");
                    var entityName = messages[1].Split("dbo.")[1];
                    var propertyDescription = messages[3].Split("_");
                    var value = messages[^1].Split("(")[1].Split(")")[0];

                    error = new ErrorResponse
                    {
                        Message = $"Exists '{(entityName.EndsWith("s") ? entityName.Remove(entityName.Length - 1) : entityName)}' with {propertyDescription[^1]} '{value}'."
                    };
                }
                else if (ex.InnerException?.Message.StartsWith("The DELETE statement conflicted") == true)
                {
                    var messages = ex.InnerException.Message.Split("\"");
                    var entityName = messages[1].Split("_")[2];
                    var linkedEntity = messages[5].Split("dbo.")[1];

                    error = new ErrorResponse
                    {
                        Message = $"This '{(entityName.EndsWith("s") ? entityName.Remove(entityName.Length - 1) : entityName)}' entity is already referenced by '{linkedEntity}' entity."
                    };
                }
            }
            else if (ex is CustomException)
            {
                context.Response.StatusCode = (ex as CustomException).StatusCode ?? StatusCodes.Status400BadRequest;

                error = new ErrorResponse
                {
                    Message = ex.Message
                };
            }

            if (error == null)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                error = new ErrorResponse
                {
                    Message = $"{ex.Message} {(!string.IsNullOrWhiteSpace(ex.InnerException?.Message) ? $"Inner exception: {ex.InnerException.Message}" : string.Empty)}"
                };
            }

            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
    /// <summary>
    /// Error response
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
