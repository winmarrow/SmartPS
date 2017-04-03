using System;
using SmartPS.WebAPI.Db.Abstract;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartPS.WebAPI.Models.Account
{
    public class AccountModel : IdentityUser<Guid>, 
        IEntityBase<Guid>
    {
        
    }
}

