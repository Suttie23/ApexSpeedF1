using MvxStarter.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ApexSpeed.Core.Services
{
    public interface IUDPListenerService
    {
        void ListenerDispose();

        // General Telemetry
        Task<TelemetryModel> ReceiveTelemetryAsync(CancellationToken cancellationToken);

    }
}