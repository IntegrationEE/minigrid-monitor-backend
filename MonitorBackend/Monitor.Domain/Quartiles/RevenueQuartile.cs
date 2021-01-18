using Monitor.Domain.Base;
using Monitor.Common.Models;

namespace Monitor.Domain.Quartiles
{
    public class RevenueQuartile : IQuartile
    {
        public double? ResidentialQ1 { get; set; }
        public double? ResidentialQ3 { get; set; }
        public Quartile ResidentialMinMax => new Quartile(ResidentialQ1, ResidentialQ3);

        public double? CommercialQ1 { get; set; }
        public double? CommercialQ3 { get; set; }
        public Quartile CommercialMinMax => new Quartile(CommercialQ1, CommercialQ3);

        public double? ProductiveQ1 { get; set; }
        public double? ProductiveQ3 { get; set; }
        public Quartile ProductiveMinMax => new Quartile(ProductiveQ1, ProductiveQ3);

        public double? PublicQ1 { get; set; }
        public double? PublicQ3 { get; set; }
        public Quartile PublicMinMax => new Quartile(PublicQ1, PublicQ3);
    }
}
