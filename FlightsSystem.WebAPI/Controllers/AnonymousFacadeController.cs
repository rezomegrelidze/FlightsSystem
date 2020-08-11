using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightsSystem.Core.BusinessLogic;

namespace FlightsSystem.WebAPI.Controllers
{
    public class AnonymousFacadeController : ApiController
    {
        private readonly AnonymousUserFacade _facade;

        public AnonymousFacadeController()
        {
            _facade = new AnonymousUserFacade();
        }

        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            return Json(_facade.GetAllFlights());
        }

        [HttpGet]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            return Json(_facade.GetAllAirlineCompanies());
        }

        [HttpGet]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            return Json(_facade.GetAllFlightsVacancy());
        }

        [HttpGet]
        public IHttpActionResult GetFlightsByOriginCountry(int countryCode)
        {
            return Json(_facade.GetFlightsByOriginCountry(countryCode));
        }

        [HttpGet]
        public IHttpActionResult GetFlightsByDestinationCountry(int countryCode)
        {
            return Json(_facade.GetFlightsByDestinationCountry(countryCode));
        }

        [HttpGet]
        public IHttpActionResult GetFlightsByDepartureDate(DateTime departureDate)
        {
            return Json(_facade.GetFlightsByDepartureDate(departureDate));
        }

        [HttpGet]
        public IHttpActionResult GetFlightsByLandingDate(DateTime landingDate)
        {
            return Json(_facade.GetFlightsByLandingDate(landingDate));
        }
    }
}
