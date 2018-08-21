using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Proyecto.Examen.WebApi._Formats;
using Proyecto.Examen.WebApi._Owin;
using Proyecto.Examen.WebApi._Providers;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Proyecto.Examen.WebApi.Startup))]

namespace Proyecto.Examen.WebApi
{
    public class Startup
    {
        public static string issuer = "http://proyecto-examen.com.mx";
        public static string clave = "099153c2625149bc8ecb3e85e03f0022";
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            // Web API routes
            config.MapHttpAttributeRoutes();
            ConfigureOAuth(app);
            ConfigureOAuth2(app);

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            app.UseWebApi(config);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(UserDBContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<RoleManager>(RoleManager.Create);
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(issuer)
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }
        public void ConfigureOAuth2(IAppBuilder app)
        {
            var audience = "099153c2625149bc8ecb3e85e03f0022";
            var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    },
                    Provider = new OAuthBearerAuthenticationProvider
                    {
                        OnValidateIdentity = context =>
                        {
                            context.Ticket.Identity.AddClaim(new System.Security.Claims.Claim("newCustomClaim", "newValue"));
                            return Task.FromResult<object>(null);
                        }
                    }
                });

        }
    }
}
