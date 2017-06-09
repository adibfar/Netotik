using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PersianDate;
using Netotik.Common;

namespace Netotik.Services.Implement
{
    public class LanguageTranslationService : BaseService<LanguageTranslation>, ILanguageTranslationService
    {
        public LanguageTranslationService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public string GetLocalized(string entityId, int languageId, string objectName, string PropertyName)
        {
            var data = dbSet.AsNoTracking().Where(x => x.LanguageId == languageId
              && x.EntityId == entityId
              && x.ObjectName == objectName
              && x.PropertyName == PropertyName).FirstOrDefault();

            return data == null ? "" : data.Value;
        }





    }
}
