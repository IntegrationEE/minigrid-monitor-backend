using System;

namespace Monitor.Domain.Base
{
    public interface IBaseEntity : IEntity
    {
        int Id { get; set; }

        DateTime? Created { get; set; }

        DateTime? Modified { get; set; }
    }
}
