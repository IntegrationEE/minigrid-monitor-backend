using System;
using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class SiteViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }

        public DateTime CommissioningDate { get; set; }
    }
}
