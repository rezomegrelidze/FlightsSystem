using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightsSystem.Core;
using FlightsSystem.Core.DAL;
using FlightsSystem.Core.Helpers;
using FlightsSystem.DBGenerator.Models;

namespace FlightsSystem.DBGenerator.Services
{
    // TODO: Finish unimplemented members
    internal class RandomDataGenerator
    {
        private readonly RandomDataSpec _randomDataSpec;
        private IAirlineDAO _airlineDAO;
        private ICustomerDAO _customerDAO;
        private IFlightDAO _flightDAO;
        private IAdministratorDAO _administratorDAO;
        private ITicketDAO _ticketDAO;
        private ICountryDAO _countryDAO;

        public RandomDataGenerator(RandomDataSpec randomDataSpec)
        {
            _randomDataSpec = randomDataSpec;
            InitializeDAOs();
        }

        private void InitializeDAOs()
        {
            _airlineDAO = new AirlineDAOEF();
            _countryDAO = new CountryDAOEF();
            _flightDAO = new FlightDAOEF();
            _administratorDAO = new AdministratorDAOEF();
            _ticketDAO = new TicketDAOEF();
            _customerDAO = new CustomerDAOEF();
        }

        public async Task AddRandomDataToDatabaseAsync()
        {
            await Task.Run(() =>
            {
                var airlineCompanies = GetAirlineCompanies().ToList();
                var customers = GetCustomers().ToList();
                var admins = GetAdministrators().ToList();
                var flights = GetFlights().ToList();
                var tickets = GetTickets().ToList();
                var countries = GetCountries().ToList();
                airlineCompanies.ForEach(airline => _airlineDAO.Add(airline));
                customers.ForEach(customer => _customerDAO.Add(customer));
                admins.ForEach(admin => _administratorDAO.Add(admin));
                flights.ForEach(flight => _flightDAO.Add(flight));
                tickets.ForEach(ticket => _ticketDAO.Add(ticket));
                countries.ForEach(country => _countryDAO.Add(country));
            });
        }

        private IEnumerable<Country> GetCountries()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Ticket> GetTickets()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Flight> GetFlights()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Administrator> GetAdministrators()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<AirlineCompany> GetAirlineCompanies()
        {
            throw new NotImplementedException();
        }

        public async Task ReplaceDatabaseAsync()
        {
            await Task.Run(() =>
            {
                _airlineDAO.Clear();
                _countryDAO.Clear();
                _flightDAO.Clear();
                _administratorDAO.Clear();
                _ticketDAO.Clear();
                _customerDAO.Clear();
            });
            await AddRandomDataToDatabaseAsync();
        }
    }
}