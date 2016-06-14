using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheWorld.Models
{
    public class FletnixContextSeedData
    {
        private FletnixContext _context;
        private UserManager<FletnixUser> _user;

        public FletnixContextSeedData(FletnixContext context, UserManager<FletnixUser> user)
        {
            _context = context;
            _user = user;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (_context.Roles.Count() != 4)
            {
                _context.Roles.Add(new IdentityRole() { Name = "Admin" });
                _context.Roles.Add(new IdentityRole() { Name = "Customer" });
                _context.Roles.Add(new IdentityRole() { Name = "Financial" });
                _context.Roles.Add(new IdentityRole() { Name = "CEO" });
            }

            if (await _user.FindByEmailAsync("walter@fletnix.com") == null)
            {
                var newUser = new FletnixUser()
                {
                    UserName = "walterjansen",
                    Email = "walter@fletnix.com"
                };
                
                await _user.CreateAsync(newUser, "P@ssw0rd!");
                await _user.AddToRoleAsync(newUser, "Admin");
            }

            if (await _user.FindByEmailAsync("peter@pan.de") == null)
            {
                var newUser = new FletnixUser()
                {
                    UserName = "peterpan",
                    Email = "peter@pan.de"
                };

                await _user.CreateAsync(newUser, "Neverland1!");
                await _user.AddToRoleAsync(newUser, "Customer");
            }

//            if (await _user.FindByEmailAsync("quam@Sed.org") == null)
//            {
//                var newUser = new FletnixUser()
//                {
//                    UserName = "wally",
//                    Email = "quam@Sed.org"
//                };
//               
//                await _user.CreateAsync(newUser, "walter1!");
//                await _user.AddToRoleAsync(newUser, "Customer");
//            }
//
//            if (await _user.FindByEmailAsync("admin@fletnix.com") == null)
//            {
//                var newUser = new FletnixUser()
//                {
//                    UserName = "admin",
//                    Email = "admin@fletnix.com"
//                };
//                
//                await _user.CreateAsync(newUser, "admin1!");
//                await _user.AddToRoleAsync(newUser, "Admin");
//            }
//
//            if (await _user.FindByEmailAsync("ceo@fletnix.com") == null)
//            {
//                var newUser = new FletnixUser()
//                {
//                    UserName = "ceo",
//                    Email = "ceo@fletnix.com"
//                };
//                
//                await _user.CreateAsync(newUser, "ceo1!");
//                await _user.AddToRoleAsync(newUser, "CEO");
//            }
//
//            if (await _user.FindByEmailAsync("financial@fletnix.com") == null)
//            {
//                var newUser = new FletnixUser()
//                {
//                    UserName = "financial",
//                    Email = "financial@fletnix.com"
//                };
//                
//                await _user.CreateAsync(newUser, "financial1!");
//                await _user.AddToRoleAsync(newUser, "Financial");
//            }

            //            if (_context.FletnixUser.Any())
            //            {
            //                
            //            }

            // Hier pas wordt alles naar de database geschreven
            _context.SaveChanges();
        }
    }
}
