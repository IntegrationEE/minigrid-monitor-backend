using Monitor.Domain.Base;

namespace Monitor.Domain.LightModels
{
    public class IntegrationLightModel : BaseLightModel
    {
        public string Token { get; set; }
        public int Interval { get; set; }
        public string QuestionHash { get; set; }
        public bool IsActive { get; set; }
    }
}
