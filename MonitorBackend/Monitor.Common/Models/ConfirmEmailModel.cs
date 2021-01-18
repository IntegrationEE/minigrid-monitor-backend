using Monitor.Common.Helpers;

namespace Monitor.Common.Models
{
    public class ConfirmEmailModel
    {
        public string EmailToken { get; set; }

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(EmailToken))
            { throw new CustomException($"{nameof(EmailToken)} is null or empty."); }

            if (!TokenHelper.IsValid(EmailToken))
            { throw new CustomException($"{nameof(EmailToken)} has expired."); }
        }
    }
}
