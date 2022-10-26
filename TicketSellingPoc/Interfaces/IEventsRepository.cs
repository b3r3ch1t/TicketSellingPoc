using TicketSellingPoc.Entities;

namespace TicketSellingPoc.Interfaces;

public interface IEventsRepository
{
    public List<Event> GetAllEvents();
    public IEnumerable<string> GetAllCitiesFromEvents();
    public void AddEvent(Event e);

    public Dictionary<string, double> GetClosedCitiesFromCity(string cityFrom);

    public int GetPrice(Event e);

    public double GetDistance(string fromCity, string toCity);

}