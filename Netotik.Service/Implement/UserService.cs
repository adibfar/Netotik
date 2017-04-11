using DnmaGroups.Common.Security;
using DnmaGroups.DataAccess;
using DnmaGroups.Domain.Entity;
using DnmaGroups.Service.Abstract;
using DnmaGroups.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DnmaGroups.Common;
using DnmaGroups.Resource;
using DnmaGroups.Model.Identity.UserAdmin;

namespace DnmaGroups.Service.Implement
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unit)
            : base(unit)
        {
            _unitOfWork = unit;
        }



        public async Task<IList<User>> GetbyIdsAsync(int[] ids)
        {
            var users = await dbSet.ToListAsync();
            IList<User> list = new List<User>();
            foreach (var item in ids)
            {
                var user = users.FirstOrDefault(x => x.Id == item);
                if (user != null)
                    list.Add(user);
            }
            return list;
        }

        public override OperationStatus Add(User user)
        {
            if (ExistsByEmail(user.Email, null))
                return new OperationStatus
                {
                    Status = false,
                    Title = Messages.MissionFail,
                    Message = string.Format(Messages.ExistError,Captions.Email),
                    Color = MessageColor.Warning
                };
            
            dbSet.Add(user);
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.AddSuccess,
                Color = MessageColor.Success
            };
        }
        public async Task<OperationStatus> AddAsync(User user)
        {
            if (await ExistsByEmailAsync(user.Email, null))
                return new OperationStatus
                {
                    Status = false,
                    Title = Messages.MissionFail,
                    Message = string.Format(Messages.ExistError, Captions.Email),
                    Color = MessageColor.Warning
                };
            
            dbSet.Add(user);
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.AddSuccess,
                Color = MessageColor.Success
            };
        }


        public override OperationStatus Update(User user)
        {
            var selectedUser = SingleOrDefault(user.Id);
            if (selectedUser.Email != user.Email)
            {
                if (ExistsByEmail(user.Email, null))
                    return new OperationStatus
                    {
                        Status = false,
                        Title = DnmaGroups.Resource.Messages.MissionFail,
                        Message = string.Format(Messages.ExistError, Captions.Email),
                        Color = MessageColor.Warning
                    };
            }

            selectedUser.FirstName = user.FirstName;
            selectedUser.LastName = user.LastName;
            selectedUser.PhoneNumber = user.PhoneNumber;
            selectedUser.Email = user.Email;

            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.UpdateSuccess,
                Color = MessageColor.Success
            };
        }


        public async Task<OperationStatus> UpdateAsync(User user)
        {
            var selectedUser = SingleOrDefault(user.Id);
            if (selectedUser.Email != user.Email)
            {
                if (await ExistsByEmailAsync(user.Email, null))
                    return new OperationStatus
                    {
                        Status = false,
                        Title = Messages.MissionFail,
                        Message = string.Format(Messages.ExistError, Captions.Email),
                        Color = MessageColor.Warning
                    };
            }

            selectedUser.FirstName = user.FirstName;
            selectedUser.LastName = user.LastName;
            selectedUser.PhoneNumber = user.PhoneNumber;
            selectedUser.Email = user.Email;

            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.UpdateSuccess,
                Color = MessageColor.Success
            };
        }

        public async Task<OperationStatus> ChangePasswordAsync(int userId, string password)
        {
            var selectedUser = await dbSet.SingleAsync(x => x.Id == userId);
            selectedUser.Password = password;
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.ChangePasswordSuccess,
                Color = MessageColor.Success
            };

        }


        #region private members

        private static VerifyUserStatus Verify(User user, string password)
        {
            var result = VerifyUserStatus.VerifiedFaild;

            bool verifiedPassword = Encryption.VerifyPassword(password, user.Password);

            if (verifiedPassword)
            {
                if (user.IsActive) result = VerifyUserStatus.VerifiedSuccessfully;
                else result = VerifyUserStatus.UserIsbaned;
            }

            return result;
        }

        #endregion
        public VerifyUserStatus VerifyUserByEmail(string email, string password, ref User user)
        {
            User selectedUser = dbSet.SingleOrDefault(x => x.Email == email);
            var result = VerifyUserStatus.VerifiedFaild;
            if (selectedUser != null)
            {
                result = Verify(selectedUser, password);
                if (result == VerifyUserStatus.VerifiedSuccessfully)
                {
                    user = selectedUser;
                }
            }
            return result;
        }

        public async Task<bool> ExistsByMobileNumberAsync(string mobileNumber, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(user => user.PhoneNumber.Equals(mobileNumber) && user.Id != id.Value);
            return await dbSet.AnyAsync(user => user.PhoneNumber.Equals(mobileNumber));
        }

        public async Task<bool> ExistsByEmailAsync(string email, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(user => user.Email.Equals(email) && user.Id != id.Value);
            return await dbSet.AnyAsync(user => user.Email.Equals(email));
        }
        


        public bool ExistsByMobileNumber(string num, int? id)
        {
            if (id.HasValue)
                return dbSet.Any(user => user.PhoneNumber.Equals(num) && user.Id != id.Value);
            return dbSet.Any(user => user.PhoneNumber.Equals(num));
        }

        public bool ExistsByEmail(string email, int? id)
        {
            if (id.HasValue)
                return dbSet.Any(user => user.Email.Equals(email) && user.Id != id.Value);
            return dbSet.Any(user => user.Email.Equals(email));
        }


        public IQueryable<TableUserModel> GetDataTableUserAccounts(string search)
        {
            IQueryable<TableUserModel> selectedUsers = dbSet
                                            .Where(x => !x.IsDelete)
                                            .OrderByDescending(x => x.CreateDate)
                                            .Select(x => new TableUserModel
                                            {
                                                Id = x.Id,
                                                IsSystemAccount = x.IsSystemAccount,
                                                IsActive = x.IsActive,
                                                Email = x.Email,
                                                LastLogindate = x.LastLoginDate,
                                                Name = x.FirstName + " " + x.LastName,
                                                //Roles = x.Roles.Select(y => y.Name),
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selectedUsers = selectedUsers.Where(x => x.Name.Contains(search) || x.Email.Contains(search)).AsQueryable();

            return selectedUsers;
        }


        public void ActiveBaneUser(int id)
        {
            User selectedUser = dbSet.Find(id);
            if (selectedUser.IsActive)
                selectedUser.IsActive = false;
            else selectedUser.IsActive = true;
        }

        public void Remove(int id)
        {
            User selectedUser = dbSet.Find(id);
            if (!selectedUser.IsSystemAccount)
                selectedUser.IsDelete = true;
        }





        public ProfileModel GetProfile(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id && !x.IsDelete && x.IsActive);

            if (entity != null)
            {
                return new ProfileModel
                {
                    Email = entity.Email,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    MobileNumber = entity.PhoneNumber,
                };
            }
            else
            {
                return null;
            }
        }

        public OperationStatus UpdateProfile(ProfileModel model, int userId)
        {
            try
            {
                var entity = dbSet.FirstOrDefault(x => x.Id == userId && !x.IsDelete && x.IsActive);

                if (entity.Email != model.Email)
                {
                    if (ExistsByEmail(model.Email, null))
                        return new OperationStatus
                        {
                            Status = false,
                            Title = Messages.MissionFail,
                            Message = string.Format(Messages.ExistError, Captions.Email),
                            Color = MessageColor.Warning
                        };
                }


                if (entity != null)
                {
                    entity.FirstName = model.FirstName;
                    entity.LastName = model.LastName;
                    entity.Email = model.Email;
                    entity.PhoneNumber = model.MobileNumber;
                    _unitOfWork.SaveChanges();   
                    return new OperationStatus
                    {
                        Title = Messages.MissionSuccess,
                        Message = Messages.UpdateSuccess,
                        Status = true,
                        Color = MessageColor.Success
                    };
                }
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromException(Messages.MissionFail, Messages.UpdateError, ex);
            }

            return new OperationStatus
            {
                Title = Messages.MissionFail,
                Message = Messages.UpdateError,
                Status = false,
                Color = MessageColor.Danger
            };


        }


        public OperationStatus ChangePassword(ChangePasswordModel model, int userId)
        {
            try
            {
                var entity = dbSet.FirstOrDefault(x => x.Id == userId && !x.IsDelete && x.IsActive);

                if (entity != null)
                {
                    entity.Password = Encryption.EncryptingPassword(model.Password);
                    _unitOfWork.SaveChanges();
                    return new OperationStatus
                    {
                        Title = Messages.MissionSuccess,
                        Message = Messages.UpdateSuccess,
                        Status = true,
                        Color = MessageColor.Success
                    };
                }
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromException(Messages.MissionFail, Messages.UpdateError, ex);
            }

            return new OperationStatus
            {
                Title = Messages.MissionFail,
                Message = Messages.UpdateError,
                Status = false,
                Color = MessageColor.Danger
            };
        }

    }
}
