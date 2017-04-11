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
using PersianDate;
using DnmaGroups.Common;
using DnmaGroups.Resource;
using DnmaGroups.Model.Identity.UserCompany;

namespace DnmaGroups.Service.Implement
{
    public class UserCompanyService : BaseService<UserCompany>, IUserCompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserCompanyService(IUnitOfWork unit)
            : base(unit)
        {
            _unitOfWork = unit;
        }

        public async Task<IList<UserCompany>> GetbyIdsAsync(int[] ids)
        {
            var comapnie_users = await dbSet.ToListAsync();
            var list = new List<UserCompany>();
            foreach (var item in ids)
            {
                var user = comapnie_users.FirstOrDefault(x => x.Id == item);
                if (user != null)
                    list.Add(user);
            }
            return list;
        }

        public override OperationStatus Add(UserCompany companyuser)
        {
            if (ExistsByEmail(companyuser.User.Email, null,companyuser.UserResellerId))
                return new OperationStatus
                {
                    Status = false,
                    Title = Messages.MissionFail,
                    Message = string.Format(Messages.ExistError, Captions.Email),
                    Color = MessageColor.Warning
                };

            dbSet.Add(companyuser);
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.AddSuccess,
                Color = MessageColor.Success
            };
        }

        public bool CheckMeliCode(string PersonCode, int? id)
        {
            if (PersonCode.Length == 10)
            {
                if (PersonCode == "1111111111" ||
                    PersonCode == "0000000000" ||
                    PersonCode == "2222222222" ||
                    PersonCode == "3333333333" ||
                    PersonCode == "4444444444" ||
                    PersonCode == "5555555555" ||
                    PersonCode == "6666666666" ||
                    PersonCode == "7777777777" ||
                    PersonCode == "8888888888" ||
                    PersonCode == "9999999999" ||
                    PersonCode == "0123456789"
                    )
                {
                    //Response.Write("كد ملي صحيح نمي باشد");          
                    return false;
                }

                int c = Convert.ToInt16(PersonCode.Substring(9, 1));

                int n = Convert.ToInt16(PersonCode.Substring(0, 1)) * 10 +
                     Convert.ToInt16(PersonCode.Substring(1, 1)) * 9 +
                     Convert.ToInt16(PersonCode.Substring(2, 1)) * 8 +
                     Convert.ToInt16(PersonCode.Substring(3, 1)) * 7 +
                     Convert.ToInt16(PersonCode.Substring(4, 1)) * 6 +
                     Convert.ToInt16(PersonCode.Substring(5, 1)) * 5 +
                     Convert.ToInt16(PersonCode.Substring(6, 1)) * 4 +
                     Convert.ToInt16(PersonCode.Substring(7, 1)) * 3 +
                     Convert.ToInt16(PersonCode.Substring(8, 1)) * 2;
                int r = n - (n / 11) * 11;
                if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r))
                {
                    //Response.Write(" کد ملی صحیح است");                
                    return true;
                }
                else
                {
                    //Response.Write("كد ملي صحيح نمي باشد");         
                    return false;
                }
            }
            else
            {
                //Response.Write("طول کد ملی وارد شده باید 10 کاراکتر باشد");           
                return false;
            }
        }

        public async Task<OperationStatus> AddAsync(Company companyuser)
        {
            if (await ExistsByEmailAsync(companyuser.Email, null,companyuser.Reseller_Id))
                return new OperationStatus
                {
                    Status = false,
                    Title = Messages.MissionFail,
                    Message = string.Format(Messages.ExistError, Captions.Email),
                    Color = MessageColor.Warning
                };

            dbSet.Add(companyuser);
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.AddSuccess,
                Color = MessageColor.Success
            };
        }


        public override OperationStatus Update(Company companyuser)
        {
            var selectedUser = SingleOrDefault(companyuser.Id);
            if (selectedUser.Email != companyuser.Email)
            {
                if (ExistsByEmail(companyuser.Email, null,companyuser.Reseller_Id))
                    return new OperationStatus
                    {
                        Status = false,
                        Title = DnmaGroups.Resource.Messages.MissionFail,
                        Message = string.Format(Messages.ExistError, Captions.Email),
                        Color = MessageColor.Warning
                    };
            }
            selectedUser.Name = companyuser.Name;
            selectedUser.Address = companyuser.Address;
            selectedUser.MobileNumber = companyuser.MobileNumber;
            selectedUser.Email = companyuser.Email;
            selectedUser.CompanyName = companyuser.CompanyName;
            selectedUser.PersonalCode = companyuser.PersonalCode;
            selectedUser.PostalCode = companyuser.PostalCode;
            selectedUser.R_Host = companyuser.R_Host;
            selectedUser.R_Password = companyuser.R_Password;
            selectedUser.R_Port = companyuser.R_Port;
            selectedUser.R_User = companyuser.R_User;
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.UpdateSuccess,
                Color = MessageColor.Success
            };
        }


        public async Task<OperationStatus> UpdateAsync(Company companyuser)
        {
            var selectedUser = SingleOrDefault(companyuser.Id);
            if (selectedUser.Email != companyuser.Email)
            {
                if (await ExistsByEmailAsync(companyuser.Email, null,companyuser.Reseller_Id))
                    return new OperationStatus
                    {
                        Status = false,
                        Title = Messages.MissionFail,
                        Message = string.Format(Messages.ExistError, Captions.Email),
                        Color = MessageColor.Warning
                    };
            }

            selectedUser.Name = companyuser.Name;
            selectedUser.Address = companyuser.Address;
            selectedUser.MobileNumber = companyuser.MobileNumber;
            selectedUser.Email = companyuser.Email;
            selectedUser.CompanyName = companyuser.CompanyName;
            selectedUser.PersonalCode = companyuser.PersonalCode;
            selectedUser.PostalCode = companyuser.PostalCode;
            selectedUser.R_Host = companyuser.R_Host;
            selectedUser.R_Password = companyuser.R_Password;
            selectedUser.R_Port = companyuser.R_Port;
            selectedUser.R_User = companyuser.R_User;
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.UpdateSuccess,
                Color = MessageColor.Success
            };
        }

        public async Task<OperationStatus> ChangePasswordAsync(int companyuserId, string password)
        {
            var selectedUser = await dbSet.SingleAsync(x => x.Id == companyuserId);
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

        private static VerifyUserStatus Verify(Company companyuser, string password)
        {
            var result = VerifyUserStatus.VerifiedFaild;

            bool verifiedPassword = Encryption.VerifyPassword(password, companyuser.Password);

            if (verifiedPassword)
            {
                if (companyuser.IsActive) result = VerifyUserStatus.VerifiedSuccessfully;
                else result = VerifyUserStatus.UserIsbaned;
            }

            return result;
        }

        public VerifyUserStatus VerifyUserByEmail(string email, string password, ref Company company,int companyid)
        {
            var selectedUser = dbSet.SingleOrDefault(x => x.Email == email && x.Reseller_Id == companyid);
            var result = VerifyUserStatus.VerifiedFaild;
            if (selectedUser != null)
            {
                result = Verify(selectedUser, password);
                if (result == VerifyUserStatus.VerifiedSuccessfully)
                {
                    company = selectedUser;
                }
            }
            return result;
        }
        #endregion

        public async Task<bool> ExistsByMobileNumberAsync(string mobileNumber, int? id,int userid)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(user => user.MobileNumber.Equals(mobileNumber) && user.Id != id.Value && user.Reseller_Id == userid);
            return await dbSet.AnyAsync(user => user.MobileNumber.Equals(mobileNumber) && user.Reseller_Id == userid);
        }

        public async Task<bool> ExistsByEmailAsync(string email, int? id,int userid)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(user => user.User.Email.Equals(email) && user.Id != id.Value && user.UserResellerId==userid);
            return await dbSet.AnyAsync(user => user.User.Email.Equals(email) && user.UserResellerId== userid);
        }



        public bool ExistsByMobileNumber(string num, int? id, long companyid)
        {
            if (id.HasValue)
                return dbSet.Any(user => user.User.PhoneNumber.Equals(num) && user.Id != id.Value && user.UserResellerId== companyid);
            return dbSet.Any(user => user.User.PhoneNumber.Equals(num) && user.UserResellerId == companyid);
        }

        public bool ExistsByEmail(string email, int? id,long companyid)
        {
            if (id.HasValue)
                return dbSet.Any(user => user.User.Email.Equals(email) && user.Id != id.Value && user.UserResellerId == companyid);
            return dbSet.Any(user => user.User.Email.Equals(email) && user.UserResellerId == companyid);
        }
        
        public IQueryable<TableCompanyModel> GetDataTableCompanyAccounts(string search,int companyid)
        {
            IQueryable<TableCompanyModel> selectedUsers = dbSet
                                            .OrderByDescending(x => x.User.CreateDate)
                                            .Where(x=> x.UserResellerId == companyid)
                                            .Select(x => new TableCompanyModel
                                            {
                                                Id = x.Id,
                                                IsActive = x.IsActive,
                                                Name = x.Name,
                                                Username = x.UserName,
                                                Email = x.Email
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selectedUsers = selectedUsers.Where(x => x.Name.Contains(search)).AsQueryable();

            return selectedUsers;
        }



        public ProfileModel GetProfile(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id && x.User.IsActive);

            if (entity != null)
            {
                return new ProfileModel
                {
                    R_Host = entity.R_Host,
                    R_Password = entity.R_Password,
                    R_Port = entity.R_Port,
                    R_User = entity.R_User,
                    Username = entity.R_User,
                    Id = entity.Id,
                    Email = entity.Email,
                    Address = entity.Address,
                    CompanyName = entity.CompanyName,
                    Name = entity.Name,
                    PersonCode = entity.PersonalCode,
                    PostalCode = entity.PostalCode,
                    Userman_Customer = entity.Userman_Customer,
                    //FirstName = entity.FirstName,
                    //LastName = entity.LastName,
                    MobileNumber = entity.MobileNumber,
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
                var entity = dbSet.FirstOrDefault(x => x.Id == userId && x.IsActive );

                if (entity.Email != model.Email)
                {
                    if (ExistsByEmail(model.Email, null,model.Reseller_Id))
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

                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.PersonalCode = model.PersonCode;
                    entity.PostalCode = model.PostalCode;
                    entity.R_Host = model.R_Host;
                    entity.R_Password = model.R_Password;
                    entity.R_Port = model.R_Port;
                    entity.R_User = model.R_User;
                    entity.MobileNumber = model.MobileNumber;

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
        public OperationStatus UpdateProfileEdit(ProfileEditModel model, int userId)
        {
            try
            {
                var entity = dbSet.FirstOrDefault(x => x.Id == userId && x.IsActive);

                if (entity.Email != model.Email)
                {
                    if (ExistsByEmail(model.Email, null, model.Reseller_Id))
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

                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.PersonalCode = model.PersonCode;
                    entity.PostalCode = model.PostalCode;
                    entity.R_Host = model.R_Host;
                    entity.R_Password = model.R_Password;
                    entity.R_Port = model.R_Port;
                    entity.R_User = model.R_User;
                    entity.MobileNumber = model.MobileNumber;
                    entity.Userman_Customer = model.Userman_Customer;
                    entity.Email = model.Email;
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
        public async Task<bool> IsCompanyNameExist(string CompanyName, int? id,int userid)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(user => user.CompanyName.ToLower().Equals(CompanyName.ToLower()) && user.Id != id.Value );
            return await dbSet.AnyAsync(user => user.CompanyName.ToLower().Equals(CompanyName.ToLower()) );
        }

        public OperationStatus ChangePassword(ChangePasswordModel model, int userId)
        {
            try
            {
                var entity = dbSet.FirstOrDefault(x => x.Id == userId && x.IsActive);

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

        public ProfileEditModel GetProfileEdit(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id && x.IsActive);

            if (entity != null)
            {
                return new ProfileEditModel
                {
                    R_Host = entity.R_Host,
                    R_Password = entity.R_Password,
                    R_Port = entity.R_Port,
                    R_User = entity.R_User,
                    Username = entity.R_User,
                    Id = entity.Id,
                    Email = entity.Email,
                    Address = entity.Address,
                    CompanyName = entity.CompanyName,
                    Name = entity.Name,
                    PersonCode = entity.PersonalCode,
                    PostalCode = entity.PostalCode,
                    Userman_Customer = entity.Userman_Customer,
                    //FirstName = entity.FirstName,
                    //LastName = entity.LastName,
                    MobileNumber = entity.MobileNumber,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
