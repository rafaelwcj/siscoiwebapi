using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Events;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
