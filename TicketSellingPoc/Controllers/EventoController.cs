using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using TicketSellingPoc.Entities;
using TicketSellingPoc.Interfaces;

namespace TicketSellingPoc.Controllers
{
    public class EventoController : Controller
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly ISendEmailCampaingn _sendEmailCampaingn;

        public EventoController(IEventsRepository eventsRepository, ISendEmailCampaingn sendEmailCampaingn)
        {
            _eventsRepository = eventsRepository;
            _sendEmailCampaingn = sendEmailCampaingn;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cities = _eventsRepository.GetAllCitiesFromEvents();

            if (cities == null) throw new Exception("The City can not be empty");


            var cityCustomer = cities.First();

            var customer = new Customer(name: "Mr. Fake", city: cityCustomer);

            var events = new List<Event>();

            var eventsSameCity = _eventsRepository.GetAllEvents()
                .Where(x => x.City == cityCustomer.ToLower())
                .Take(5); 

            events.AddRange(eventsSameCity);

            if (events.Count > 5)
            {
                foreach (var  e in events)
                {
                    _sendEmailCampaingn.AddToEmail(customer, e );
                }

                return Ok(); 
            }


            var closedCitiesFromCity = _eventsRepository.GetClosedCitiesFromCity(cityCustomer); 


            while (events.Count() <5)
            {
                foreach (var c in closedCitiesFromCity)
                {
                    var e = _eventsRepository.GetAllEvents()
                        .Where(x => x.City == cityCustomer.ToLower()); 

                    events.AddRange(e);
                }
            }

            foreach (var e in events)
            {
                _sendEmailCampaingn.AddToEmail(customer, e);
            }

            return Ok();

        }
    }
}
