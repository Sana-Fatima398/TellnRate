using Review_Rating.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Review_Rating.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Review_Rating.Models.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ApplicationDbContext _context;

        public ContentRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }



        public List<Content> GetAll()
        {


            List<Content> temp = _context.Contents.ToList();

            return temp;
        }

        public Content GetByID(int id) {
           Content content =  _context.Contents.Find(id);
           return content;
        }


        public void AddRating(string contentId, int rating) {
            Rating data = new Rating();
            data.ContentId = int.Parse(contentId);
           
            data.Value = rating;

            //_context.Ratings.Add(data);
            _context.SaveChanges();
        }


        public string AddContent(Content content)
        {

            _context.Contents.Add(content);
            int result = _context.SaveChanges();
            string message = string.Empty;

            if (result == 1)
            {
                message = "New Content Added Successfully";
            }
            else
            {
                message = "Adding Failed";
            }
            return message;
        }
        public Dictionary<string, int> GetCounts() { 
            Dictionary<string, int> counts = new Dictionary<string, int>();
            counts["contents"] = _context.Contents.Count();
            counts["reviews"] = _context.Reviews.Count();
            counts["users"] = _context.Users.Count();
            counts["ratings"] = _context.Reviews.Count() * 4;
            return counts;
        }

        public void UpdateContent(Content content)
        {
            var temp = _context.Contents.Find(content.ID);
            if (temp == null)
            {
                throw new Exception($"Content with ID {content.ID} not found.");
                
            }
            temp.Name = content.Name;
            temp.PlotSummary = content.PlotSummary;
            temp.Type = content.Type;
            temp.ReleaseYear = temp.ReleaseYear;
            temp.Genre = content.Genre;
            temp.ImageLink = content.ImageLink;
            temp.TrailerLink = content.TrailerLink;

            _context.SaveChanges();
        }

        public string DeleteContent(string id)
        {
            int ID = int.Parse(id);
            var content = _context.Contents.Find(ID);
            _context.Contents.Remove(content);
            int result = _context.SaveChanges();
            string message = string.Empty;

            if (result == 1)
            {
                message = "Content Deleted";
            }
            else
            {
                message = "Cant Delete the Content";
            }
            return message;

        }






        public List<Content> GetSearchData(string inputData, string contentType, string contentGenre, string contentReleaseYear)
        {

            List<Content> contents = _context.Contents.ToList();

            if (!string.IsNullOrWhiteSpace(inputData))
            {
                contents = contents.Where(c => c.Name.Contains(inputData, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(contentType) && contentType != "All")
            {
                contents = contents.Where(c => c.Type == contentType).ToList();
            }

            if (!string.IsNullOrEmpty(contentGenre) && contentGenre != "All")
            {
                contents = contents.Where(c => c.Genre == contentGenre).ToList();
            }

            if (!string.IsNullOrEmpty(contentReleaseYear) && contentReleaseYear != "All")
            {
                if (contentReleaseYear == "2008")
                {
                    contents = contents.Where(c => c.ReleaseYear <= 2008).ToList();
                }
                else if (int.TryParse(contentReleaseYear, out int year))
                {
                    contents = contents.Where(c => c.ReleaseYear == year).ToList();
                }
            }

            return contents;

        }



        public List<Content> GetFavs(string email) {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            string userId = user.Id;

            var result = _context.Favorites.Where(c => c.UserId == userId)
                                            .Select(c => c.ContentId).ToList();
            List<Content> data = _context.Contents.Where(c => result.Contains(c.ID)).ToList();
            return data;
        }


        public void AddtoFav(int id, string email) {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
           

            Favorite fav = new Favorite();
            fav.ContentId = id;
            fav.UserId = user.Id;

            var check = _context.Favorites.Where(c => c.ContentId == id && c.UserId == user.Id).ToList();

            if (!check.Any()) {
                _context.Favorites.Add(fav);
                _context.SaveChanges();
            }
        }

        public void RemoveFav(int ID, string email) {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            string userId = user.Id;

            var data = _context.Favorites.FirstOrDefault(c => c.ContentId == ID && c.UserId == userId);
            _context.Favorites.Remove(data);
            _context.SaveChanges();
        }




        public ICollection<Comment> GetComments(int id) {
            ICollection <Comment> comments = _context.Comments.Where(c => c.ContentID == id).ToList();
      
            return comments;
        
        }
        public Rating GetRatings(int id) {
             int ratings = int.Parse(_context.Ratings.Where(r => r.ContentId == id)
                                              .Sum(v => v.Value).ToString());

               int count = _context.Ratings.Where(r => r.ContentId == id)
                                              .Count();

               int result;
               if (count == 0)
               {
                   result = 0;
               }
               else {
                   result = ratings / count;
               }
            
            
            Rating rate = new Rating();
            rate.Value = result;
            rate.ContentId = id;
            rate.Id = 1;
            

            return rate;

        }

        public ICollection<Review> GetReviews(int id) {
            ICollection<Review> reviews = _context.Reviews.Where(r => r.ContentID == id).ToList();
            return reviews;
        }

    }
}
