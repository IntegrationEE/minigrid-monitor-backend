namespace Monitor.Common.Models
{
    public class SignUpModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(Username))
            { throw new CustomException($"{nameof(Username)} is required."); }
            else if (string.IsNullOrWhiteSpace(Email))
            { throw new CustomException($"{nameof(Email)} is required."); }
            else if (string.IsNullOrWhiteSpace(Password))
            { throw new CustomException($"{nameof(Password)} is required."); }
        }
    }
}
