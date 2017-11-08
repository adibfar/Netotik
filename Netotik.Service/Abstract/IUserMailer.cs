using Netotik.Domain.Entity;
using Netotik.ViewModels.Identity.Account;
using Mvc.Mailer;

namespace Netotik.Services.Abstract
{
    public interface IUserMailer
    {

        MvcMailMessage ResetPassword(EmailViewModel resetPasswordEmail);
        MvcMailMessage ContactUsEmail(EmailContactUsViewModel ContactUsEmail);
        MvcMailMessage ConfirmAccount(EmailViewModel confirmAccountEmail);
        MvcMailMessage Factor(EmailFactorViewModel factor);
        MvcMailMessage ClientUserPass(EmailClientUserPassViewModel ClientUserPass);

    }
}
