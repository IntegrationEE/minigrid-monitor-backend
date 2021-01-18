using Monitor.Common;
using Monitor.Domain.Base;

namespace Monitor.Domain.LightModels
{
    public class UserDetailsModel : BaseViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int? StateId { get; set; }
        public string State { get; set; }
        public int? LocalGovernmentAreaId { get; set; }
        public string LocalGovernmentArea { get; set; }
        public string PhoneNumber { get; set; }

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            { throw new CustomException($"{nameof(FullName)} is required."); }
            else if (string.IsNullOrWhiteSpace(City))
            { throw new CustomException($"{nameof(City)} is required."); }
            else if (string.IsNullOrWhiteSpace(Street))
            { throw new CustomException($"{nameof(Street)} is required."); }
            else if (string.IsNullOrWhiteSpace(Number))
            { throw new CustomException($"{nameof(Number)} is required."); }
            else if (string.IsNullOrWhiteSpace(PhoneNumber))
            { throw new CustomException($"{nameof(PhoneNumber)} is required."); }
            else if (!LocalGovernmentAreaId.HasValue)
            { throw new CustomException($"LGA is required."); }
            else if (!StateId.HasValue)
            { throw new CustomException($"{nameof(State)} is required."); }
        }
    }
}
