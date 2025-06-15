using System.ComponentModel.DataAnnotations.Schema;

namespace Review_Rating.Models
{
    public class Review
    {
        public int ID { get; set; }

        public float Story { get; set; }
        public float Acting { get; set; }
        public float Music { get; set; }
        public float RewatchValue { get; set; }


        public string? Headline { get; set; }
        public string? Explanation { get; set; }

        public int ContentID { get; set; }
        public Content Content { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
      

      
    }
}
