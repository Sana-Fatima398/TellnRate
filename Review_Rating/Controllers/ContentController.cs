using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Review_Rating.Models.Interfaces;
using Review_Rating.Models.Repositories;
using Review_Rating.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Review_Rating.Controllers
{
    
    public class ContentController : Controller
    {
        
        private readonly IContentRepository _contentRepository;

        public ContentController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

            

        [Authorize(Policy = "ForAdminOnly")]
        public IActionResult AddContent()
        {
            return View();
        }

        [Authorize(Policy = "ForAdminOnly")]
        public IActionResult DeleteContent(string contentId)
        {
            string message = _contentRepository.DeleteContent(contentId);

            TempData["Message"] = message;
            return RedirectToAction("ContentList");
        }

        [Authorize(Policy = "ForAdminOnly")]
        public IActionResult ContentList()
        {
            List<Content> data = _contentRepository.GetAll();
            return View(data);
        }

        [Authorize(Policy = "ForAdminOnly")]
        public IActionResult ToAdmin()
        {
            Dictionary<string, int> data = _contentRepository.GetCounts();
            return View("Views/Home/AdminDashboard.cshtml", data);
        }


        [Authorize(Policy = "ForAdminOnly")]
        public IActionResult SaveContent(string contentName, string contentDescription, string contentType,
                                        int contentReleaseYear, string contentGenre, string contentTrailer,
                                        string contentPoster)
        {


            Content temp = new Content();
            temp.Name = contentName;
            temp.PlotSummary = contentDescription;
            temp.Type = contentType;
            temp.ReleaseYear = contentReleaseYear;
            temp.Genre = contentGenre;
            temp.TrailerLink = contentTrailer;
            temp.ImageLink = contentPoster;

            string message = _contentRepository.AddContent(temp);

            TempData["Message"] = message;
            return RedirectToAction("AddContent");


        }


        [Authorize(Policy = "ForAdminOnly")]
        public IActionResult UpdateContent(int id)
        {
            Content content = _contentRepository.GetByID(id);
            return View(content);
        }


        [Authorize(Policy = "ForAdminOnly")]
        public IActionResult Change(Content content)
        {
            _contentRepository.UpdateContent(content);
            Content data = _contentRepository.GetByID(content.ID);
            return RedirectToAction("Display", data);

        }







        [AllowAnonymous]
        public IActionResult BrowseMovies()
        {
            return View();
        }


        [AllowAnonymous]
        public ViewResult Display(string ID)
        {  
            int id = int.Parse(ID);
            Content data = _contentRepository.GetByID(id);
            data.Comments = _contentRepository.GetComments(id);
            data.Rating = _contentRepository.GetRatings(id);
            data.Reviews = _contentRepository.GetReviews(id);
            return View(data);
        }

        [AllowAnonymous]
        public IActionResult GetContent()
        {
            
            List<Content> data = _contentRepository.GetAll();
            return View("Views/Home/Index.cshtml", data);
            

        }

        [AllowAnonymous]
        public ActionResult FetchData(string inputData, string contentType, string contentGenre, string contentReleaseYear)
        {
            // Filter logic here
            List<Content> filteredData = _contentRepository.GetSearchData(inputData, contentType, contentGenre, contentReleaseYear);
            return PartialView("_Result", filteredData);
        }





        [Authorize(Policy = "ForUsersOnly")]
        public IActionResult Rate(string contentId, int rating)
        {
            _contentRepository.AddRating(contentId, rating);
            Content content = _contentRepository.GetByID(int.Parse(contentId));
            return View("Views/Content/Display.cshtml", content);
        }


        [Authorize(Policy = "ForUsersOnly")]
        public IActionResult Favorite() {
            string email = User.FindFirstValue(ClaimTypes.Email);
            List<Content> favs = _contentRepository.GetFavs(email);
            return View(favs);
        }

        [Authorize(Policy = "ForUsersOnly")]
        public IActionResult AddToFavorite(string contentId) {

            string email = User.FindFirstValue(ClaimTypes.Email);

            int id = int.Parse(contentId);

            _contentRepository.AddtoFav(id, email);

            return RedirectToAction("Favorite");
        
        }

        [Authorize(Policy = "ForUsersOnly")]
        public IActionResult RemoveFav(int ID) {
        
            string email = User.FindFirstValue(ClaimTypes.Email);
            
            _contentRepository.RemoveFav(ID, email);
            return RedirectToAction("Favorite");
        }
    }
}
