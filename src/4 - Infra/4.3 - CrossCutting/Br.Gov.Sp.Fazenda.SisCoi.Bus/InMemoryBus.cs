using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Bus;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Events;
using MediatR;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}
