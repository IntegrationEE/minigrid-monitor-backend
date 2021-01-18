using System.Collections.Generic;
using Monitor.Common.Enums;

namespace Monitor.Common.Models
{
    public class ApproveModel
    {
        public ApproveModel()
        {
            Programmes = new List<int>();
        }

        public RoleCode Role { get; set; }
        public bool IsHeadOfCompany { get; set; }
        public List<int> Programmes { get; set; }
    }
}
