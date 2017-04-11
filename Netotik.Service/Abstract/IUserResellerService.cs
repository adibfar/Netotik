using DnmaGroups.Common;
using DnmaGroups.Domain.Entity;
using DnmaGroups.Model.Identity.UserReseller;
using DnmaGroups.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnmaGroups.Service.Abstract
{
    public interface IUserResellerService : IBaseService<UserReseller>
    {
        /// <summary>
        /// Inserts the data into the table
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        Task<OperationStatus> AddAsync(UserReseller entity);

        Task<IList<UserReseller>> GetbyIdsAsync(int[] ids);
        Task<OperationStatus> UpdateAsync(UserReseller entity);

        Task<OperationStatus> ChangePasswordAsync(int companyId, string password);

        Task<bool> ExistsByMobileNumberAsync(string num, int? id);
        Task<bool> ExistsByEmailAsync(string email, int? id);
        bool ExistsByMobileNumber(string num, int? id);
        bool ExistsByEmail(string email, int? id);

        IQueryable<TableResellerModel> GetDataTableResellerAccounts(string search);
        void ActiveBaneReseller(int id);

        ProfileModel GetProfile(int id);
        OperationStatus UpdateProfile(ProfileModel model, int companyId);

        OperationStatus ChangePassword(ChangePasswordModel model, int companyId);
        
        VerifyUserStatus VerifyUserByEmail(string email, string password, ref UserReseller company);
        Task<bool> IsCompanyNameExist(string companyName, int? id);
        bool CheckMeliCode(string PersonCode, int? id);
    }
}
