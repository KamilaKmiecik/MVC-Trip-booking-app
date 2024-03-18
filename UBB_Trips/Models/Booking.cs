using System.ComponentModel.DataAnnotations;

namespace UBB_Trips.Models
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public Trip Trip { get; set; }

        [Display(Name = "Number of people")]
        public int NumberOfBookings { get; set; }

        public IEnumerable<Client>? Clients { get; set; }

        public Booking()
        {
            
        }
    }
}
