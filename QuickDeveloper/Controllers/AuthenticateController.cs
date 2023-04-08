using BenchmarkDotNet.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuickDeveloper.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuickDeveloper.Controllers
{
    public class AuthenticateController : Controller
    {

        private static readonly Lazy<AuthenticateController> lazy = new Lazy<AuthenticateController>(() => new AuthenticateController());

        public static AuthenticateController Instance { get { return lazy.Value; } }

        private HttpRequest httpRequest { get; set; }

        private ClaimsPrincipal claimsPrincipal { get; set; }

        public string RecoveryToken()
        {
            var jwt = httpRequest.Cookies["token"]?.ToString();
            return jwt;
        }

        public TokenValidationParameters ValidationParameters(string token)
        {            
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0asdjas09djsa09djasjdasadajsd09asjd09sajcnzxn"))
            };           
            return validationParameters;
        }

        public static bool VerifyUser(HttpRequest request)
        {
            try
            {
                AuthenticateController.Instance.httpRequest = request;              
                string token = AuthenticateController.Instance.RecoveryToken();
                TokenValidationParameters validationParameters = AuthenticateController.Instance.ValidationParameters(token);

                var tokenHandler = new JwtSecurityTokenHandler();
                AuthenticateController.Instance.claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    // O token expirou
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                // Em caso de erro ao decodificar o token JWT, lança uma exceção
                return false;
            }
        }

        public static bool VerifyUser(HttpRequest request, string role)
        {
            if (!AuthenticateController.VerifyUser(request))
            {
                return false;
            }

            var roleUser = AuthenticateController.Instance.claimsPrincipal.FindFirst(ClaimTypes.Role).Value;

            bool result = roleUser.ToUpper() == role.ToUpper() ? true : false;

            return result;
        }


    }
}
