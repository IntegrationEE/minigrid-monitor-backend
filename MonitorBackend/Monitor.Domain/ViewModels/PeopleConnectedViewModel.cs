using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class PeopleConnectedViewModel : BaseVisiteSiteViewModel
    {
        public int Productive { get; set; }

        public int Commercial { get; set; }

        public int Residential { get; set; }

        public int Public { get; set; }
    }
}
