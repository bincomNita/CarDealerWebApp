using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerWebApp.Models
{
    public class Inquiry
    {
        public int InquiryId { get; set; }
        
        [ForeignKey("Cars")]
        public int CarId { get; set; }
       
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Message { get; set; }

        public Cars Cars { get; set; }
    }
}
