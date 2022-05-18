using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SecurityLogic;
using SessionInterface;

namespace WebApiFilters
{
    internal class AuthenticationFilter : BaseFilter, IAsyncAuthorizationFilter
    {
        private readonly string[] rolesAllowed = new string[0];
        private readonly string[] permissionsAllowed = new string[0];

        public AuthenticationFilter(string[] rolesAllowed, string[] permissionsAllowed)
        {
            this.rolesAllowed = rolesAllowed;
            this.permissionsAllowed = permissionsAllowed;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
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
                var isValidAuthorization = await sessionLogic.IsValidAuthorizationHeaderFormat(authorizationHeader);

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
                    var isValidAuthentication = await sessionLogic.AuthenticateAndSaveUser(authorizationHeader);

                    if (!isValidAuthentication)
                    {
                        context.Result = new UnauthorizedObjectResult(
                        new
                        {
                            Message = "Header 'Authorization' expired",
                        });
                    }
                    else
                    {
                        var securityService = base.GetService<SecurityService>(context);

                        var userLogged = sessionLogic.GetUserLogged();

                        var checkUserPermissions = await securityService.CheckPermissionsAsync(userLogged.Id, this.permissionsAllowed);
                        var checkUserRole = await securityService.CheckRoleAsync(userLogged.Id, this.rolesAllowed);

                        if (!checkUserPermissions || !checkUserRole)
                        {
                            context.Result = new ObjectResult(
                                new
                                {
                                    Message = "The user hasn't the right permissions to access this endpoint."
                                }
                            )
                            {
                                StatusCode = (int)HttpStatusCode.Forbidden
                            };
                        }
                    }
                }
            }
        }
    }
}