using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class LocalGovernmentArea : BaseEntity
    {
        public string Name { get; private set; }
        public int StateId { get; private set; }
        public virtual State State { get; private set; }

        public void Set(string name, int stateId)
        {
            Name = name;
            StateId = stateId;
        }
    }
}
