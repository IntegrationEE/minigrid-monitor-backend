using System.Linq;
using NetTopologySuite.Geometries;
using Monitor.Domain.Entities;

namespace Monitor.Infrastructure.SeedHelpers
{
    public static class StatesHelper
    {
        public static void Seed(MinigridDbContext context)
        {
            if (!context.States.Any())
            {
                context.States.Add(new State("Abia"));
                context.States.Add(new State("Adamawa"));
                context.States.Add(new State("Akwa Ibom"));
                context.States.Add(new State("Anambra"));
                context.States.Add(new State("Bauchi"));
                context.States.Add(new State("Bayelsa"));
                context.States.Add(new State("Benue"));
                context.States.Add(new State("Borno"));
                context.States.Add(new State("Cross River"));
                context.States.Add(new State("Delta"));
                context.States.Add(new State("Ebonyi"));
                context.States.Add(new State("Edo"));
                context.States.Add(new State("Ekiti"));
                context.States.Add(new State("Enugu"));
                context.States.Add(new State("Federal Capital Territory"));
                context.States.Add(new State("Gombe"));
                context.States.Add(new State("Imo"));
                context.States.Add(new State("Jigawa"));
                context.States.Add(new State("Kaduna"));
                context.States.Add(new State("Kano"));
                context.States.Add(new State("Katsina"));
                context.States.Add(new State("Kebbi"));
                context.States.Add(new State("Kogi"));
                context.States.Add(new State("Kwara"));
                context.States.Add(new State("Lagos"));
                context.States.Add(new State("Nasarawa"));
                context.States.Add(new State("Niger"));
                context.States.Add(new State("Ogun"));
                context.States.Add(new State("Ondo"));
                context.States.Add(new State("Osun"));
                context.States.Add(new State("Oyo"));
                context.States.Add(new State("Plateau"));
                context.States.Add(new State("Rivers"));
                context.States.Add(new State("Sokoto"));
                context.States.Add(new State("Taraba"));
                context.States.Add(new State("Yobe"));
                context.States.Add(new State("Zamfara"));

                context.SaveChanges();
            }

            if (context.States.Any(z => z.BorderLine == null))
            {
                var states = context.States.ToList();

                states.ForEach(state =>
                {
                    state.SetBorderLine(GetCoordinates(state.Name));
                });

                context.SaveChanges();
            }
        }

        private static MultiPolygon GetCoordinates(string name)
        {
            return name switch
            {
                "Abia" => StateCoordinates.Abia,
                "Adamawa" => StateCoordinates.Adamawa,
                "Akwa Ibom" => StateCoordinates.Akwa_Ibom,
                "Anambra" => StateCoordinates.Anambra,
                "Bauchi" => StateCoordinates.Bauchi,
                "Bayelsa" => StateCoordinates.Bayelsa,
                "Benue" => StateCoordinates.Benue,
                "Borno" => StateCoordinates.Borno,
                "Cross River" => StateCoordinates.Cross_River,
                "Delta" => StateCoordinates.Delta,
                "Ebonyi" => StateCoordinates.Ebonyi,
                "Edo" => StateCoordinates.Edo,
                "Ekiti" => StateCoordinates.Ekiti,
                "Enugu" => StateCoordinates.Enugu,
                "Federal Capital Territory" => StateCoordinates.Federal_Capital_Territory,
                "Gombe" => StateCoordinates.Gombe,
                "Imo" => StateCoordinates.Imo,
                "Jigawa" => StateCoordinates.Jigawa,
                "Kaduna" => StateCoordinates.Kaduna,
                "Kano" => StateCoordinates.Kano,
                "Katsina" => StateCoordinates.Katsina,
                "Kebbi" => StateCoordinates.Kebbi,
                "Kogi" => StateCoordinates.Kogi,
                "Kwara" => StateCoordinates.Kwara,
                "Lagos" => StateCoordinates.Lagos,
                "Nasarawa" => StateCoordinates.Nasarawa,
                "Niger" => StateCoordinates.Niger,
                "Ogun" => StateCoordinates.Ogun,
                "Ondo" => StateCoordinates.Ondo,
                "Osun" => StateCoordinates.Osun,
                "Oyo" => StateCoordinates.Oyo,
                "Plateau" => StateCoordinates.Plateau,
                "Rivers" => StateCoordinates.Rivers,
                "Sokoto" => StateCoordinates.Sokoto,
                "Taraba" => StateCoordinates.Taraba,
                "Yobe" => StateCoordinates.Yobe,
                "Zamfara" => StateCoordinates.Zamfara,
                _ => null,
            };
        }
    }
}
