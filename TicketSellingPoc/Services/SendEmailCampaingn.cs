using TicketSellingPoc.Entities;
using TicketSellingPoc.Interfaces;

namespace TicketSellingPoc.Services
{
    public class SendEmailCampaingn : ISendEmailCampaingn
    {

        // I recomend to use Redis, Mongo or another database as cache

        private readonly IEventsRepository _eventsRepository;

        public SendEmailCampaingn(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public void AddToEmail(Customer c, Event e)
        {
            Console.WriteLine("The email was sent! ");
        }

        public double GetDistance(string fromCity, string toCity)
        {
            return _eventsRepository.GetDistance(fromCity, toCity); 
        }
    }
}
