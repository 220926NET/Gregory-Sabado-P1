using Microsoft.AspNetCore.Mvc;
using Models;
using BusinessLogic;

namespace api_ExpenseReimbursement.Controllers
{
    public class UserController : ControllerBase
    {
        IAuthService authService;
        public UserController(IAuthService authService)
        {
            this.authService = authService;
        }


        [HttpPost]
        [Route("register/")]
        public ActionResult Register(User user)
        {
            int id = authService.Register(user.username, user.password);
            if (id != -1)
                return Created("", $"Registration successful. ID:{id}");
            else
                return Unauthorized("Username already exists");
        }

        [HttpPost]
        [Route("login/")]
        public ActionResult Login(User user)
        {
            User login = authService.Login(user.username, user.password);
            if (login.id != 0)
                return Accepted("", login.manager ? "Manager" : "Employee");
            else
                return Unauthorized("Invalid login");
        }
    }
}
