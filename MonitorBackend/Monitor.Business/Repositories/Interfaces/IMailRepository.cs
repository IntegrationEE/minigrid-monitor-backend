using System;
using System.Threading.Tasks;
using Monitor.Common.Enums;

namespace Monitor.Business.Repositories
{
    public interface IMailRepository : IDisposable
    {
        Task SendMessage(MailType mailType, string recipient, string link = null, string username = null);
        Task SendMessage(MailType mailType, string[] recipients, string link = null, string username = null);
    }
}
