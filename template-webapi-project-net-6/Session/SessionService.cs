using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SessionInterface;

namespace Session
{
    public class SessionService : ISessionService
    {
        private readonly UserLogged _userLogged;

        public bool AuthenticateAndSaveUser(string authorizationHeader)
        {
            throw new NotImplementedException();
        }

        public UserLogged GetUserLogged()
        {
            return this._userLogged;
        }

        public bool IsValidAuthorizationHeaderFormat(string authorizationHeader)
        {
            throw new NotImplementedException();
        }
    }
}