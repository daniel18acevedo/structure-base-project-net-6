using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogicAdapter;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Read;
using Model.Write;
using WebApi.Filters;

[assembly: ApiController]
namespace WebApi.Controllers
{
    [Route("users")]
    //[AuthenticationFilter]
    public class UserController : Controller
    {
        private readonly UserLogicAdapter _userLogicAdapter;

        public UserController(UserLogicAdapter userLogicAdapter)
        {
            this._userLogicAdapter = userLogicAdapter;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]PaginationFilter paginationFilter)
        {
            var users = this._userLogicAdapter.GetCollection(paginationFilter);

            return Ok(users);
        }

        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            var userCreated = this._userLogicAdapter.Create<UserDetailInfoModel>(user);
            
            return CreatedAtRoute("GetUserById", new { userId = userCreated.Id }, userCreated);
        }
    }
}