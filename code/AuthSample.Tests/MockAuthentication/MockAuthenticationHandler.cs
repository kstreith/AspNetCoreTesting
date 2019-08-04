using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AuthSample.Tests.MockAuthentication
{
    public class MockAuthenticationHandler : AuthenticationHandler<MockAuthenticationOptions>
    {
        public MockAuthenticationHandler(IOptionsMonitor<MockAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        /// <summary>
        /// Default value for AuthenticationScheme property in the MockAuthenticationOptions
        /// </summary>
        public const string AuthenticationScheme = "MockAuthenticationScheme";

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var roles = Context.RequestServices.GetRequiredService<MockRoles>();

            if (roles.Roles.Any())
            {
                var claims = roles.Roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
                var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
                var authProperties = new AuthenticationProperties();

                return Task.FromResult(AuthenticateResult.Success(
                    new AuthenticationTicket(
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties,
                        Scheme.Name)));
            }

            return Task.FromResult(AuthenticateResult.Fail("MockAuthentication failed."));
        }
    }
}
