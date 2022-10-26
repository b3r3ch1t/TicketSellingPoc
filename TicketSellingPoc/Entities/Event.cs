namespace TicketSellingPoc.Entities
{
    public class Event
    {

        public string Name { get; private set; }
        public string City { get; private set; }

        protected Event()
        {

        }

        public Event(string name, string city)
        {
            Name = name;
            City = city;
        }
    }
}
