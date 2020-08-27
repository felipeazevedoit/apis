using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpMedicos.Entities;
using WpMedicos.Infrastructure.Exceptions;

namespace WpMedicos.Infrastructure
{
    public class MedicoRepository : Repository<Medico>
    {
        public async Task<IEnumerable<MedicoXEspecialidade>> GetAllWithEspecialidadesAsync(int idCliente)
        {
            try
            {
                using (var context = new WpMedicosContext())
                {
                    var query = await (from mXe in context.MedicosXEspecialidades
                                 join m in context.Medicos on mXe.MedicoId equals m.ID where m.IdCliente.Equals(idCliente)
                                 join e in context.Especialidades on mXe.EspecialidadeId equals e.ID
                                 select new
                                 {
                                     medico = m,
                                     especialidade = e,
                                 }).ToListAsync();

                    IList<MedicoXEspecialidade> result = new List<MedicoXEspecialidade>();

                    foreach (var item in query)
                    {
                        try
                        {
                            result.Add(new MedicoXEspecialidade(item.medico, item.especialidade ?? null));
                        }
                        catch(Exception e)
                        {

                        }
                    }

                    return result;
                }                
            }
            catch(Exception e)
            {
                throw new MedicoException("Não foi possível recuperar os médicos solicitados.", e);
            }
        }

        public async Task<IEnumerable<MedicoXEspecialidade>> GetAllByEspecialidadeAsync(int especialidadeId, int idCliente)
        {
            try
            {
                using (var context = new WpMedicosContext())
                {
                    var query = await (from mXe in context.MedicosXEspecialidades
                                       join m in context.Medicos on mXe.MedicoId equals m.ID
                                       where m.IdCliente.Equals(idCliente)
                                       join e in context.Especialidades on especialidadeId equals e.ID
                                       select new
                                       {
                                           medico = m,
                                           especialidade = e,
                                       }).ToListAsync();

                    IList<MedicoXEspecialidade> result = new List<MedicoXEspecialidade>();

                    foreach (var item in query)
                    {
                        result.Add(new MedicoXEspecialidade(item.medico, item.especialidade));
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível recuperar os médicos solicitados.", e);
            }
        }

        public async Task<MedicoXEspecialidade> GetWithEspecialidadeAsync(int medicoId, int idCliente)
        {
            try
            {
                using (var context = new WpMedicosContext())
                {
                    var query = await (from mXe in context.MedicosXEspecialidades
                                        join m in context.Medicos on mXe.MedicoId equals m.ID where m.IdCliente.Equals(idCliente) && m.ID.Equals(medicoId)
                                        join e in context.Especialidades on mXe.EspecialidadeId equals e.ID
                                        select new
                                        {
                                            medico = m,
                                            especialidade = e,
                                        }).SingleOrDefaultAsync();

                    var result = new MedicoXEspecialidade(query.medico, query.especialidade);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível recuperar o médico solicitado.", e);
            }
        }
    }
}
