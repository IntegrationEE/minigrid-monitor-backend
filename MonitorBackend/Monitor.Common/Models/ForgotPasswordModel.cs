using System.Text;

namespace Monitor.Common.Models
{
    public class ForgotPasswordModel
    {
        /// <summary>
        /// User e-mail address or Associated e-amil
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Backend API url 
        /// </summary>
        public string BaseUrl { get; set; }

        public void IsValid()
        {
            var exceptionMessage = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Email))
            { exceptionMessage.AppendLine($"{nameof(Email)} is null or empty."); }

            if (string.IsNullOrWhiteSpace(BaseUrl))
            { exceptionMessage.AppendLine($"{nameof(BaseUrl)} is null or empty."); }

            if (exceptionMessage.Length > 0)
            { throw new CustomException(exceptionMessage.ToString()); }
        }
    }
}
