using System.Web.Http;
using FlightsSystem.Core;
using FlightsSystem.Core.BusinessLogic;
using FlightsSystem.Core.Login;
using FlightsSystem.WebAPI.Filters;

namespace FlightsSystem.WebAPI.Controllers
{
    [BasicAuthenticationFilter]
    public class CustomerFacadeController : ApiController
    {
        private LoggedInCustomerFacade _facade;

        public CustomerFacadeController()
        {
            _facade = new LoggedInCustomerFacade();
        }

        LoginToken<Customer> GetLoginToken()
        {
            if (Request.Properties.TryGetValue("LoginToken", out var token))
            {
                return (LoginToken<Customer>)token;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllMyFlights()
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            return Json(_facade.GetAllMyFlights(loginToken));
        }

        [HttpPost]
        public IHttpActionResult PurchaseTicket([FromBody] Flight flight)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.PurchaseTicket(loginToken,flight);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult CancelTicket([FromBody] Ticket ticket)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.CancelTicket(loginToken, ticket);
            return Ok();
        }
    }
}