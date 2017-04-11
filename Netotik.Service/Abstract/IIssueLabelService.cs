﻿using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IIssueLabelService : IBaseService<IssueLabel>
    {
        Task<IssueLabel> SingleOrDefaultAsync(int primaryKey);

        Task<bool> IsExistByName(string name,int? id);

        IList<IssueLabel> GetbyIds(int[] ids);
    }
}
