using Formulario.Dinamico.Entities;
using Formulario.Dinamico.Infrastructure.Exceptions;
using Notificacoes.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Formulario.Dinamico.Infrastructure
{
    public class PerguntasRepository : Repository<Pergunta>
    {
        public async Task<IEnumerable<Pergunta>> GetPerguntasAsync(int idCliente, int idExterno)
        {
            try
            {
                using (var context = new FormularioContext())
                {
                    var query = await context.Perguntas.Where(p => p.IdCliente.Equals(idCliente))
                        .Select(p => new
                        {
                            p,
                            respostas = context.Respostas.Where(r => r.PerguntaId.Equals(p.ID) && r.CodigoExterno.Equals(idExterno)).ToList(),
                        }).ToListAsync();


                    IList<Pergunta> perguntas = new List<Pergunta>();

                    foreach (var item in query)
                    {
                        item.p.Respostas = item.respostas;

                        perguntas.Add(item.p);
                    }

                    return perguntas;
                }
            }
            catch(Exception e)
            {
                throw new PerguntaException("Não foi possível recuperar as perguntas solicitadas.", e);
            }
        }
    }
}
