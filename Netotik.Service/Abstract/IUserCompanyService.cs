using DnmaGroups.Common;
using DnmaGroups.Domain.Entity;
using DnmaGroups.Model.Identity.UserCompany;
using DnmaGroups.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnmaGroups.Service.Abstract
{
    public interface IUserCompanyService : IBaseService<UserCompany>
    {
        /// <summary>
        /// Inserts the data into the table
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        Task<OperationStatus> AddAsync(UserCompany entity);

        Task<IList<UserCompany>> GetbyIdsAsync(int[] ids);
        Task<OperationStatus> UpdateAsync(UserCompany entity);

        Task<OperationStatus> ChangePasswordAsync(int companyId, string password);

        Task<bool> ExistsByMobileNumberAsync(string num, int? id,int userid);
        Task<bool> ExistsByEmailAsync(string email, int? id, int userid);
        bool ExistsByMobileNumber(string num, int? id,int companyid);
        bool ExistsByEmail(string email, int? id, int companyid);

        IQueryable<TableCompanyModel> GetDataTableCompanyAccounts(string search,int companyid);

        ProfileModel GetProfile(int id);
         OperationStatus UpdateProfile(ProfileModel model, int companyId);
        OperationStatus UpdateProfileEdit(ProfileEditModel model, int companyId);

        OperationStatus ChangePassword(ChangePasswordModel model, int companyId);
        VerifyUserStatus VerifyUserByEmail(string email, string password, ref UserCompany user,int companyid);
        Task<bool> IsCompanyNameExist(string companyName, int? id,int userid);
        ProfileEditModel GetProfileEdit(int id);
        bool CheckMeliCode(string PersonCode, int? id);
    }
}
