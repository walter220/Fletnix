using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public partial class CustomerFeedback
    {
        public int movie_id { get; set; }
        public string customer_mail_address { get; set; }
        public string comments { get; set; }
        public DateTime feedback_date { get; set; }
        public int rating { get; set; }

        public virtual Customer customer_mail_addressNavigation { get; set; }
        public virtual Movie movie { get; set; }
    }
}
