using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class IntegrationStep : BaseEntity
    {
        private IntegrationStep()
        { }

        public IntegrationStep(Integration integration)
        {
            if (integration.Id > 0)
            {
                IntegrationId = integration.Id;
            }
            else
            {
                Integration = integration;
            }
        }

        public int IntegrationId { get; private set; }
        public virtual Integration Integration { get; private set; }

        public string Name { get; private set; }

        [Required]
        public string Function { get; private set; }

        [Range(1, 100)]
        public int Ordinal { get; private set; }

        public bool IsArchive { get; private set; }

        public virtual ICollection<IntegrationRecord> Records { get; set; }

        public void Set(string name, string function, int ordinal)
        {
            Name = name;
            Function = function;
            Ordinal = ordinal;
        }

        public void MarkAsArchived()
        {
            IsArchive = true;
        }
    }
}
