using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Netotik.AutoMapperProfiles.Extentions
{
    public class StringToDateConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(ResolutionContext context)
        {
            var stringDate = context.SourceValue;
            return PersianDateTime.Parse(stringDate.ToString()).ToDateTime();
        }

    }
}
