using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.User.Create;
using Application.Queries.User;

namespace Migration.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserQueries _userQueries;

        public UserController(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }
       

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand command) =>
            Ok(await mediator.Send(command));

        [HttpGet]
        public async Task<IActionResult> GetUser(string userName , string password)
        {
            var result = await _userQueries.GetUser(userName, password);
            return Ok(result);
        }
    }
 
}
