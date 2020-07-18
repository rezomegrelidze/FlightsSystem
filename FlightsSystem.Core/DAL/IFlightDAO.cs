using System;
using System.Collections.Generic;

namespace FlightsSystem.Core.DAL
{
    public interface IFlightDAO
    {
        IList<Flight> GetAllFlightsVacancy();
        Flight GetFlightById(int id);
        IList<Flight> GetFlightsByCustomer(Customer customer);
        IList<Flight> GetFlightsByDepartureDate(DateTime departureDate);
        IList<Flight> GetFlightsByDestinationCountry(Country destinationCountry);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        IList<Flight> GetFlightsByOriginCountry(Country originCountry);
    }
}