namespace WebPixPrincipalRepository.Entity
{
    public class Page : Base
    {
        
        public string Titulo { get; set; }
        public byte[] Conteudo { get; set; }
        public string Url { get; set; }
        public int idMenu { get; set; }
      
    }
}
