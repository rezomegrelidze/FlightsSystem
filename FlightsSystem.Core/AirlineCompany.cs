using System.ComponentModel.DataAnnotations.Schema;

namespace FlightsSystem.Core
{
    public class AirlineCompany : IPoco,IUser
    {
        public long Id { get; set; }
        public string AirlineName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [ForeignKey("Country")]
        public long CountryCode { get; set; }

        public Country Country { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AirlineCompany company && this.Id == company.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(AirlineCompany a, AirlineCompany b)
        {
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(AirlineCompany a, AirlineCompany b)
        {
            return !(a == b);
        }
    }
}