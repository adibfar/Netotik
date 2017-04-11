using DnmaGroups.Common;
using DnmaGroups.Domain.Entity;
using DnmaGroups.Model.Identity.UserAdmin;
using DnmaGroups.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnmaGroups.Service.Abstract
{
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// Inserts the data into the table
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        Task<OperationStatus> AddAsync(User entity);

        Task<IList<User>> GetbyIdsAsync(int[] ids);
        Task<OperationStatus> UpdateAsync(User entity);

        Task<OperationStatus> ChangePasswordAsync(int userId, string password);

        Task<bool> ExistsByMobileNumberAsync(string num, int? id);
        Task<bool> ExistsByEmailAsync(string email, int? id);
        bool ExistsByMobileNumber(string num, int? id);
        bool ExistsByEmail(string email, int? id);

        IQueryable<TableUserModel> GetDataTableUserAccounts(string search);
        void ActiveBaneUser(int id);
        void Remove(int id);



        ProfileModel GetProfile(int id);
        OperationStatus UpdateProfile(ProfileModel model, int userId);

        OperationStatus ChangePassword(ChangePasswordModel model, int userId);
        VerifyUserStatus VerifyUserByEmail(string email, string password, ref User user);
    }
}
