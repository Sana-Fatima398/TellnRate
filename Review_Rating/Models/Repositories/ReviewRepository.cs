using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Review_Rating.Data;
using Review_Rating.Models.Interfaces;
namespace Review_Rating.Models.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public List<Review> GetAll(int id)
        {
            List<Review> temp = _context.Reviews.Where(x => x.ContentID == id).ToList();
            return temp;
        }

        public string Save(Review review) {

            _context.Reviews.Add(review);
            int result = _context.SaveChanges();
            string message = string.Empty;

            if (result == 1)
            {
                message = "New Review Added Successfully";
            }
            else
            {
                message = "Adding Failed";
            }
            return message;
        }

        public AppUser FindUser(string email) {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }

    }
}
