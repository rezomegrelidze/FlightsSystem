using System.Web.Http;
using FlightsSystem.Core;
using FlightsSystem.Core.BusinessLogic;
using FlightsSystem.Core.Login;
using FlightsSystem.WebAPI.Filters;

namespace FlightsSystem.WebAPI.Controllers
{
    [BasicAuthenticationFilter]
    public class AdministratorFacadeController : ApiController
    {
        private readonly LoggedInAdministratorFacade _facade;

        public AdministratorFacadeController()
        {
            _facade = new LoggedInAdministratorFacade();
        }

        LoginToken<Administrator> GetLoginToken()
        {
            if (Request.Properties.TryGetValue("LoginToken", out var token))
            {
                return (LoginToken<Administrator>) token;
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public IHttpActionResult CreateNewAirline([FromBody] AirlineCompany airline)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.CreateNewAirline(loginToken, airline);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateAirlineDetails([FromBody] AirlineCompany customer)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.UpdateAirlineDetails(loginToken, customer);
            return Ok();
        }
    


        [HttpPost]
        public IHttpActionResult RemoveAirline([FromBody]AirlineCompany airline)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.RemoveAirline(loginToken, airline);
                return Ok();
        }

        [HttpPost]
        public IHttpActionResult CreateNewCustomer([FromBody]Customer customer)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.CreateNewCustomer(loginToken, customer);
                return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomerDetails([FromBody]Customer customer)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.UpdateCustomerDetails(loginToken, customer);
                return Ok();
        }

        [HttpPost]
        public IHttpActionResult RemoveCustomer([FromBody] Customer customer)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.RemoveCustomer(loginToken, customer);
                return Ok();
        }
    }
}