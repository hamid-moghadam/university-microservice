using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Identity.API.Data
{
    public class ApplicationDbContextSeed
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        public async Task SeedAsync(ApplicationDbContext context, IWebHostEnvironment env,
            ILogger<ApplicationDbContextSeed> logger, int? retry = 0)
        {
            int retryForAvaiability = retry.Value;

            try
            {
                if (!context.Users.Any())
                {
                    await context.Users.AddRangeAsync(GetDefaultUser());

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvaiability < 10)
                {
                    retryForAvaiability++;

                    logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}",
                        nameof(ApplicationDbContext));

                    await SeedAsync(context, env, logger, retryForAvaiability);
                }
            }
        }

        private IEnumerable<ApplicationUser> GetDefaultUser()
        {
            var user =
                new ApplicationUser()
                {
                    Email = "hamid.rm9877@gmail.com",
                    Id = Guid.NewGuid().ToString(),
                    LastName = "moghadam",
                    FirstName = "hamid",
                    PhoneNumber = "09393615326",
                    UserName = "admin",
                    NormalizedEmail = "HAMID.RM9877@GMAIL.COM",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                };

            user.PasswordHash = _passwordHasher.HashPassword(user, "Pass@word1");

            return new List<ApplicationUser>()
            {
                user
            };
        }
    }
}