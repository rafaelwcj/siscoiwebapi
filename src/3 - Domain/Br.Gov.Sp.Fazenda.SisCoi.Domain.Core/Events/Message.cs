using MediatR;
using System;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Events
{
    public abstract class Message : IRequest<bool>
    {
        public string MessageType  { get; protected set; }

        public Guid AggregateId { get; protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
