using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerTestAPI2.Models;

namespace SwaggerTestAPI2.Controllers
{
    /// <summary>
    /// We need a hero!
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        /// <summary>
        /// Gets a person
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(201, Type = typeof(Person))]
        [ProducesResponseType(400)]
        [MapToApiVersion("1.0")]
        public Person CreatePerson()
        {
            return new Person
            {
                FirstName = "Bob",
                LastName = "Allen"
            };
        }
        /// <summary>
        /// Gets a person
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(201, Type = typeof(Person))]
        [ProducesResponseType(400)]
        [MapToApiVersion("1.1")]
        public Person CreatePerson2([FromBody]int id)
        {
            return new Person
            {
                FirstName = "Bobby",
                LastName = "Allen"
            };
        }

        /// <summary>
        /// Gets a person
        /// </summary>
        /// <returns></returns>
        [HttpGet("Person")]
        [ProducesResponseType(201, Type = typeof(Person))]
        [ProducesResponseType(400)]
        [MapToApiVersion("1.2")]
        public Person CreatePerson3()
        {
            return new Person
            {
                FirstName = "Bobbie",
                LastName = "Allen"
            };
        }
    }
}