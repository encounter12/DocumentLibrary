using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DocumentLibrary.Data.Identity;
using DocumentLibrary.DTO.Config;
using DocumentLibrary.DTO.JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DocumentLibrary.API.Admin.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]  
    public class AuthenticateController : ControllerBase
    {  
        private readonly UserManager<ApplicationUser> _userManager;  
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly IConfiguration _configuration;  
  
        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)  
        {  
            _userManager = userManager;  
            _roleManager = roleManager;  
            _configuration = configuration;  
        }  
  
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login([FromBody] LoginModel model)  
        {  
            var user = await _userManager.FindByNameAsync(model.Username);  
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))  
            {  
                var userRoles = await _userManager.GetRolesAsync(user);  
  
                var authClaims = new List<Claim>  
                {  
                    new Claim(ClaimTypes.Name, user.UserName),  
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                };  
  
                foreach (var userRole in userRoles)  
                {  
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
                }
                
                var jwtBearer = new JwtConfig();
                _configuration.GetSection(nameof(JwtConfig)).Bind(jwtBearer);

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearer.IssuerSigningKey));  
  
                var token = new JwtSecurityToken(
                    issuer: jwtBearer.ValidIssuer,
                    audience: jwtBearer.ValidAudience,
                    expires: DateTime.Now.AddHours(3),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );  
  
                return Ok(new  
                {  
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo  
                });  
            }
            
            return Unauthorized();  
        }  
  
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody] RegisterModel model)  
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            
            if (userExists != null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User already exists!" });
            }
                
            ApplicationUser user = new ApplicationUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = string.Join(" ", result.Errors.Select(x => x.Description))
                    });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }
    }  
}