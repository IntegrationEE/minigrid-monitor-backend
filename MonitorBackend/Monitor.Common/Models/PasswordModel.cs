namespace Monitor.Common.Models
{
    public class PasswordModel
    {
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(NewPassword))
            { throw new CustomException($"{nameof(NewPassword)} is null or empty."); }
            else if (string.IsNullOrWhiteSpace(CurrentPassword))
            { throw new CustomException($"{nameof(CurrentPassword)} is null or empty."); }
        }
    }
}
