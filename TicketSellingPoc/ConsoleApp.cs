using TicketSellingPoc.Entities;
using TicketSellingPoc.Interfaces;

namespace TicketSellingPoc
{
    public class ConsoleApp
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly ISendEmailCampaingn _sendEmailCampaingn;

        public ConsoleApp(IEventsRepository eventsRepository, ISendEmailCampaingn sendEmailCampaingn)
        {
            _eventsRepository = eventsRepository;
            _sendEmailCampaingn = sendEmailCampaingn;
        }

        public void Run()
        {
            var cities = _eventsRepository.GetAllCitiesFromEvents();

            if (cities == null) throw new Exception("The City can not be empty");


            var cityCustomer = cities.First();

            var customer = new Customer(name: "Mr. Fake", city: cityCustomer);

            var events = new List<Event>();

            var eventsSameCity = _eventsRepository.GetAllEvents()
                .Where(x => x.City.ToLower() == cityCustomer.ToLower())
                .Take(5);

            events.AddRange(eventsSameCity);

            if (events.Count >= 5)
            {
                foreach (var e in events)
                {
                    _sendEmailCampaingn.AddToEmail(customer, e);
                }

                return ;
            }


            var closedCitiesFromCity = _eventsRepository.GetClosedCitiesFromCity(cityCustomer);


            while (events.Count() <5)
            {
                foreach (var c in closedCitiesFromCity)
                {

                    if(events.Count()>=5) continue;
                    
                    var e = _eventsRepository.GetAllEvents()
                        .FirstOrDefault(  x => x.City.ToLower() == c.Key.ToLower());


                    if (e == null || events.Contains(e)) continue; 


                    events.Add(e);
                }
            }

            foreach (var e in events)
            {
                _sendEmailCampaingn.AddToEmail(customer, e);
            }

            return ;
        }
    }
}
