using Academy.Domain.Core.Commands;
using Academy.Domain.Core.Events;
using System.Threading.Tasks;

namespace Academy.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
