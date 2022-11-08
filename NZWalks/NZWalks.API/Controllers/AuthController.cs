using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            //validate incoming request (make sure entry is not empty)

            //check if user is authenticated

            // check username and password
            var isAuthenticated = await userRepository.AuthenticateAsync(
                loginRequest.Username, loginRequest.Password);

            if (isAuthenticated)
            {
                // generate a jwt token
            }

            return BadRequest("Username or password is incorrect.");
        }
    }
}
