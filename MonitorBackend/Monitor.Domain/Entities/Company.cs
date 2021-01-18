using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class Company : BaseEntity
    {
        public Company()
        {
            Sites = new HashSet<Site>();
            Users = new HashSet<User>();
        }

        [Required]
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public int StateId { get; private set; }
        public virtual State State { get; private set; }
        public int LocalGovernmentAreaId { get; private set; }
        public virtual LocalGovernmentArea LocalGovernmentArea { get; private set; }
        public string WebsiteUrl { get; private set; }
        public string PhoneNumber { get; private set; }

        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public void Set(string name, string websiteUrl, string phoneNumber)
        {
            Name = name;
            WebsiteUrl = websiteUrl;
            PhoneNumber = phoneNumber;
        }

        public void SetAddress(string city, string street, string number, int stateId,
            int localGovernmentAreaId)
        {
            City = city;
            Street = street;
            Number = number;
            StateId = stateId;
            LocalGovernmentAreaId = localGovernmentAreaId;
        }
    }
}
