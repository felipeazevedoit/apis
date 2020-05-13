using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mensagens.Domains.Generics
{
    public interface IDomain<T> where T : class
    {
        T Save(T entity);
        T Update(T entity);
        IEnumerable<T> GetAll(int idCliente);
        T GetById(int entityId, int idCliente);
        T Delete(T entity);
    }
}
