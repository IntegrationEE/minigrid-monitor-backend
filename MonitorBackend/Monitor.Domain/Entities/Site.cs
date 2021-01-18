using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Monitor.Common;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class Site : BaseEntity
    {
        public Site()
        {
            Revenues = new HashSet<Revenue>();
            Employments = new HashSet<Employment>();
            Consumptions = new HashSet<Consumption>();
            FinanceOpex = new HashSet<FinanceOpex>();
            NewServices = new HashSet<NewService>();
            PeopleConnected = new HashSet<PeopleConnected>();
            Tariffs = new HashSet<Tariff>();
        }

        [Required]
        public string Name { get; private set; }

        public int StateId { get; private set; }

        public virtual State State { get; set; }

        public int CompanyId { get; private set; }

        public virtual Company Company { get; private set; }

        public int ProgrammeId { get; private set; }

        public virtual Programme Programme { get; private set; }

        public decimal Lat { get; private set; }

        public decimal Long { get; private set; }

        public virtual SiteTechParameter TechnicalParameter { get; private set; }

        public DateTime CommissioningDate { get; private set; }

        public virtual FinanceCapex FinanceCapex { get; private set; }

        public string QrCode { get; private set; }

        public bool IsPublished { get; private set; }

        public virtual ICollection<CustomerSatisfaction> CustomerSatisfactions { get; private set; }

        public virtual ICollection<Employment> Employments { get; private set; }

        public virtual ICollection<NewService> NewServices { get; private set; }

        public virtual ICollection<PeopleConnected> PeopleConnected { get; private set; }

        public virtual ICollection<Tariff> Tariffs { get; private set; }

        public virtual ICollection<Consumption> Consumptions { get; private set; }

        public virtual ICollection<Revenue> Revenues { get; private set; }

        public virtual ICollection<FinanceOpex> FinanceOpex { get; private set; }

        public void Set(string name, int stateId, int programmeId, int companyId, decimal lat, decimal longitude,
            DateTime comisioningDate)
        {
            Name = name;
            StateId = stateId;
            ProgrammeId = programmeId;
            CompanyId = companyId;
            Lat = lat;
            Long = longitude;
            CommissioningDate = comisioningDate;
        }

        public void UpdateQrCode()
        {
            QrCode = $"{Id}{(DateTime.UtcNow - Constants.UnixEpoch).TotalMilliseconds}";
        }

        public void ToggleIsPublished()
        {
            IsPublished = !IsPublished;
        }
    }
}
