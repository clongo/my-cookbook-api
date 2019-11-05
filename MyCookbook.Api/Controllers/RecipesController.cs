using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCookbook.Api.Models;

namespace MyCookbook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;
            return new[] { "test1", "test2" };
        }

        [HttpPut]
        public IEnumerable<string> Put(Recipe recipe)
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;
            return new[] { "test1", "test2" };
        }

        [HttpPost]
        public IEnumerable<string> Post(Recipe recipe)
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;
            return new[] { "test1", "test2" };
        }

        [HttpDelete]
        public IEnumerable<string> Delete(string url)
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;
            return new[] { "test1", "test2" };
        }
    }
}