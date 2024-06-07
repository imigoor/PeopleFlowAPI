using PeopleHubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace PeopleHubAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "igor" && password == "2005")
            {
                var token = TokenService.GenerateToken(new Model.People());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}
