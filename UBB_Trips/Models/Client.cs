using System.ComponentModel.DataAnnotations;

namespace UBB_Trips.Models;

public class Client
{
    [Key]
    public int ID { get; set; }
    
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Display(Name = "Last name")]
    public string LastName { get; set; }
    public string Email { get; set; }
    public Booking? Booking { get; set; }

    public Client()
    {
            
    }
}
