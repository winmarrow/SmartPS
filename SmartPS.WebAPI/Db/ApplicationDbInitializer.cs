using System;
using System.Collections.Generic;
using System.Linq;
using SmartPS.WebAPI.Models;
using SmartPS.WebAPI.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartPS.WebAPI.Db
{
    public static class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            var rand = new Random(DateTime.Now.Millisecond);

            try
            {
                //context.Database.EnsureDeleted();

                context.Database.EnsureCreated();

                // -- Init there --
                


                context.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }

        }
    }
}
