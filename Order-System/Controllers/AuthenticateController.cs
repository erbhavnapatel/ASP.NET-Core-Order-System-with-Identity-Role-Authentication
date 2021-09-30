using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Order_System.Areas.Identity;
using Order_System.Areas.Services;
using Order_System.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Order_System.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationUser> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> customer, RoleManager<ApplicationUser> supplier, IConfiguration configuration)
        {
            this.userManager = customer;
            this.roleManager = supplier;
            this._configuration = configuration;
        }


        #region Login

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Models.LoginModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    //audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        #endregion


        #region Register

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Models.RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.UserId);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "User already exists"
                    });

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Name
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "User creation failed. Please check user details and try again."
                    });

            return Ok(new Response
            {
                Status = "Success",
                Message = "User created successfully."
            });
        }

        #endregion

        /*
        #region Register-Admin

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterCustomer([FromBody] Models.RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.UserId);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "User already exists"
                    });
            }

            ApplicationUser user = new ApplicationUser()
            {
                //Id = model.CustId,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Name
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "User creation failed. check your data and try again"
                    });
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.CustomerRole))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.CustomerRole));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.SupplierRole))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.SupplierRole));
            }

            return Ok(new Response
            {
                Status = "Success",
                Message = "User created successfully"
            });
        }

        #endregion

        */

    }
}
