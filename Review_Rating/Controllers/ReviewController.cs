using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Review_Rating.Models;
using Review_Rating.Models.Interfaces;
using System.Security.Claims;

namespace Review_Rating.Controllers
{
    [Authorize(Policy = "ForUsersOnly")]
    public class ReviewController : Controller
    {
        private IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IActionResult AddReview(int id, string name)
        {
            ViewBag.Name = name;
            ViewBag.Id = id;
            return View();
        }

        
     
        public IActionResult SaveReview(float story, float acting,  float music, float rewatch,
                                        string headline, string description, int contentId) {


            Review reviewobj = new Review();
            reviewobj.Story = story; 
            reviewobj.Acting = acting;
            reviewobj.Music = music;
            reviewobj.RewatchValue = rewatch;
            reviewobj.Headline = headline;
            reviewobj.Explanation = description;
            reviewobj.ContentID = contentId;
           
            string email = User.FindFirstValue(ClaimTypes.Email);

            var user = _reviewRepository.FindUser(email);
            reviewobj.UserName = user.UserName;
            reviewobj.UserId = user.Id;
            string message = _reviewRepository.Save(reviewobj);
            TempData["Message"] = message;

            return RedirectToAction("AddReview");
        }
        public IActionResult ReviewView(int id, string name)
        {
            ViewBag.Name = name;
            ViewBag.Id = id;
            List<Review> reviews = _reviewRepository.GetAll(id);
            ViewBag.Name = name;
            ViewBag.Id = id;
            return View(reviews);
        }
     
    }
}
