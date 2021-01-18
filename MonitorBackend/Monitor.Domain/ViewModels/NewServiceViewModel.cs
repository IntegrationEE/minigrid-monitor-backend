using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class NewServiceViewModel : BaseVisiteSiteViewModel
    {
        public int Commercial { get; set; }

        public int Productive { get; set; }

        public int Health { get; set; }

        public int Education { get; set; }
    }
}