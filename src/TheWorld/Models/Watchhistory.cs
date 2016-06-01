using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class Watchhistory
    {
        public int movie_id { get; set; }
        public string customer_mail_address { get; set; }
        public DateTime watch_date { get; set; }
        public bool invoiced { get; set; }
        public decimal price { get; set; }

        public virtual Customer customer_mail_addressNavigation { get; set; }
        public virtual Movie movie { get; set; }
    }
}
