using System.Collections.Generic;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.BusinessLogic
{
    public interface ILoggedInCustomerFacade
    {
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight);
        IList<Flight> GetAllMyFlights(LoginToken<Customer> token);
    }

    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        private readonly LoginService _service;

        public LoggedInCustomerFacade()
        {
            _service = new LoginService(_airlineDAO,_customerDAO);
        }

        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (LoggedIn(token))
            {
                _ticketDAO.Remove(ticket);
            }
        }

        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            if (LoggedIn(token))
            {
                var customer = token.User;
                var ticket = new Ticket()
                {
                    Customer = customer,
                    CustomerId = customer.Id,
                    Flight = flight,
                    FlightId = flight.Id
                };
                _ticketDAO.Add(ticket);
                return ticket;
            }

            return null;
        }

        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (LoggedIn(token))
            {
                return _flightDAO.GetFlightsByCustomer(token.User);
            }

            return null;
        }

        private bool LoggedIn(LoginToken<Customer> token)
        {
            return _service.TryCustomerLogin(token.User.UserName, token.User.Password, out token);
        }
    }
}