using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using FlightsSystem.Core.DAL;

namespace FlightsSystem.Core.Login
{
    public interface ILoginService
    {
        bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token);
        bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token);
        bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token);
    }

    public class LoginService : ILoginService
    {
        private readonly IAirlineDAO _airlineDAO;
        private readonly ICustomerDAO _customerDAO;

        public LoginService(IAirlineDAO airlineDAO,ICustomerDAO customerDAO)
        {
            _airlineDAO = airlineDAO;
            _customerDAO = customerDAO;
        }

        public bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            throw new System.NotImplementedException();
        }

        public bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            throw new System.NotImplementedException();
        }
    }

    

    public class UserNotFoundException : Exception
    {

    }

    public class WrongPasswordException : Exception
    {

    }

    public class Administrator : IUser, IPoco
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Administrator a, Administrator b)
        {
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Administrator a, Administrator b)
        {
            return !(a == b);
        }
    }

    public class LoginToken<T> where T: IUser
    {
        public T User { get; set; }
    }
}