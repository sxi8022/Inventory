using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Inventory.WebApi.Models;

namespace Inventory.WebApi.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //for choi : Entity Framwork를 사용하지 않음
            //if (context.UserName == "admin" && context.Password == "admin")
            //{
            //    //쿠키를 만든다. 사용자명과 User권한을 쿠키에 넣어서 보내준다. 클라이언트가 쿠키를 다시 보내주면 사용자명과 권한을 추출해 통과시킨다
            //    var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            //    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, "User"));

            //    context.Validated(oAuthIdentity); //인증 ok

            //    var cookiesIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

            //    AuthenticationProperties properties = CreateProperties(context.UserName);
            //    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //    context.Validated(ticket);

            //    //context.Request.Context.Authentication.SignIn(cookiesIdentity);  //이게 뭐하는 거지? 
            //}

            // var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == "admin" && context.Password == "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Sourav Mondal"));
                context.Validated(identity);

                var cookiesIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                AuthenticationProperties properties = CreateProperties(context.UserName);
                AuthenticationTicket ticket = new AuthenticationTicket(cookiesIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
            else if (context.UserName == "user" && context.Password == "user")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("username", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Suresh Sha"));
                context.Validated(identity);

                var cookiesIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                AuthenticationProperties properties = CreateProperties(context.UserName);
                AuthenticationTicket ticket = new AuthenticationTicket(cookiesIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
            else
            {
                context.SetError("invalid grant", "provided username and password is incorrect");
                return;
            }
            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "사용자 이름 또는 암호가 잘못되었습니다.");
            //    return;
            //}

            //ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
            //   OAuthDefaults.AuthenticationType);
            //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            //    CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName);
            //AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // 리소스 소유자 암호 자격 증명에서 클라이언트 ID가 제공되지 않습니다.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}