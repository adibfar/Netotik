using Netotik.Domain.Entity;
using Netotik.ViewModels.Identity.Account;
using Mvc.Mailer;

namespace Netotik.Services.Abstract
{
    public interface IUserMailer
    {

        MvcMailMessage ResetPassword(EmailViewModel resetPasswordEmail);
        MvcMailMessage ConfirmAccount(EmailViewModel confirmAccountEmail);

    }
}