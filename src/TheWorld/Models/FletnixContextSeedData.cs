using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TheWorld.Models
{
    public class FletnixContextSeedData
    {
        private FletnixContext _context;
        private UserManager<FletnixUser> _userManager; 

        public FletnixContextSeedData(FletnixContext context, UserManager<FletnixUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("walter@fletnix.com") == null)
            {
                var newUser = new FletnixUser()
                {
                    UserName = "walterjansen",
                    Email = "walter@fletnix.com"
                };

                await _userManager.CreateAsync(newUser, "P@ssw0rd!");
            }


//            if (_context.FletnixUser.Any())
//            {
//                
//            }

            // Hier pas wordt alles naar de database geschreven
            _context.SaveChanges();
        }
    }
}
