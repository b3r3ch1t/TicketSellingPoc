using Bogus;
using TicketSellingPoc.Entities;
using TicketSellingPoc.Interfaces;

namespace TicketSellingPoc.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly List<Event> _events;
        private readonly List<DistanceFromCityToCity> _distanceFromCityToCities;

        public EventsRepository()
        {
           
            try
            {
                _events = new List<Event>();
                _distanceFromCityToCities = new List<DistanceFromCityToCity>();

                var qte = Random.Shared.Next(minValue: 15, maxValue: 30);
                
                for (var i = 0; i < qte; i++)
                {
                    var faker = new Faker("en");
                    var eventName = $"Event-{faker.Name.FullName()}";
                    var eventCity = $"City-{faker.Address.City()}";


                    _events.Add(new Event(name: eventName, city: eventCity));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public List<Event> GetAllEvents()
        {
            return _events;
        }

        public IEnumerable<string> GetAllCitiesFromEvents()
        {
            var cities = _events.Select(x => x.City);
            return cities;
        }

        public void AddEvent(Event e)
        {
            _events.Add(e);
        }

        public Dictionary<string, double> GetClosedCitiesFromCity(string cityFrom)
        {
           
            var results = _events.GroupBy(n => new { n.City})
                .Select(g => new {
                    Key = g.Key.City,
                    Value = GetDistance(cityFrom, g.Key.City   )})
                .OrderBy(o=> o.Value)
                .ToDictionary(x => x.Key, x => x.Value);


            return results;
        }

        public double GetDistance(string fromCity, string toCity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fromCity) || string.IsNullOrWhiteSpace(toCity)) return 0;

                if (fromCity.ToLower() == toCity.ToLower()) return 0;

                var distanceFromCityToCity = _distanceFromCityToCities
                    .FirstOrDefault(x => (x.City1 == fromCity.ToLower() && x.City2 == toCity.ToLower()) ||
                                         (x.City2 == fromCity.ToLower() && x.City1 == toCity.ToLower()));

                if (distanceFromCityToCity != null) return distanceFromCityToCity.Distance;


                var distance = Utils.AlphebiticalDistance(fromCity, toCity);

                distanceFromCityToCity = new DistanceFromCityToCity(fromCity, toCity, distance);

                _distanceFromCityToCities.Add(distanceFromCityToCity);

                return distance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

        }

        public int GetPrice(Event e)
        {
            return (Utils.AlphebiticalDistance(e.City, "") + Utils.AlphebiticalDistance(e.Name, "")) / 10;
        }

    }
}
