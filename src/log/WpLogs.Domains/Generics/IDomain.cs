using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpLogs.Domains.Generics
{
    public interface IDomain<T> where T : class
    {
        T Save(T entity);
        T Update(T entity);
        IEnumerable<T> GetAll(int idCliente);
        T GetById(int entityId);
        void Delete(T entity);
    }
}
