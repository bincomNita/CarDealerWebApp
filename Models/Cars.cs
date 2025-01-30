using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerWebApp.Models
{
    public class Cars
    {
        [Key]
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }   
        
        public List<Inquiry> Inquiries { get; set; }
    }
}
