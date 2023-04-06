using MvxStarter.Core.Models;
using System.Threading.Tasks;

namespace ApexSpeed.Core.Services
{
    public interface IUDPListenerService
    {
        // General Telemetry
        Task<TelemetryModel> ReceiveTelemetryAsync();

    }
}