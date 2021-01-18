using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class Integration : BaseEntity
    {
        public Integration()
        {
            Records = new HashSet<IntegrationRecord>();
            Steps = new HashSet<IntegrationStep>();
        }

        [Required]
        public string Name { get; private set; }

        [Required]
        [StringLength(200)]
        public string Token { get; private set; }

        public int Interval { get; private set; }

        [Required]
        public string QuestionHash { get; private set; }

        public bool IsActive { get; private set; }

        public virtual ICollection<IntegrationRecord> Records { get; private set; }
        public virtual ICollection<IntegrationStep> Steps { get; private set; }

        public void Set(string name, string token, int interval, string question)
        {
            Name = name;
            Token = token;
            Interval = interval;
            QuestionHash = question;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
