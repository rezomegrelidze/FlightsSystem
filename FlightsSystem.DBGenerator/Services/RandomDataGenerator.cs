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

            await Task.Run(async () =>
            {
                var airlineCompanies = (await GetAirlineCompanies()).ToList();
                OperationProgress += incr;
                var customers = (await GetCustomers()).ToList();
                OperationProgress += incr;
                var admins = (await GetAdministrators()).ToList();
                OperationProgress += incr;
                var flights = GetFlights().ToList();
                OperationProgress += incr;
                var tickets = GetTickets().ToList();
                OperationProgress += incr;
                var countries = (await GetCountries()).ToList();
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

        private async Task<IEnumerable<Country>> GetCountries()
        {
            var random = new Random();
            var service = new CountryService();
            return (await service.GetAllCountries()).OrderBy(_ => random.Next());
        }

        private IEnumerable<Ticket> GetTickets()
        {
            var rand = new Random();
            var numTickets = rand.Next(_randomDataSpec.MinTicketPerCustomer, _randomDataSpec.MaxTicketPerCustomer);
            for (int i = 0; i < numTickets; i++)
            {
                var ticket = new Ticket()
                {

                };
            }
            throw new NotImplementedException();
        }

        private IEnumerable<Flight> GetFlights()
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<Administrator>> GetAdministrators()
        {
            var admins = new List<Administrator>();
            var randomUserService = new RandomUserService();
            for (int i = 0; i < _randomDataSpec.AdministratorCount; i++)
            {
                var user = await randomUserService.GetRandomUser();
                var admin = new Administrator()
                {
                    Address = user.Address,
                    Password = user.Password,
                    Id = i,
                    UserName = user.Username,
                    PhoneNumber = user.Phone,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                admins.Add(admin);
            }
            return admins;
        }

        private async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = new List<Customer>();
            var customerCount = new Random().Next(_randomDataSpec.MinCustomerCount,_randomDataSpec.MaxCustomerCount);
            var randomUserService = new RandomUserService();
            for (int i = 0; i < customerCount; i++)
            {
                var user = await randomUserService.GetRandomUser();
                var customer = new Customer
                {
                    Address = user.Address,
                    Password = user.Password,
                    Id = i,
                    UserName = user.Username,
                    PhoneNumber = user.Phone,
                    CreditCardNumber = GetRandomCreditCardNumber(),
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                customers.Add(customer);
            }
            return customers;
        }

        private string GetRandomCreditCardNumber()
        {
            var random = new Random();
            return string.Join("", Enumerable.Range(1, 14).Select(x => random.Next(0, 10).ToString()));
        }

        private async Task<IEnumerable<AirlineCompany>> GetAirlineCompanies()
        {
            var rand = new Random();
            var randomUserService = new RandomUserService();
            var countryService = new CountryService();
            var countries = await countryService.GetAllCountries();
            var airlineService = new AirlineService();
            var airlineCompanyCount = _randomDataSpec.AirlineCompanyCount;
            var airlineNames = (await airlineService.GetAirlineNames())
                .OrderBy(x => rand.Next()).Take(airlineCompanyCount);
            var airlines = new List<AirlineCompany>();
            int id = 0;
            foreach (var airlineName in airlineNames)
            {
                var randomUser = await randomUserService.GetRandomUser();
                var airline = new AirlineCompany
                {
                    AirlineName = airlineName,
                    Password = randomUser.Password,
                    Country = countries.OrderBy(x => rand.Next()).Single()
                };
                airline.CountryCode = airline.Country.Id;
                airline.Id = id++;
                airlines.Add(airline);
            }

            return airlines;
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