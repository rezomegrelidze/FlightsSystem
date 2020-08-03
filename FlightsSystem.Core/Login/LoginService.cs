using System;
using System.Linq;
using FlightsSystem.Core.DAL;

namespace FlightsSystem.Core.Login
{
    public class LoginService : ILoginService
    {
        private FlightsSystemContext db; 
        private readonly IAirlineDAO _airlineDAO;
        private readonly ICustomerDAO _customerDAO;

        public LoginService(IAirlineDAO airlineDAO,ICustomerDAO customerDAO)
        {
            _airlineDAO = airlineDAO;
            _customerDAO = customerDAO;
            db = new FlightsSystemContext(); // make DAO for LoginService ...
        }

        public bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            var admin = db.Administrators.SingleOrDefault(a => a.UserName == userName);
            if (admin != null)
            {
                if (admin.Password == password)
                {
                    token = new LoginToken<Administrator> {User = admin};
                    return true;
                }
                else
                {
                    throw new WrongPasswordException();
                }
            }
            throw new UserNotFoundException($"username: {userName} of type administrator could not be found");
        }

        public bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            var customer = db.Customers.SingleOrDefault(a => a.UserName == userName);
            if (customer != null)
            {
                if (customer.Password == password)
                {
                    token = new LoginToken<Customer> { User = customer };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException();
                }
            }
            throw new UserNotFoundException($"userName: {userName} of type customer could not be found"); // make exceptions more verbose
        }

        public bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            var airline = db.AirlineCompanies.SingleOrDefault(a => a.UserName == userName);
            if (airline != null)
            {
                if (airline.Password == password)
                {
                    token = new LoginToken<AirlineCompany> { User = airline };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException();
                }
            }
            throw new UserNotFoundException($"userName: {userName} of type airlinecompany could not be found");
        }

        public void ChangePassword<T>(LoginToken<T> token,string oldPassword, string newPassword) where T:IUser
        {
            if (token is LoginToken<AirlineCompany>)
            {
                var airline = _airlineDAO.Get((token.User as AirlineCompany).Id);
                if (airline.Password == oldPassword)
                {
                    airline.Password = newPassword;
                    _airlineDAO.Update(airline);
                }
                else
                {
                    throw new InvalidOperationException("Incorrect old password provided!");
                }
            }
        }
    }
}