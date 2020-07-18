using System.ComponentModel.DataAnnotations.Schema;

namespace FlightsSystem.Core
{
    public class Ticket : IPoco
    {
        public long Id { get; set; }
        [ForeignKey("Flight")]
        public long FlightId { get; set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }

        public Flight Flight { get; set; }
        public Customer Customer { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AirlineCompany company && this.Id == company.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Ticket a, Ticket b)
        {
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Ticket a, Ticket b)
        {
            return !(a == b);
        }
    }
}