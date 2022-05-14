using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SessionInterface
{
    public interface ISessionService
    {
        Task<bool> IsValidAuthorizationHeaderFormat(string authorizationHeader);

        Task<bool> AuthenticateAndSaveUser(string authorizationHeader);

        UserLogged GetUserLogged();
    }
}