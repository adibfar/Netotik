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
using DnmaGroups.Model.Identity.UserReseller;

namespace DnmaGroups.Service.Implement
{
    public class ResellerService : BaseService<UserReseller>, IUserResellerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ResellerService(IUnitOfWork unit)
            : base(unit)
        {
            _unitOfWork = unit;
        }



        public async Task<IList<Reseller>> GetbyIdsAsync(int[] ids)
        {
            var comapnies = await dbSet.ToListAsync();
            var list = new List<Reseller>();
            foreach (var item in ids)
            {
                var user = comapnies.FirstOrDefault(x => x.Id == item);
                if (user != null)
                    list.Add(user);
            }
            return list;
        }

        public override OperationStatus Add(Reseller company)
        {
            if (ExistsByEmail(company.Email, null))
                return new OperationStatus
                {
                    Status = false,
                    Title = Messages.MissionFail,
                    Message = string.Format(Messages.ExistError, Captions.Email),
                    Color = MessageColor.Warning
                };

            dbSet.Add(company);
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.AddSuccess,
                Color = MessageColor.Success
            };
        }
        public async Task<OperationStatus> AddAsync(Reseller company)
        {
            if (await ExistsByEmailAsync(company.Email, null))
                return new OperationStatus
                {
                    Status = false,
                    Title = Messages.MissionFail,
                    Message = string.Format(Messages.ExistError, Captions.Email),
                    Color = MessageColor.Warning
                };

            dbSet.Add(company);
            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.AddSuccess,
                Color = MessageColor.Success
            };
        }


        public override OperationStatus Update(Reseller company)
        {
            var selectedUser = SingleOrDefault(company.Id);
            if (selectedUser.Email != company.Email)
            {
                if (ExistsByEmail(company.Email, null))
                    return new OperationStatus
                    {
                        Status = false,
                        Title = DnmaGroups.Resource.Messages.MissionFail,
                        Message = string.Format(Messages.ExistError, Captions.Email),
                        Color = MessageColor.Warning
                    };
            }

            selectedUser.CompanyName = company.CompanyName;
            selectedUser.Address = company.Address;
            selectedUser.MobileNumber = company.MobileNumber;
            selectedUser.Email = company.Email;

            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.UpdateSuccess,
                Color = MessageColor.Success
            };
        }


        public async Task<OperationStatus> UpdateAsync(Reseller company)
        {
            var selectedUser = SingleOrDefault(company.Id);
            if (selectedUser.Email != company.Email)
            {
                if (await ExistsByEmailAsync(company.Email, null))
                    return new OperationStatus
                    {
                        Status = false,
                        Title = Messages.MissionFail,
                        Message = string.Format(Messages.ExistError, Captions.Email),
                        Color = MessageColor.Warning
                    };
            }

            selectedUser.CompanyName = company.CompanyName;
            selectedUser.Address = company.Address;
            selectedUser.MobileNumber = company.MobileNumber;
            selectedUser.Email = company.Email;

            return new OperationStatus
            {
                Status = true,
                Title = Messages.MissionSuccess,
                Message = Messages.UpdateSuccess,
                Color = MessageColor.Success
            };
        }

        public async Task<OperationStatus> ChangePasswordAsync(int companyId, string password)
        {
            var selectedUser = await dbSet.SingleAsync(x => x.Id == companyId);
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

        private static VerifyUserStatus Verify(Reseller company, string password)
        {
            var result = VerifyUserStatus.VerifiedFaild;

            bool verifiedPassword = Encryption.VerifyPassword(password, company.Password);

            if (verifiedPassword)
            {
                if (company.IsActive) result = VerifyUserStatus.VerifiedSuccessfully;
                else result = VerifyUserStatus.UserIsbaned;
            }

            return result;
        }

        #endregion
        public VerifyUserStatus VerifyUserByEmail(string email, string password, ref Reseller company)
        {
            var selectedUser = dbSet.SingleOrDefault(x => x.Email == email);
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

        public async Task<bool> ExistsByMobileNumberAsync(string mobileNumber, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(user => user.MobileNumber.Equals(mobileNumber) && user.Id != id.Value);
            return await dbSet.AnyAsync(user => user.MobileNumber.Equals(mobileNumber));
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

        public async Task<bool> ExistsByEmailAsync(string email, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(user => user.Email.Equals(email) && user.Id != id.Value);
            return await dbSet.AnyAsync(user => user.Email.Equals(email));
        }



        public bool ExistsByMobileNumber(string num, int? id)
        {
            if (id.HasValue)
                return dbSet.Any(user => user.MobileNumber.Equals(num) && user.Id != id.Value);
            return dbSet.Any(user => user.MobileNumber.Equals(num));
        }

        public bool ExistsByEmail(string email, int? id)
        {
            if (id.HasValue)
                return dbSet.Any(user => user.Email.Equals(email) && user.Id != id.Value);
            return dbSet.Any(user => user.Email.Equals(email));
        }
        
        public IQueryable<TableResellerModel> GetDataTableResellerAccounts(string search)
        {
            IQueryable<TableResellerModel> selectedUsers = dbSet
                                            .OrderByDescending(x => x.CreateDate)
                                            .Include(x => x.Companies)
                                            .Select(x => new TableResellerModel
                                            {
                                                Id = x.Id,
                                                IsActive = x.IsActive,
                                                CompanyName=x.CompanyName,
                                                Name = x.Name,
                                                Username = x.UserName,
                                                MobileNumber = x.PhoneNumber,
                                                Email = x.Email,
                                                Address = x.Address,
                                                PostalCode = x.PostalCode.ToString(),
                                                PersonCode = x.PersonCode.ToString(),
                                                UserCount=x.Companies.Count
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selectedUsers = selectedUsers.Where(x => x.Name.Contains(search)).AsQueryable();

            return selectedUsers;
        }


        public void ActiveBaneReseller(int id)
        {
            Reseller selectedUser = dbSet.Find(id);
            if (selectedUser.IsActive)
                selectedUser.IsActive = false;
            else selectedUser.IsActive = true;
        }



        public ProfileModel GetProfile(int id)
        {
            var entity = dbSet.FirstOrDefault(x => x.Id == id && x.IsActive);

            if (entity != null)
            {
                return new ProfileModel
                {
                    Email = entity.Email,
                    Address = entity.Address,
                    CompanyName = entity.CompanyName,
                    Name = entity.Name,
                    PersonCode = entity.PersonCode.ToString(),
                    PostalCode = entity.PostalCode.ToString(),
                    CreateDate = entity.CreateDate.ToString(),
                    Username = entity.UserName,
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
                var entity = dbSet.FirstOrDefault(x => x.Id == userId && x.IsActive);

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
                    //entity.CompanyName = model.c;
                    //entity.LastName = model.LastName;
                    entity.Email = model.Email;
                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.PostalCode = long.Parse(model.PostalCode);
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

        public async Task<bool> IsCompanyNameExist(string CompanyName, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(user => user.CompanyName.ToLower().Equals(CompanyName.ToLower()) && user.Id != id.Value);
            return await dbSet.AnyAsync(user => user.CompanyName.ToLower().Equals(CompanyName.ToLower()));
        }
    }
}
