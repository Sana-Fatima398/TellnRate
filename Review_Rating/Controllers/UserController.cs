using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review_Rating.Models;
using Review_Rating.Models.Interfaces;

namespace Review_Rating.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }

        public IActionResult UserList()
        {
            List<AppUser> users = _userRepository.GetAllUsers();

            return View(users);
        }

        [HttpPost]
        public IActionResult Delete(string userId)
        {
  
            string message = _userRepository.Remove(userId);
            TempData["message"] = message;
            return RedirectToAction("UserList"); 
        }

    }
}
