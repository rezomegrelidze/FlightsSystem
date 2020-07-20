using System.Collections.Generic;
using System.Linq;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.BusinessLogic
{
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        private readonly LoginService _service;

        public LoggedInAirlineFacade()
        {
            _service = new LoginService(_airlineDAO,_customerDAO);
        }

        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            if (LoggedIn(token))
            {
                return _ticketDAO.GetAll();
            }

            return null;
        }

        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            if (LoggedIn(token))
            {
                return _flightDAO.GetAll();
            }

            return null;
        }

        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (LoggedIn(token))
            {
                _flightDAO.Remove(flight);
            }
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (LoggedIn(token))
            {
                _flightDAO.Add(flight);
            }
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (LoggedIn(token))
            {
                _flightDAO.Update(flight);
            }
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if (LoggedIn(token))
            {
                _service.ChangePassword(token,oldPassword,newPassword);
            }
        }

        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (LoggedIn(token))
            {
                _airlineDAO.Update(airline);
            }
        }
        private bool LoggedIn(LoginToken<AirlineCompany> token)
        {
            return _service.TryAirlineLogin(token.User.UserName, token.User.Password, out token);
        }
    }
}