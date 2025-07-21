using MediatR;
using System;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        public Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
