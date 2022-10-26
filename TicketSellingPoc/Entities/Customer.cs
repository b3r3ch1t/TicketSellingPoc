namespace TicketSellingPoc.Entities
{
    public class Customer
    {
        public string Name { get; private set; }
        public string City { get; private set; }

        protected Customer()
        {

        }

        public Customer(string name, string city)
        {
            Name = name;
            City = city;
        }
    }
}
