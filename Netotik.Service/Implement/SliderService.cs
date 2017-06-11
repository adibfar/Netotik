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
    public class SliderService : BaseService<Slider>, ISliderService
    {
        public SliderService(IUnitOfWork unit)
            : base(unit)
        {

        }


        public IList<Slider> GetAll(int languageId)
        {
            return dbSet
                .Where(x => x.LanguageId == languageId && x.IsActive)
                .OrderBy(x => x.Order)
                .Include(x => x.Picture)
                .ToList();
        }
    }
}
