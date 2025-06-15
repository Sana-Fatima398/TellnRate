using System.ComponentModel.DataAnnotations.Schema;

namespace Review_Rating.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Statement { get; set; }


        public int ContentID { get; set; }
        public Content Content { get; set; }

        public string UserName { get; set; }
       
        public string UserId { get; set; }

       
        public AppUser User { get; set; }

        
    }
}
