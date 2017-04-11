using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Common.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ISettingService 
    {
        GeneralSettingModel GetAll();
        void Update(GeneralSettingModel model);
    }
}
