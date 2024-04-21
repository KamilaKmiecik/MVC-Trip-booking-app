using System.ComponentModel.DataAnnotations;

namespace UBB_Trips.ViewModels
{
    public class BookingViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trip is required")]
        public int TripID { get; set; }

        [Display(Name = "Number of people")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of people must be greater than 0")]
        public int NumberOfBookings { get; set; }

        public IEnumerable<ClientViewModel> Clients { get; set; }
    }
}
