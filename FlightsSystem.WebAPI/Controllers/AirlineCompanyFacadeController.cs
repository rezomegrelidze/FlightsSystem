using System.Web.Http;
using FlightsSystem.Core;
using FlightsSystem.Core.BusinessLogic;
using FlightsSystem.Core.Login;
using FlightsSystem.WebAPI.Filters;

namespace FlightsSystem.WebAPI.Controllers
{
    [BasicAuthenticationFilter]
    public class AirlineCompanyFacadeController : ApiController
    {
        private LoggedInAirlineFacade _facade;

        public AirlineCompanyFacadeController()
        {
            _facade = new LoggedInAirlineFacade();
        }

        LoginToken<AirlineCompany> GetLoginToken()
        {
            if (Request.Properties.TryGetValue("LoginToken", out var token))
            {
                return (LoginToken<AirlineCompany>)token;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllTickets()
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            return Json(_facade.GetAllTickets(loginToken));
        }

        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            return Json(_facade.GetAllFlights(loginToken));
        }

        [HttpPost]
        public IHttpActionResult CancelFlight([FromBody]Flight flight)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.CancelFlight(loginToken,flight);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult CreateFlight([FromBody]Flight flight)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.CreateFlight(loginToken, flight);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateFlight([FromBody]Flight flight)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.UpdateFlight(loginToken, flight);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ChangeMyPassword([FromBody]string oldPassword,[FromBody] string newPassword)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.ChangeMyPassword(loginToken, oldPassword,newPassword);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ModifyAirlineDetails([FromBody]AirlineCompany airline)
        {
            var loginToken = GetLoginToken();
            if (loginToken == null) return Unauthorized();
            _facade.ModifyAirlineDetails(loginToken, airline);
            return Ok();
        }
    }
}