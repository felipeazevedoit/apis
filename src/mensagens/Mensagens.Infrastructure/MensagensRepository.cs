using Mensagens.Entities;
using Mensagens.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mensagens.Infrastructure
{
    public class MensagensRepository : Repository<Mensagem>
    {
        public async Task<IEnumerable<Mensagem>> GetByEntidadeIdAsync(int entityId, int idCliente)
        {
            try
            {
                using (var context = new MensagensContext())
                {
                    var mensagens = await context.Mensagens.Where(m => (m.DestinatarioId.Equals(entityId) || m.RemetenteId.Equals(entityId)) && m.IdCliente.Equals(idCliente)).ToListAsync();
                    return mensagens;
                }
            }
            catch(Exception e)
            {
                throw new MensagensException("Não foi possível recuperar as mensagens solicitadas.", e);
            }
        }
    }
}
