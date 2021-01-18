using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.Extensions;

namespace Monitor.Business.Repositories
{
    public class MailRepository : IMailRepository
    {
        private readonly IBaseRepository _repository;

        public MailRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task SendMessage(MailType mailType, string recipient, string link = null, string username = null)
        {
            await SendMessage(mailType, new[] { recipient }, link, username);
        }

        public async Task SendMessage(MailType mailType, string[] recipients, string link = null, string username = null)
        {
            SmtpClient smtpClient = null;
            MailMessage mailMessage = null;

            try
            {
                var settings = await _repository.GetQuery<Setting>(x =>
                    x.Code == SettingCode.SMTP_LOGIN ||
                    x.Code == SettingCode.SMTP_PASSWORD ||
                    x.Code == SettingCode.SMTP_PORT ||
                    x.Code == SettingCode.SMTP_SERVER ||
                    x.Code == SettingCode.SMTP_USE_SSL ||
                    x.Code == SettingCode.SMTP_FROM_ADDRESS
                ).ToListAsync();

                smtpClient = GetSmtpClient(settings);
                var mail = GetSubjectAndMessage(mailType, username);
                var body = GetEmailBody(mailType, link, mail);

                mailMessage = new MailMessage
                {
                    From = new MailAddress(settings.Get(SettingCode.SMTP_FROM_ADDRESS).Value, "Mini-Grid Monitor"),
                    Body = body,
                    Subject = mail.Subject,
                    IsBodyHtml = true,
                };

                foreach (var recipient in recipients)
                {
                    mailMessage.To.Add(recipient);
                }

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                smtpClient?.Dispose();
                mailMessage?.Dispose();
            }
        }

        private string GetEmailBody(MailType type, string link, (string Subject, string Message) mail)
        {
            try
            {
                string body = string.Empty;

                using (StreamReader reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, $"Assets/EmailTemplate.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("[TITLE]", mail.Subject);
                body = body.Replace("[BODY]", mail.Message);
                body = body.Replace("[SUMMARY]", GetSummary(type));

                if (!string.IsNullOrWhiteSpace(link))
                {
                    body = body.Replace("[LINK]", link);
                }

                return body;
            }
            catch (Exception ex)
            {
                _repository.Logger.Error(ex);
                throw ex;
            }
        }

        private (string Subject, string Message) GetSubjectAndMessage(MailType type, string username = null) =>
           type switch
           {
               MailType.REGISTERED_BY_ADMIN => ("New account", $"Created new account in Mini-Grid Monitor application. Username: <b>{username}</b>. You can set password using the link:"),
               MailType.REGISTRATION => ("Register new account", $"Registered new account in Mini-Grid Monitor application. Username: <b>{username}</b>. You can confirm your e-mail address using the link:"),
               MailType.RESET_PASSWORD => ("Password reset", "We've received a password change request. To change your password, click the link below:"),
               MailType.NEW_ACCOUNT_IN_COMPANY => ("New account to approve", $"New user <b>{username}</b> is waiting for approval to join to your company."),
               _ => throw new CustomException("Incorrect email type."),
           };

        private static string GetSummary(MailType mailType)
        {
            switch (mailType)
            {
                case MailType.REGISTRATION:
                case MailType.RESET_PASSWORD:
                case MailType.REGISTERED_BY_ADMIN:
                    var summary = new StringBuilder("<div style=\"margin-top: 20px; margin-bottom: 20px; font-size: 10pt; color: #005a95; text-decoration: none; cursor: pointer; font-family: 'segoe ui', 'lucida sans', sans-serif; font-size: 15px; font-style: normal; font-weight: 400; letter-spacing: normal; orphans: 2; background-color: #ffffff;\">");
                    summary.AppendLine("<a href=\"[LINK]\" target=\"_blank\" style=\"color: #005a95; text-decoration: none; cursor: pointer;\">[LINK]</a>");
                    summary.AppendLine("</div>");

                    return summary.ToString();
                case MailType.NEW_ACCOUNT_IN_COMPANY:
                    return string.Empty;
                default:
                    throw new CustomException("Incorrect email type.");
            }
        }

        private SmtpClient GetSmtpClient(IList<Setting> settings) =>
            new SmtpClient(settings.Get(SettingCode.SMTP_SERVER).Value, settings.Get(SettingCode.SMTP_PORT).GetIntValue())
            {
                EnableSsl = settings.Get(SettingCode.SMTP_USE_SSL).GetBoolValue(),
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(settings.Get(SettingCode.SMTP_LOGIN).Value, settings.Get(SettingCode.SMTP_PASSWORD).Value)
            };

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _repository.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
