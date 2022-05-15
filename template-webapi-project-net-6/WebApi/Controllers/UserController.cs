using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicAdapter;
using BusinessLogicFilter;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Read;
using Model.Write;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Get([FromQuery] UserFilter paginationFilter)
        {
            var users = await this._userLogicAdapter.GetCollectionAsync(paginationFilter);

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
            var userCreated = await this._userLogicAdapter.CreateAsync<UserDetailInfoModel>(user);

            return CreatedAtRoute("GetUserById", new { userId = userCreated.Id }, userCreated);
        }
    }
}