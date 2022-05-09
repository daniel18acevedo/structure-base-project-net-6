using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SessionInterface;

namespace WebApi.Filters
{
    internal class AuthenticationFilter : BaseFilter, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new ObjectResult(
                    new
                    {
                        Message = "Incorrect format of header 'Authorization'",
                    })
                {
                    StatusCode = (int)HttpStatusCode.Forbidden,
                };
            }
            else
            {
                var sessionLogic = base.GetService<ISessionService>(context);
                var isValidAuthorization = sessionLogic.IsValidAuthorizationHeaderFormat(authorizationHeader);

                if (!isValidAuthorization)
                {
                    context.Result = new UnauthorizedObjectResult(
                    new
                    {
                        Message = "Incorrect format of header 'Authorization'",
                    });
                }
                else
                {
                    var isValidAuthentication = sessionLogic.AuthenticateAndSaveUser(authorizationHeader);

                    if (!isValidAuthentication)
                    {
                        context.Result = new UnauthorizedObjectResult(
                        new
                        {
                            Message = "Header 'Authorization' expired",
                        });
                    }
                }
            }
        }
    }
}