using Microsoft.AspNetCore.Identity;
using PoshBay.Data.Data;
using PoshBay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Services
{
    public class AppUserRole : IAppUserRoles
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppUserRole(AppDbContext context,
                           RoleManager<IdentityRole> roleManager,
                           UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Roles()
        {
            _context.Database.EnsureCreated();

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole(AppRoles.ADMIN));
            }

            var user = await _userManager.FindByEmailAsync("williamifunanya@gmail.com");
            if (user == null)
            {
                var adminUser = new ApplicationUser
                {
                    FirstName = "Ifunanya",
                    LastName = "Onah",
                    Email = "williamifunanya@gmail.com",
                    UserName = "williamifunanya@gmail.com",
                    PhoneNumber = "08063935581",
                    IsAdmin = true,
                };

                await _userManager.CreateAsync(adminUser, Environment.GetEnvironmentVariable("DefaultAdmin"));
                await _userManager.AddToRoleAsync(adminUser, AppRoles.ADMIN);
            }
        }
    }
}
