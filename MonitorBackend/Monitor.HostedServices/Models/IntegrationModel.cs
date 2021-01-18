using System.Collections.Generic;

namespace Monitor.HostedServices.Models
{
    internal class IntegrationModel
    {
        public int Id { get; set; }
        public int Interval { get; set; }
        public string QuestionHash { get; set; }
        public string Token { get; set; }
        public IEnumerable<StepModel> Steps { get; set; }
    }

    internal class StepModel
    {
        public int Id { get; set; }
        public int Ordinal { get; set; }
        public string Function { get; set; }
        public string Name { get; set; }
    }
}
