using System;
using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class CustomerSatisfactionViewModel : BaseVisiteSiteViewModel
    {
        public int VerySatisfied { get; set; }

        public int SomehowSatisfied { get; set; }

        public int NeitherSatisfiedNorUnsatisfied { get; set; }

        public int SomehowUnsatisfied { get; set; }

        public int VeryUnsatisfied { get; set; }
    }
}
