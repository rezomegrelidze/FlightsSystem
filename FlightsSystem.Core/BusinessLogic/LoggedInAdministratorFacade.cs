using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.BusinessLogic
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        private readonly LoginService service;

        public LoggedInAdministratorFacade()
        {
            service = new LoginService(_airlineDAO, _customerDAO);
        }

        public void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (LoggedIn(token))
                _airlineDAO.Add(airline);
        }
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany customer)
        {
            if(LoggedIn(token))
                _airlineDAO.Update(customer);
        }

        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if(LoggedIn(token))
                _airlineDAO.Remove(airline);
        }

        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if(LoggedIn(token))
                _customerDAO.Add(customer);
        }

        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            if(LoggedIn(token))
                _customerDAO.Update(customer);
        }

        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if(LoggedIn(token))
                _customerDAO.Remove(customer);
        }
        private bool LoggedIn(LoginToken<Administrator> token)
        {
            
            return service.TryAdminLogin(token.User.UserName, token.User.Password, out token);
        }
    }
}