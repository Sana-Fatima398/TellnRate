using Microsoft.EntityFrameworkCore;
using Review_Rating.Data;
using Review_Rating.Models.Interfaces;
namespace Review_Rating.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) { 
            _context = context;
        
        }
        public List<AppUser> GetAllUsers()
        {
            List<AppUser> users = _context.Users.ToList();
            return users;
        }

        public string Remove(string id) {
            int result = 0;
            var user = _context.Users.Find(id);
            if (user != null) {
                if (user.Email != "admin@tellnrate.com") {
                    var userComments = _context.Comments.Where(c => c.UserId == id).ToList();
                    _context.Comments.RemoveRange(userComments);

                    var userReviews = _context.Reviews.Where(c => c.UserId == id).ToList();
                    _context.Reviews.RemoveRange(userReviews);
                    _context.Users.Remove(user);
                    result = _context.SaveChanges();
                }
                
            }
            Console.WriteLine(result); 
            if (result == 1)
            {
                return $"User {id} is suceesfully deleted";
            }
            else {
                return $"Unable to delete the User {id}";
            }
        }
    }
}
