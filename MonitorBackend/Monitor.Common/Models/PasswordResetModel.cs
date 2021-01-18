using System.Text;
using Monitor.Common.Helpers;

namespace Monitor.Common.Models
{
    public class PasswordResetModel
    {
        public string EncryptedPassword { get; set; }

        public string PasswordToken { get; set; }

        public void IsValid()
        {
            var exceptionMessage = new StringBuilder();

            if (string.IsNullOrWhiteSpace(PasswordToken))
            { exceptionMessage.AppendLine($"{nameof(PasswordToken)} is null or empty."); }

            if (string.IsNullOrWhiteSpace(EncryptedPassword))
            { exceptionMessage.AppendLine($"{nameof(EncryptedPassword)} is null or empty."); }

            if (exceptionMessage.Length > 0)
            { throw new CustomException(exceptionMessage.ToString()); }

            if (!TokenHelper.IsValid(PasswordToken))
            { throw new CustomException($"{nameof(PasswordToken)} has expired."); }
        }
    }
}
