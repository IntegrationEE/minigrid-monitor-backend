using System.Threading.Tasks;
using Monitor.Domain.ViewModels;

namespace Monitor.HostedServices
{
    public interface IIntegrationHostedService
    {
        Task Start();

        void AddNew(IntegrationViewModel integration, int userId);

        void Restart(int integrationId, int userId);

        void Remove(int integrationId, int userId);
    }
}
