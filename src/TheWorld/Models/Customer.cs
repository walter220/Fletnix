using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerFeedback = new HashSet<CustomerFeedback>();
            Watchhistory = new HashSet<Watchhistory>();
        }

        public string customer_mail_address { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string paypal_account { get; set; }
        public DateTime? subscription_end { get; set; }
        public DateTime subscription_start { get; set; }

        public virtual ICollection<CustomerFeedback> CustomerFeedback { get; set; }
        public virtual ICollection<Watchhistory> Watchhistory { get; set; }
    }
}
