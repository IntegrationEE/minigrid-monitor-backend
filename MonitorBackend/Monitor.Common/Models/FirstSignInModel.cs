using System.Threading.Tasks;
using Monitor.Common.Helpers;

namespace Monitor.Common.Models
{
    public class FirstSignInModel
    {
        public string FullName { get; set; }
        public int? CompanyId { get; set; }

        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int? StateId { get; set; }
        public int? LocalGovernmentAreaId { get; set; }
        public string WebsiteUrl { get; set; }
        public string PhoneNumber { get; set; }

        public async Task IsValid()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            { throw new CustomException($"{nameof(FullName)} is required."); }
            else if (!CompanyId.HasValue)
            {
                if (string.IsNullOrWhiteSpace(CompanyName))
                { throw new CustomException($"Company Name is required."); }
                else if (string.IsNullOrWhiteSpace(City))
                { throw new CustomException($"{nameof(City)} is required."); }
                else if (string.IsNullOrWhiteSpace(Street))
                { throw new CustomException($"{nameof(Street)} is required."); }
                else if (string.IsNullOrWhiteSpace(Number))
                { throw new CustomException($"{nameof(Number)} is required."); }
                else if (!StateId.HasValue)
                { throw new CustomException("State is required."); }
                else if (!LocalGovernmentAreaId.HasValue)
                { throw new CustomException("LGA is required."); }
                else if (string.IsNullOrWhiteSpace(PhoneNumber))
                { throw new CustomException($"Phone number is required."); }
                else if (!string.IsNullOrWhiteSpace(WebsiteUrl) && !await UrlHelper.Exists(WebsiteUrl))
                { throw new CustomException($"Website doesn't exist"); }
            }
        }
    }
}
