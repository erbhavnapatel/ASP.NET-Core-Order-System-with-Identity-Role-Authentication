//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Order_System.Identity;
//using Order_System.Models;
//using Order_System.Repositories.Authentication;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Order_System.Controllers
//{
//    [Route("[controller]/[action]")]
//    [ApiController]
//    public class AccountController : ControllerBase
//    {
//        private readonly IConfiguration _config;
//        private readonly ITokenRepository _tokenRepository;
//        private readonly IUserRepository _userRepository;

//        public AccountController(IConfiguration config, ITokenRepository tokenRepository, IUserRepository userRepository)
//        {
//            _config = config;
//            _tokenRepository = tokenRepository;
//            _userRepository = userRepository;
//        }
        

//        [AllowAnonymous]
//        //[Route("login")]
//        [HttpPost]
//        public IActionResult Login(UserModel userModel)
//        {
//            if (string.IsNullOrEmpty(userModel.UserId) || string.IsNullOrEmpty(userModel.Password))
//            {
//                return (RedirectToAction("Error"));
//            }
//            IActionResult response = Unauthorized();
//            var validUser = GetUser(userModel);

//            if (validUser != null)
//            {
//                var generatedToken = _tokenRepository.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
//                if (generatedToken != null)
//                {
//                    return RedirectToAction("MainWindow");
//                }
//                else
//                {
//                    return (RedirectToAction("Error"));
//                }
//            }
//            else
//            {
//                return (RedirectToAction("Error"));
//            }
//        }

//        private User GetUser(UserModel userModel)
//        {
//            // Write your code here to authenticate the user     
//            return _userRepository.GetUser(userModel);
//        }

//        [Authorize]
//        //[Route("mainwindow")]
//        [HttpGet]
//        public IActionResult MainWindow()
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
//            _config["Jwt:Issuer"],
//            null,
//            expires: DateTime.Now.AddMinutes(120),
//            signingCredentials: credentials);

//            string tokengen = new JwtSecurityTokenHandler().WriteToken(token);

//            if (!_tokenRepository.ValidateToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
//            {
//                return BadRequest();
//            }

//            return Ok(new { token = token });
//        }

//        public IActionResult Error()
//        {
//            return Unauthorized();
//        }

//        private string BuildMessage(string stringToSplit, int chunkSize)
//        {
//            var data = Enumerable.Range(0, stringToSplit.Length / chunkSize).Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));
//            string result = "The generated token is:";
//            foreach (string str in data)
//            {
//                result += Environment.NewLine + str;
//            }
//            return result;
//        }
//    }
//}