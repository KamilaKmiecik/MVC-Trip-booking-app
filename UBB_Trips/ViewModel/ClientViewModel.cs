using System.ComponentModel.DataAnnotations;

namespace UBB_Trips.ViewModels
{
    public class ClientViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public int? BookingID { get; set; } 
    }
}
