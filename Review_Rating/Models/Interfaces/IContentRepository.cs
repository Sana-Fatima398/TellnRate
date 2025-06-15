using Microsoft.AspNetCore.Components.Forms;

namespace Review_Rating.Models.Interfaces
{
    public interface IContentRepository
    {
        public List<Content> GetAll();
        public Content GetByID(int id);
        public string AddContent(Content content);
        public void UpdateContent(Content content);

        public Dictionary<string, int> GetCounts();
        public string DeleteContent(string id);
        public void AddRating(string contentId, int rating);

        public List<Content> GetSearchData(string inputData, string contentType, string contentGenre, string contentReleaseYear);

        public List<Content> GetFavs(string userid);
        public void AddtoFav(int id, string userid);
        public void RemoveFav(int ID, string userId);


        public Rating GetRatings(int id);
        public ICollection<Comment> GetComments(int id);
        public ICollection<Review> GetReviews(int id);

       

    }
}
 