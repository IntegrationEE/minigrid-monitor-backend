using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.ViewModels
{
    public class IntegrationViewModel : BaseViewModel
    {
        public IntegrationViewModel()
        {
            Steps = new List<IntegrationStepViewModel>();
        }

        public string Name { get; set; }
        public string Token { get; set; }

        public int Interval { get; set; }
        public string QuestionHash { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public IntegrationTaskStatus? TaskStatus { get; set; }

        public List<IntegrationStepViewModel> Steps { get; set; }

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
            { throw new CustomException($"{nameof(Name)} is required."); }
            else if (string.IsNullOrWhiteSpace(Token))
            { throw new CustomException($"{nameof(Token)} is required."); }
            else if (string.IsNullOrWhiteSpace(QuestionHash))
            { throw new CustomException($"{nameof(QuestionHash)} is required."); }
            else if (Interval <= 0)
            { throw new CustomException($"{nameof(Interval)} has to be greater than 0."); }
            else if (Steps.GroupBy(z => z.Ordinal).Any(z => z.Count() > 1))
            { throw new CustomException("More than one steps have the same ordinal."); }
            else if (Steps.Any(z => string.IsNullOrWhiteSpace(z.Function)))
            { throw new CustomException("Function's name is required."); }
        }

        public bool WereStepsChanged(IList<string> currentSteps)
        {
            var stepsChange = currentSteps.Count != Steps.Count;

            if (!stepsChange)
            {
                Steps = Steps.OrderBy(z => z.Ordinal).ToList();

                for (var i = 0; i < currentSteps.Count; i++)
                {
                    if (currentSteps[i] != Steps[i].Function)
                    {
                        stepsChange = true;
                        break;
                    }
                }
            }

            return stepsChange;
        }

        public void SetTaskStatus(bool currentIsActive, int currentInterval, bool stepsWereChange, bool notDefinedSteps)
        {
            if (!currentIsActive && IsActive)
            {
                TaskStatus = IntegrationTaskStatus.ADD;
            }
            else if (!notDefinedSteps && stepsWereChange || (currentIsActive && currentInterval != Interval))
            {
                TaskStatus = IntegrationTaskStatus.RESTART;
            }
            else if (notDefinedSteps || (currentIsActive && !IsActive))
            {
                TaskStatus = IntegrationTaskStatus.REMOVE;
            }
        }
    }
}
