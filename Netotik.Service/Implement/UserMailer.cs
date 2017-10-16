using Netotik.ViewModels.Identity.Account;
using Netotik.Services.Abstract;
using Mvc.Mailer;
using System.Text;

namespace Netotik.Services.Implement
{
    public class UserMailer : MailerBase, IUserMailer
    {

        public UserMailer()
        {
            MasterName = "_Layout";
        }

        public MvcMailMessage ResetPassword(EmailViewModel resetPasswordEmail)
        {
            ViewData.Model = resetPasswordEmail;
            return Populate(x =>
            {
                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                x.BodyEncoding = Encoding.UTF8;
                x.Subject = resetPasswordEmail.Subject;
                x.ViewName = resetPasswordEmail.ViewName;
                x.Body = resetPasswordEmail.Message;
                x.To.Add(resetPasswordEmail.To);
            });
        }
        public MvcMailMessage ContactUsEmail(EmailContactUsViewModel ContactUsEmail)
        {
            ViewData.Model = ContactUsEmail;
            return Populate(x =>
            {
                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                x.BodyEncoding = Encoding.UTF8;
                x.Subject = ContactUsEmail.Subject;
                x.ViewName = ContactUsEmail.ViewName;
                x.Body = ContactUsEmail.Message;
                x.To.Add(ContactUsEmail.To);
            });
        }

        public MvcMailMessage ConfirmAccount(EmailViewModel confirmAccountEmail)
        {
            ViewData.Model = confirmAccountEmail;
            return Populate(x =>
            {
                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                x.BodyEncoding = Encoding.UTF8;
                x.Body = confirmAccountEmail.Message;
                x.Subject = confirmAccountEmail.Subject;
                x.ViewName = confirmAccountEmail.ViewName;
                x.To.Add(confirmAccountEmail.To);
            });
        }
        public MvcMailMessage Factor(EmailFactorViewModel factor)
        {
            ViewData.Model = factor;
            return Populate(x =>
            {
                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                x.BodyEncoding = Encoding.UTF8;
                x.Body = factor.Message;
                x.Subject = factor.Subject;
                x.ViewName = factor.ViewName;
                x.To.Add(factor.To);
            });
        }
    }
}