using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private FletnixContext _context;

        public FeedbackController(FletnixContext context)
        {
            _context = context;
        }

        [HttpGet("api/feedback")]
        public JsonResult Get()
        {
            CustomerFeedback cf = _context.CustomerFeedback.FirstOrDefault();
            return Json(cf);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("api/feedback/create")]
        public JsonResult Create([FromForm] int movieId, string email, int rating, string comment)
        {
            IEnumerable<CustomerFeedback> cf = _context.CustomerFeedback.Where(c => c.customer_mail_address == email).ToList();

            CustomerFeedback f = new CustomerFeedback()
            {
                movie_id = movieId,
                customer_mail_address = email,
                rating = rating,
                comments = comment,
                feedback_date = DateTime.Now.Date
            };

            _context.CustomerFeedback.Add(f);
            _context.SaveChanges();

            return Json(cf);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("api/feedback/delete")]
        public JsonResult Create([FromForm] int movieId, string email)
        {
            CustomerFeedback cf = _context.CustomerFeedback.First(
                c => c.customer_mail_address == email && 
                     c.movie_id == movieId);
            
            _context.CustomerFeedback.Remove(cf);
            _context.SaveChanges();

            return Json(cf);
        }
    }
}
