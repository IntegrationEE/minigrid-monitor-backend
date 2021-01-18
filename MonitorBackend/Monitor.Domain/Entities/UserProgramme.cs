using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class UserProgramme : IEntity
    {
        public UserProgramme()
        { }

        public UserProgramme(User user, int programmeId)
            : base()
        {
            User = user;
            ProgrammeId = programmeId;
        }

        public int UserId { get; private set; }
        public virtual User User { get; private set; }

        public int ProgrammeId { get; private set; }
        public virtual Programme Programme { get; private set; }
    }
}
