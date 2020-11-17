using System;
using System.Collections.Generic;
using System.Text;

namespace Paginas.Api.Entities
{
    public class PaginaXPaciente
    {
        public int ID { get; set; }
        public int PaginaId { get; set; }
        //public Pagina Pagina { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataVisualizacao { get; set; }


        public PaginaXPaciente()
        {

        }

        public PaginaXPaciente(Pagina pagina)
        {
            PaginaId = pagina.ID;
            //Noticia = noticia;
        }

        public PaginaXPaciente(int id, int paginaId, int pacienteId, int medicoId)
        {
            ID = id;
            PaginaId = paginaId;
            PacienteId = pacienteId;
            //MedicoId = medicoId;
        }
    }
}
