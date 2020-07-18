using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.DAL
{
    public class FlightsSystemContext : DbContext
    {
        public FlightsSystemContext() : base("FlightsSystem")
        {
            
        }

        public DbSet<AirlineCompany> AirlineCompanies { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

    }

    public class FlightDAOEF : IFlightDAO
    {
        public IList<Flight> GetAllFlightsVacancy()
        {
            throw new NotImplementedException();
        }

        public Flight GetFlightById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetFlightsByDestinationCountry(Country destinationCountry)
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetFlightsByOriginCountry(Country originCountry)
        {
            throw new NotImplementedException();
        }
    }
}