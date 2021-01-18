using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Monitor.WebApi.Filters
{
    /// <summary>
    /// Swagger schema filter
    /// </summary>
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            var excludedProperties = context.Type.GetProperties()
                .Where(t => t.GetCustomAttribute<JsonIgnoreAttribute>() != null);

            foreach (PropertyInfo excludedProperty in excludedProperties)
            {
                if (schema.Properties.ContainsKey(excludedProperty.Name))
                {
                    schema.Properties.Remove(excludedProperty.Name);
                }
            }
        }
    }
}
