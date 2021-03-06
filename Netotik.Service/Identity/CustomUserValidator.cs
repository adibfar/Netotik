using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Netotik.Domain.Entity;
using Microsoft.AspNet.Identity;
using Netotik.Services.Identity;

namespace Netotik.Services.Identity
{
    public class CustomUserValidator<TUser, TKey> : IIdentityValidator<User>
        where TUser : class, IUser<long>
        where TKey : IEquatable<long>
    {

        public bool AllowOnlyAlphanumericUserNames { get; set; }
        public bool RequireUniqueEmail { get; set; }

        private ApplicationUserManager Manager { get; set; }
        public CustomUserValidator(ApplicationUserManager manager)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            AllowOnlyAlphanumericUserNames = true;
            Manager = manager;
        }
        public async Task<IdentityResult> ValidateAsync(User item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            var errors = new List<string>();
            await ValidateUserName(item, errors);
            //if (RequireUniqueEmail)
            //    await ValidateEmailAsync(item, errors);
            return  errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

        private async Task ValidateUserName(User user, ICollection<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(string.Format(Resources.Captions.RequiredError, Resources.Captions.UserName));
            }
            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, "^[A-Za-z0-9@_\\.]+$"))
            {
                errors.Add("Username is not valid.. ");
            }
            else
            {
                var owner = await Manager.FindByNameAsync(user.UserName);
                if (owner != null && !EqualityComparer<long>.Default.Equals(owner.Id, user.Id))
                    errors.Add("Username has taken by other user..");
            }
        }

        //private async Task ValidateEmailAsync(User user, ICollection<string> errors)
        //{
        //    var email = await Manager.GetEmailStore().GetEmailAsync(user).WithCurrentCulture();
        //    if (string.IsNullOrWhiteSpace(email))
        //    {
        //        errors.Add(string.Format(Resources.Captions.RequiredError, Resources.Captions.Email));
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var m = new MailAddress(email);

        //        }
        //        catch (FormatException)
        //        {
        //            errors.Add("Email is not valid..");
        //            return;
        //        }
        //        //var owner = await Manager.FindByEmailAsync(email);
        //        //if (owner != null && !EqualityComparer<long>.Default.Equals(owner.Id, user.Id))
        //        //    errors.Add("این ایمیل قبلا ثبت شده است");
        //    }
        //}
    }

}
