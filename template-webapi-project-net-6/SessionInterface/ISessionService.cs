using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SessionInterface
{
    public interface ISessionService
    {
        bool IsValidAuthorizationHeaderFormat(string authorizationHeader);

        bool AuthenticateAndSaveUser(string authorizationHeader);

        UserLogged GetUserLogged();
    }
}