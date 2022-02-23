using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentSpy.Core;
using StudentSpy.Core.Requests;
using StudentSpy.Core.Validators;
using StudentSpy.DataManager.Helpers;
using StudentSpy.DataManager.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentSpy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly ILogger<AuthController> logger;
        const int expiresIn = 3;

        public AuthController(UserManager<User> userM,
        RoleManager<IdentityRole> roleM, IConfiguration config,
        ILogger<AuthController> log)
        {
            userManager = userM;
            roleManager = roleM;
            configuration = config;
            logger = log;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest model)
        {
            AuthRequestValidator validator = new AuthRequestValidator();
            var result = validator.Validate(model);
            if (result.IsValid)
            {
                var username = model.Email.Split('@');
                var user = await userManager.FindByNameAsync(username.First());
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                //&& user.EmailConfirmed == true
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    var userId = userManager.GetUserIdAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId.Result),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GenerateToken(authClaims);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        role = userRoles
                    });
                }
            }            
            logger.LogInformation("Unauthorized. Refuse to login.");
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            RegisterRequestValidator validator = new RegisterRequestValidator();
            var validResult = validator.Validate(model);
            if (validResult.IsValid)
            {
                var username = model.Email.Split('@');
                var userExists = await userManager.FindByNameAsync(username.First());
                if (userExists != null)
                {
                    logger.LogError(StatusCodes.Status409Conflict.ToString());
                    return StatusCode(StatusCodes.Status409Conflict);
                }

                User user = new()
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = username.First(),
                    Age = Convert.ToInt32(model.Age),
                    PhotoPath = model.PhotoPath,
                    RegisterDate = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    logger.LogError(StatusCodes.Status500InternalServerError.ToString());
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.User);
                }
                
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Auth",
                    new { userName = user.UserName, code = code },
                    HttpContext.Request.Scheme);

                EmailService emailService = new EmailService();
                emailService.SendEmail(model.Email, "Confirm your account",
                    $"Your account has been created. To login you need to confirm " +
                    $"your accout first. To do that: <a href='{callbackUrl}'>Click here!</a>");

                

                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("confirmed")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userName, string code)
        {
            if (userName == null || code == null)
            {
                throw new KeyNotFoundException("userName or code is not found");
            }
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                logger.LogInformation("Email has been confirmed successfully!");
                return Ok();                
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest model)
        {
            var username = model.Email.Split('@');
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                logger.LogError(StatusCodes.Status409Conflict.ToString());
                return StatusCode(StatusCodes.Status409Conflict);
            }

            User user = new()
            {
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                UserName = username.First(),
                Age = Convert.ToInt32(model.Age),
                PhotoPath = model.PhotoPath,
                RegisterDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                logger.LogError(StatusCodes.Status500InternalServerError.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            logger.LogInformation("OK. Admin has been created successfully.");
            return Ok();
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(expiresIn),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
