using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using System.Security.Principal;
using System.Security.Claims;

namespace GigHub.Tests.Extensions
{
    public static class ApiControllerExtensions
    {
        public static void MockCurrentUser(this ApiController controller,string userId,string username) 
        {
            var identity = new GenericIdentity(username);
            identity.AddClaims(new List<Claim> {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",userId)
                });

            var principle = new GenericPrincipal(identity, null);
            controller.User = principle;
        }
    }
}
