namespace Review_Rating.Models.Interfaces
{
    public interface IReviewRepository
    {
        public List<Review> GetAll(int id);

        public string Save(Review review);

        public AppUser FindUser(string email);
    }
}
