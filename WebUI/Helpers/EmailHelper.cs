using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebUI.Helpers;

public static class EmailHelper
{
    public static bool ResetPassword(string userEmail, string resetToken)
    {
        try
        {
            SmtpClient smtpClient = new("smtp.ethereal.email", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("chester.herzog@ethereal.email", "39FFx9g5jujJWMX4yG");

            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress("alfreda.grant34@ethereal.email");
            mailMessage.To.Add(userEmail);
            mailMessage.Subject = "Reset your password";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"<a target='_blank' href='http://localhost:5053/auth/reset?token={resetToken}&email={userEmail}'>Reset Password</a>";
            smtpClient.Send(mailMessage);
            return true;

        }
        catch (Exception e)
        {

            return false;

        }


    }
}
