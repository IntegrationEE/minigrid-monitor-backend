using System.Text.Json.Serialization;

namespace Monitor.Common.Models
{
    public class AuthenticationModel : SignInModel
    {
        [JsonIgnore]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
