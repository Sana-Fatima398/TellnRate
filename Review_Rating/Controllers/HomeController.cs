using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Review_Rating.Data;
using Review_Rating.Models;
using Review_Rating.Models.Repositories;
using System.Diagnostics;

namespace Review_Rating.Controllers
{
    [Authorize(Policy = "ForAdminOnly")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToAction("GetContent", "Content");
        }

        public IActionResult AdminDashboard()
        {

            return View();
        }
    



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
