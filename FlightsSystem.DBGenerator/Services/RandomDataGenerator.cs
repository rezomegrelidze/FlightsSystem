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
        private double _operationProgress;

        internal delegate void ProgressChanged();

        public event ProgressChanged OnProgressChanged;

        public double OperationProgress
        {
            get => _operationProgress;
            set
            {
                _operationProgress = value;
                OnProgressChanged?.Invoke();
            }
        }

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
            OperationProgress = 0;
            double incr = 100 / 12.0; // 12 because there are 12 lines of blocking code 

            await Task.Run(() =>
            {
                var airlineCompanies = GetAirlineCompanies().ToList();
                OperationProgress += incr;
                var customers = GetCustomers().ToList();
                OperationProgress += incr;
                var admins = GetAdministrators().ToList();
                OperationProgress += incr;
                var flights = GetFlights().ToList();
                OperationProgress += incr;
                var tickets = GetTickets().ToList();
                OperationProgress += incr;
                var countries = GetCountries().ToList();
                OperationProgress += incr;
                airlineCompanies.ForEach(airline => _airlineDAO.Add(airline));
                OperationProgress += incr;
                customers.ForEach(customer => _customerDAO.Add(customer));
                OperationProgress += incr;
                admins.ForEach(admin => _administratorDAO.Add(admin));
                OperationProgress += incr;
                flights.ForEach(flight => _flightDAO.Add(flight));
                OperationProgress += incr;
                tickets.ForEach(ticket => _ticketDAO.Add(ticket));
                OperationProgress += incr;
                countries.ForEach(country => _countryDAO.Add(country));
                OperationProgress += incr;
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
            OperationProgress = 0;
            double incr = 100 / 6.0; // 6 because 6 blocking operations
            await Task.Run(() =>
            {
                _airlineDAO.Clear();
                OperationProgress += incr;
                _countryDAO.Clear();
                OperationProgress += incr;
                _flightDAO.Clear();
                OperationProgress += incr;
                _administratorDAO.Clear();
                OperationProgress += incr;
                _ticketDAO.Clear();
                OperationProgress += incr;
                _customerDAO.Clear();
                OperationProgress += incr;
            });
            await AddRandomDataToDatabaseAsync();
        }
    }
}