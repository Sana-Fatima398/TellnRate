namespace Review_Rating.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public int ContentId { get; set; }
        public Content content { get; set; }    

        public string UserId { get; set; }
        public AppUser user { get; set; }
    }
}
