using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FlightsSystem.Core;
using FlightsSystem.Core.BusinessLogic;
using FlightsSystem.Core.DAL;
using FlightsSystem.Core.Login;
using FlightsSystem.WebAPI.Controllers;

namespace FlightsSystem.WebAPI.Filters
{
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var identity = ParseAuthorizationHeader(actionContext);
            if (!identity.HasValue) return;

            LoginService loginService = FlyingCenterSystem.Instance.LoginService;

            if (actionContext.ControllerContext.Controller is AdministratorFacadeController)
            {
                if (loginService.TryAdminLogin(identity.Value.username, identity.Value.password, out var loginToken))
                {
                    actionContext.Request.Properties["LoginToken"] = loginToken;
                }
            }
            else if (actionContext.ControllerContext.Controller is AirlineCompanyFacadeController)
            {
                if (loginService.TryAirlineLogin(identity.Value.username, identity.Value.password, out var loginToken))
                {
                    actionContext.Request.Properties["loginToken"] = loginToken;
                }
            }
            else if (actionContext.ControllerContext.Controller is CustomerFacadeController)
            {
                if (loginService.TryCustomerLogin(identity.Value.username, identity.Value.password, out var loginToken))
                {
                    actionContext.Request.Properties["loginToken"] = loginToken;
                }
            }

            base.OnAuthorization(actionContext);
        }

        protected (string username,string password)? ParseAuthorizationHeader(HttpActionContext actionContext)
        {
            string authHeader = null;
            var auth = actionContext.Request.Headers.Authorization;
            if (auth != null)
                authHeader = auth.Parameter;

            if (string.IsNullOrEmpty(authHeader))
                return null;

            authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

            var tokens = authHeader.Split(new []{':'},2);
            if (tokens.Length < 2)
                return null;

            return (tokens[0], tokens[1]);
        }
    }
}