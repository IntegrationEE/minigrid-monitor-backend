using System;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Monitor.WebApi.Filters
{
    /// <summary>
    /// Enum Schema Filter
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply
        /// Show Enums Value as String in Swagger
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Type = "string";
                schema.Enum.Clear();

                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(n => schema.Enum.Add(new OpenApiString(n)));
            }
        }
    }
}
