using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> Get()
            => new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Rodrigo",
                    Age = 25
                },
                
            };
    }
}
