namespace WebPixPrincipalRepository.Entity
{
    public class Menu : Base
    {
        
        public string Url { get; set; }
        public int Pai { get; set; }
        public int Tipo { get; set; }
    }
}
