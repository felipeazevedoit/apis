using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpMidias.Entities;

namespace WpMidias.Infrastructure
{
    public class MidiasRepository : Repository<Midia>
    {
        public async Task<IEnumerable<Midia>> GetMidiasAsync(int idCliente)
        {
            var midias = default(IEnumerable<Midia>);
            using(var context = new WpMidiasContext())
            {
                midias = await context.Midias.Where(m => m.IdCliente.Equals(idCliente))
                    .Include(m => m.Categoria).Include(m => m.Tipo).ToListAsync();
            }

            return midias;
        }

        public async Task<Midia> GetMidiaAsync(int midiaId, int idCliente)
        {
            var midia = default(Midia);
            using (var context = new WpMidiasContext())
            {
                midia = await context.Midias.Where(m => m.IdCliente.Equals(idCliente) && m.ID.Equals(midiaId))
                    .Include(m => m.Categoria).Include(m => m.Tipo).SingleOrDefaultAsync();
            }

            return midia;
        }

        public async Task<IEnumerable<Midia>> GetMidiaByIdExternoAsync(int idExterno, int idCliente)
        {
            var midias = default(IEnumerable<Midia>);
            using (var context = new WpMidiasContext())
            {
                midias = await context.Midias.Where(m => m.IdCliente.Equals(idCliente) 
                && m.CodigoExterno.Equals(idExterno)).Include(m => m.Categoria).Include(m => m.Tipo).ToListAsync();
            }

            return midias;
        }

        public async Task<IEnumerable<Midia>> GetMidiaByIdsExternosAsync(IEnumerable<int> codigos, int idCliente)
        {
            var midias = default(IEnumerable<Midia>);
            using (var context = new WpMidiasContext())
            {
                midias = await context.Midias.Where(m => m.IdCliente.Equals(idCliente)
                && codigos.Contains(m.CodigoExterno)).Include(m => m.Categoria).Include(m => m.Tipo).ToListAsync();
            }

            return midias;
        }

        public async Task<IEnumerable<Midia>> GetByCodExternoFromTake(int codigoExterno, int idCliente, int lastid, int take)
        {
            var midias = default(IEnumerable<Midia>);
            using(var context = new WpMidiasContext())
            {
                midias = await context.Midias
                    .Where(m => m.IdCliente.Equals(idCliente)
                                && m.CodigoExterno.Equals(codigoExterno)
                                && m.ID > lastid)
                    .Include(m => m.Categoria)
                    .Include(m => m.Tipo)
                    .Take(take)
                    .ToListAsync();
            }

            return midias;
        }
    }
}
