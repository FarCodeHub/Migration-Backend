using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Person.Create;
using Application.Queries.Person;
using Application.Queries.User;

namespace Migration.Controllers
{
    public class PersonController : BaseApiController
    {
        private readonly IPersonQueries _personQueries;

        public PersonController(IPersonQueries personQueries)
        {
            _personQueries = personQueries;
        }



        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePersonCommand command) =>
            Ok(await mediator.Send(command));
        [HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            var result =await _personQueries.GetPersons();
            return Ok(result);
    }
    }
}
