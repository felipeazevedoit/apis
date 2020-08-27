using System;
using System.Collections.Generic;
using System.Text;

namespace WpNoticias.Entities
{
    public class NoticiaXPaciente
    {
        public int ID { get; set; }
        public int NoticiaId { get; set; }
        //public Noticia Noticia { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataVisualizacao { get; set; }


        public NoticiaXPaciente()
        {

        }

        public NoticiaXPaciente(Noticia noticia)
        {
            NoticiaId = noticia.ID;
            //Noticia = noticia;
        }

        public NoticiaXPaciente(int id, int noticiaId, int pacienteId)
        {
            ID = id;
            NoticiaId = noticiaId;
            PacienteId = pacienteId;
        }
    }
}
