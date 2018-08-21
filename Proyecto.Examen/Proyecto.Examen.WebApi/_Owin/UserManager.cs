using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Proyecto.Examen.WebApi._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Owin
{
    /// <summary>
    /// Clase para llevar el control de los usuarios.
    /// </summary>
    public class UserManager: UserManager<User>
    {
        /// <summary>
        /// Constructor para la clase implementando la clase padre.
        /// </summary>
        /// <param name="store"></param>
        public UserManager(IUserStore<User> store)
            : base(store)
        {
        }

        /// <summary>
        /// Metoodo que regresa un instancia de User Manager.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
          var appDbContext = context.Get<UserDBContext>();
            var appUserManager = new UserManager(new UserStore<User>(appDbContext));

            // Configure validation logic for usernames
            appUserManager.UserValidator = new UserValidator<User>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            //appUserManager.EmailService = new EmailService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("Identity Framework"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return appUserManager;
        }
    }
}