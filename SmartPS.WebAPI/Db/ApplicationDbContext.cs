using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartPS.WebAPI.Models;
using SmartPS.WebAPI.Models.Account;


namespace SmartPS.WebAPI.Db
{
    public class ApplicationDbContext : IdentityDbContext<AccountModel, AccountRoleModel, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccountModel>(e => e.Property(u => u.Id).HasDefaultValueSql("newsequentialid()"));
            builder.Entity<AccountRoleModel>(e => e.Property(ur => ur.Id).HasDefaultValueSql("newsequentialid()"));

        }
    }
}
