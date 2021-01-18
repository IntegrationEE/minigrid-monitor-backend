using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class IntegrationStepViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Function { get; set; }
        public int Ordinal { get; set; }
    }
}
