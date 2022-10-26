namespace TicketSellingPoc.Entities
{
    public class DistanceFromCityToCity
    {
        public DistanceFromCityToCity(string city1, string city2, double distance)
        {
            if (string.IsNullOrWhiteSpace(city1) ) throw new Exception("city1 is invalid !")  ;

            if (string.IsNullOrWhiteSpace(city2)) throw new Exception("city2 is invalid !");

            if (distance < 0) throw new Exception("Distance is inválid !"); 

            City1 = city1.ToLower();
            City2 = city2.ToLower();
            Distance = distance;
        }

        public string City1 { get; private set;  }
        public string City2 { get; private set; }

        public double Distance { get; private set; }
    }
}
