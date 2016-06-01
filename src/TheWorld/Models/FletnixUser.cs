using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheWorld.Models
{
    public class FletnixUser : IdentityUser
    {
        public DateTime FirsteTime { get; set; }
    }
}