using TicketSellingPoc.Entities;

namespace TicketSellingPoc.Interfaces;

public interface ISendEmailCampaingn
{
    public double GetDistance(string fromCity, string toCity);
    public void AddToEmail(Customer c, Event e);

}