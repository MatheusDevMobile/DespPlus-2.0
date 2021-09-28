using System.Linq;

namespace PowerDev.Enterprise.Eureka.Interfaces
{
    public interface IFiltroBase<T>
    {
        IQueryable<T> ObterFiltro(IQueryable<T> lista);
        bool Satifaz(T entidade);
    }
}