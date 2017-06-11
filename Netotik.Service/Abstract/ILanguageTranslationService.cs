using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ILanguageTranslationService : IBaseService<LanguageTranslation>
    {
        string GetLocalized(string entityId, int languageId, string objectName, string PropertyName);
    }
}
