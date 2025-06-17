using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using UMS.Data;
using UMS.Models;
using UMS.Models.Entities;

namespace UMS.Services
{
    public class JWTService(ApplicationDbContext dbContext,IConfiguration configuration)
    {
        public LoginResponseModel AuthenticateLogin(LoginRequestModel request) 
        {
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponseModel()
                {
                    UserName = "Not Found",
                    AccessToken = "Not Found",
                    Email = "Not Found"
                };
            } 
            var existingEmployee = dbContext.Employees
                .FirstOrDefault(u=>u.UserName == request.UserName && u.Password == request.Password);

            var existingManager = dbContext.Managers
                .FirstOrDefault(u => u.UserName == request.UserName && u.Password == request.Password);
            if (existingEmployee == null && existingManager == null) 
            {
                return new LoginResponseModel();
            }
            if (existingEmployee != null)
            {
                var token = GenerateAccessToken(existingEmployee.Id, existingEmployee.FullName,existingEmployee.Email, existingEmployee.UserName, UMS.Enums.Roles.Employee.ToString());
                return token;
            }
            else if (existingManager != null)
            {
                var token = GenerateAccessToken(existingManager.Id, existingManager.FullName,ConstantValues.MANAGER_DEFAULT_EMAIL, existingManager.UserName, UMS.Enums.Roles.Manager.ToString());
                return token;
            }
            else
            {
                return new LoginResponseModel()
                {
                    UserName = "Not Found",
                };
            }

        }
        public LoginResponseModel GenerateAccessToken(int Id,string FullName,string Email,string UserName,string Role)
        {
            var issuer = configuration["JWTConfig:Issuer"];
            var audience = configuration["JWTConfig:Audience"];
            var key = configuration["JWTConfig:Key"];
            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("JWT configuration values are not set properly.");
            }
            var tokenValidityMins = configuration.GetValue<int>("JWTConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(tokenValidityMins);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                new Claim(JwtRegisteredClaimNames.Name, UserName),
                new Claim(JwtRegisteredClaimNames.Email,Email),
                new Claim(ClaimTypes.Role,Role)
                ]),
                Issuer = issuer,
                Audience = audience,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseModel
            {
                RefreshToken = GenerateRefreshToken(Id),
                UserName = UserName,
                AccessToken = accessToken,
                Expiration = tokenExpiryTimeStamp
            };
        }

        public string GenerateRefreshToken(int userId)
        {
            return "";
            //var tokenId = Guid.NewGuid().ToString();
            //var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            //int expiryDays = 5;
            //var expiryDate = DateTime.Now.AddDays(expiryDays);

            //var token = new RefreshToken
            //{
            //    Token = tokenId,
            //    RefreshUserToken = refreshToken,
            //    UserId = userId
            //};
            //dbContext.RefreshTokens.Add(token);
            //dbContext.SaveChanges();
            //return refreshToken;
        }
    }
}
