namespace Review_Rating.Models
{
    public class Content
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public string PlotSummary { get; set; }

        public string Type { get; set; }

        public int ReleaseYear { get; set; }

        public string Genre { get; set; }

        public string TrailerLink { get; set; }

        public string ImageLink { get; set; }

        public Rating Rating { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
