using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpFinanceiro.Domains.Generics
{
    public interface IDomain<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Save(T entity);
        void Update(T entity);
    }
}
