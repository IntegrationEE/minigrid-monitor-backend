using System.Threading.Tasks;
using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Helpers;

namespace Monitor.Domain.ViewModels
{
    public class CompanyViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int LocalGovernmentAreaId { get; set; }
        public string LocalGovernmentAreaName { get; set; }
        public string WebsiteUrl { get; set; }
        public string PhoneNumber { get; set; }

        public async Task IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
            { throw new CustomException($"{nameof(Name)} is required."); }
            else if (string.IsNullOrWhiteSpace(City))
            { throw new CustomException($"{nameof(City)} is required."); }
            else if (string.IsNullOrWhiteSpace(Street))
            { throw new CustomException($"{nameof(Street)} is required."); }
            else if (string.IsNullOrWhiteSpace(Street))
            { throw new CustomException($"{nameof(Number)} is required."); }
            else if (StateId == 0)
            { throw new CustomException($"{nameof(StateId)} is required."); }
            else if (LocalGovernmentAreaId == 0)
            { throw new CustomException($"LGA is required."); }
            else if (string.IsNullOrWhiteSpace(PhoneNumber))
            { throw new CustomException($"{nameof(PhoneNumber)} is required."); }
            else if (!string.IsNullOrWhiteSpace(WebsiteUrl) && !await UrlHelper.Exists(WebsiteUrl))
            { throw new CustomException($"Website doesn't exist"); }
        }
    }
}
