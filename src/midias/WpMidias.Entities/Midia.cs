using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace WpMidias.Entities
{
    public class Midia : Base
    {
        private const string _basePath = @"C:\inetpub\wwwroot\midias\";

        [NotMapped]
        public byte[] Arquivo { get { return SetArquivo(); } set { } }

        public string CaminhoFisico { get; set; }
        public string CaminhoVirtual { get; set; }
        public Tipo Tipo { get; set; }
        public int TipoId { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        public string Extensao { get; set; }
        public int CodigoExterno { get; set; }

        public byte[] SetArquivo()
        {
            try
            {
                if (Directory.Exists(_basePath))
                {
                    string filePath = string.Concat(_basePath, $"{Nome}{Extensao}");
                    if (File.Exists(filePath))
                        return File.ReadAllBytes(filePath);                    
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
