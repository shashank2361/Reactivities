using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


// this Identity should be out of Domain , Domain should be free of any  dependency
namespace Domain
{
    public class AppUser :IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }

        public virtual ICollection <UserActivity> UserActivities { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
// virtual keyword is for lazy loading