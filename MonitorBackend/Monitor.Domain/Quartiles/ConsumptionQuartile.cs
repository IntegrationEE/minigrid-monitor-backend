using Monitor.Domain.Base;
using Monitor.Common.Models;

namespace Monitor.Domain.Quartiles
{
    public class ConsumptionQuartile : IQuartile
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

        public double? PeakLoadQ1 { get; set; }
        public double? PeakLoadQ3 { get; set; }
        public Quartile PeakLoadMinMax => new Quartile(PeakLoadQ1, PeakLoadQ3);

        public double? TotalQ1 { get; set; }
        public double? TotalQ3 { get; set; }
        public Quartile TotalMinMax => new Quartile(TotalQ1, TotalQ3);
    }
}
