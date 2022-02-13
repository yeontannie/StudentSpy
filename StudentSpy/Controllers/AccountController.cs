using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentSpy.Core;
using StudentSpy.Core.Users;
using StudentSpy.Repositories;

namespace StudentSpy.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        //[HttpPost]
        //[Route("/register")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register([FromBody] User newUser)
        //{
        //    var userExists = await userManager.FindByNameAsync(newUser.Email);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //    var user = new User
        //    {
        //        Name = newUser.Name,
        //        LastName = newUser.LastName,
        //        Email = newUser.Email,
        //        Age = Convert.ToInt32(newUser.Age),
        //        PhotoPath = newUser.PhotoPath,
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    };

        //    IdentityResult result = await userManager.CreateAsync(user, newUser.Password);

        //    // добавляем пользователя
        //    //IdentityResult result = await userManager.CreateAsync(user, newUser.Password);
        //    if (result.Succeeded)
        //    {
        //        // генерация токена для пользователя
        //        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        //        var callbackUrl = Url.Action(
        //            "ConfirmEmail",
        //            "Account",
        //            new { userId = user.Id, code = code },
        //            protocol: HttpContext.Request.Scheme);
        //        EmailService emailService = new EmailService();
        //        await emailService.SendEmailAsync(newUser.Email, "Confirm your account",
        //            $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

        //        return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        //    }
        //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                AddRole(user);
                return Ok();
            }                
            else
                return BadRequest("Error");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Login(AuthRequest loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginDto.Email);
                if (user != null)
                {
                    // проверяем, подтвержден ли email
                    if (!await userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                        return NotFound();
                    }
                }

                var result = await signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return Ok();


            //var user = userRepo.CheckUser(loginDto.Email);
            //if (user == null)
            //{
            //    return Unauthorized("Invalid UserName");
            //}
            //var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(user.Password));
            //var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //for (int i = 0; i < computedHash.Length; i++)
            //{
            //    if (computedHash[i] != user.PasswordHash[i])
            //    {
            //        return Unauthorized("Invalid Password");
            //    }
            //}
            //var login_token = tokenService.CreateToken(user);           
            //return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        public async Task<IActionResult> AddRole(User user)
        {
            var result = await userManager.AddToRoleAsync(user, "Student");
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }            
        }
    }
}
