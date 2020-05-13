using System.Collections.Generic;


namespace WebPixInAPI.Model
{
    public class MotorAuxViewModel : BaseViewModel
    {
        public List<AcaoViewModel> Acoes { get; set; }
        public string Url { get; set; }
    }
}
