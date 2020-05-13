using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpFinanceiro.Entities;

namespace WpFinanceiro.Infrastructure
{
    public class ExtratoRepository : Repository<Extrato>
    {
        public async Task<IEnumerable<Extrato>> GetByQueryAsync(string query)
        {
            using (var context = new WpFinanceiroContext())
            {
                var result = await context.Set<Extrato>().FromSql(query).ToListAsync();
                return result;
            }   
        }
    }
}
