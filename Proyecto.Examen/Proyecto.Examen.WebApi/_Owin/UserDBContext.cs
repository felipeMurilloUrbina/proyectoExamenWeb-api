using EntityFramework.DynamicFilters;
using Microsoft.AspNet.Identity.EntityFramework;
using Proyecto.Examen.WebApi._Interfaces;
using Proyecto.Examen.WebApi._Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Owin
{
    public class UserDBContext: IdentityDbContext<User>
    {
        //new public virtual IDbSet<Role> Roles { get; set; }
        public UserDBContext()
            : base("IdentityConnection", throwIfV1Schema:false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        //public DbSet<Conexion> Conexion { get; set; }
        public static UserDBContext Create()
        {
            return new UserDBContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");

            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
            modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
            new {
                UserId = l.UserId,
                LoginProvider = l.LoginProvider,
                ProviderKey
                = l.ProviderKey
            }).ToTable("UserLogins");
            modelBuilder.Entity<User>().HasMany<IdentityUserRole>((User u) => u.Roles);
            modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("UserRoles");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            EntityTypeConfiguration<Role> roleConfig =
            modelBuilder.Entity<Role>().ToTable("Roles");
            roleConfig.Property((Role r) => r.Name).IsRequired();

            modelBuilder.Entity<Role>()
               .HasMany<Permission>(p => p.Permissions)
               .WithMany(c => c.Roles)
               .Map(cs =>
               {
                   cs.MapLeftKey("RoleId");
                   cs.MapRightKey("PermissionId");
                   cs.ToTable("RolePermissions");
               });
            AddFilters(ref modelBuilder);
        }
        private void AddFilters(ref DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("DeleteLogic", (IObject d) => d.Active, true);
        }

        public void disabled()
        {
            this.DisableFilter("DeleteLogic");
        }
        public void enabled()
        {
            this.EnableFilter("DeleteLogic");
        }
    }
}