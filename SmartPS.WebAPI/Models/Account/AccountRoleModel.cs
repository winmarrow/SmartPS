using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartPS.WebAPI.Models.Account
{
    public class AccountRoleModel : IdentityRole<Guid>
    {
        public AccountRoleModel()
        {

        }

        public AccountRoleModel(string roleName)
            : base(roleName)
        {
            
        }
    }
}
