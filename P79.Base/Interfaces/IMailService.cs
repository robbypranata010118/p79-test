using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Interfaces
{
    public interface IMailService
    {
        Task SendEmailVerification(string id ,string to , string subject , string body , bool isHtml);
        Task SendEmailResetPassword(string id, string to, string subject, string body, bool isHtml);
    }
}
