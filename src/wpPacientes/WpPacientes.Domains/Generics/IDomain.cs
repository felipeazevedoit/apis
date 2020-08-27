using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpPacientes.Domains.Generics
{
    public interface IDomain<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int idCliente);
        T GetById(int entityId, int idCliente);
        T Save(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
