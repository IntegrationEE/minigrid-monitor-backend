using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class MeteringType : BaseEntity
    {
        public MeteringType()
        { }

        public MeteringType(string name)
        {
            Set(name);
        }

        public string Name { get; private set; }

        public void Set(string name)
        {
            Name = name;
        }
    }
}
