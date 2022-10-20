using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api_ReimbursementTicketSystem.Controllers
{
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route ("/register")]
        public async Task<IActionResult> register()
        {
            return BadRequest();
        }
    }
}
