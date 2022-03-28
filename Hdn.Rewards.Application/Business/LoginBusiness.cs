using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using Hdn.Rewards.Domain.Interfaces;
using Hdn.Rewards.Domain.Interfaces.Business;
using Hdn.Rewards.Infra.Configurations;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Hdn.Rewards.Application.Business
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly IUserRepository _userRepository;
        private TokenConfiguration _configuration;

        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public LoginBusiness(IUserRepository userRepository, TokenConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public TokenDto ValidateCredentials(LoginRequest login)
        {
            var userDB = _userRepository.FindByEmail(login.Email);
            var passwithHash = ComputeHash(login.Password, new SHA256CryptoServiceProvider());
            if (userDB == null) return null;

            else
            {
                if (userDB.Password == passwithHash)
                {
                    //generate token

                    //dados que vão ficar dentro do payload
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userDB.FullName),
                        new Claim(JwtRegisteredClaimNames.Email, userDB.Email)
                    };

                    var accessToken = GenerateAccessToken(claims);
                    var refreshToken = GenerateRefreshToken();

                    userDB.RefreshToken = refreshToken;
                    userDB.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

                    UpdateLastAccess(userDB);

                    return new TokenDto(
                           true,
                           createDate.ToString(DATE_FORMAT),
                           expirationDate.ToString(DATE_FORMAT),
                           accessToken,
                           refreshToken
                        );

                }
                else
                {
                    return null;
                    //throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
            }

        }

        public TokenDto ValidadeCredentials(TokenDto token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);

            //var username = principal.Identity.Name;
            var email = principal.Claims.First(c => c.Type == ClaimTypes.Email).Value;

            var user = _userRepository.FindByEmail(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = GenerateAccessToken(principal.Claims);
            refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            UpdateLastAccess(user);

            return new TokenDto(
                   true,
                   createDate.ToString(DATE_FORMAT),
                   expirationDate.ToString(DATE_FORMAT),
                   accessToken,
                   refreshToken
                );

        }


        public void UpdateLastAccess(User user)
        {
            user.LastAccess = DateTime.Now;
            _userRepository.Update(user);

        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;


            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture)) throw new SecurityTokenException("Invalid Token");
            return principal;
        }
    }
}
