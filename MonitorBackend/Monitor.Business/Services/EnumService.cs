using System;
using System.Linq;
using System.Collections.Generic;
using Monitor.Domain.ViewModels;
using Monitor.Common.Extensions;

namespace Monitor.Business.Services
{
    public class EnumService : IEnumService
    {
        public List<EnumViewModel<T>> GetList<T>()
            where T : struct
        {
            var response = new List<EnumViewModel<T>>();

            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();

            foreach (var enumValue in enumValues)
            {
                response.Add(new EnumViewModel<T>()
                {
                    Value = enumValue,
                    Label = enumValue.GetDescription()
                });
            }

            return response
                .OrderBy(z => z.Label)
                .ToList();
        }
    }
}
