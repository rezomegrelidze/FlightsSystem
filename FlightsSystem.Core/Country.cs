namespace FlightsSystem.Core
{
    public class Country : IPoco
    {
        public long Id { get; set; }
        public string CountryName { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AirlineCompany company && this.Id == company.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Country a, Country b)
        {
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Country a, Country b)
        {
            return !(a == b);
        }
    }
}