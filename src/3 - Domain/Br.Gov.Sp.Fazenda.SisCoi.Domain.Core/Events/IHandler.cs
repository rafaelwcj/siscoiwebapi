namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Events
{
    public interface IHandler<in T> where T : Message 
    {
        void Handler(T message);
    }
}
