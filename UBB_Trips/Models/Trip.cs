using System.ComponentModel.DataAnnotations;

namespace UBB_Trips.Models;


public enum TripTypeModel
{
    [Display(Name = "Family")]
    Family,

    [Display(Name = "Honeymoon")]
    Honeymoon,

    [Display(Name = "Luxury")]
    Luxury,

    [Display(Name = "Self Drive")]
    SelfDrive,

    [Display(Name = "Holidays")]
    Holidays,

    [Display(Name = "Vacation")]
    Vacation
}

public enum FoodAcoomodationModel
{
    [Display(Name = "None")]
    None,

    [Display(Name = "Room Only")]
    RoomOnly,

    [Display(Name = "Half Board")]
    HalfBoard,

    [Display(Name = "Full Board")]
    FullBoard
}

public enum CountryModel
{
    Poland, Germany, Ukraine, Brasil, Mexico, Brazil, USA, Maldives, France
}

public class Trip
{
    [Key]
    public int ID { get; set; }

    public DateTime TripSTART { get; set; }

    public DateTime TripEND { get; set; }

    public double days => Math.Floor((TripEND - TripSTART).TotalDays);

    public string Title { get; set; }
    public string Description { get; set; }

    public TripTypeModel Type { get; set; }

    public FoodAcoomodationModel Food { get; set; }

    public CountryModel Country { get; set; }

    [Display(Name = "Image")]
    public string ImageURL { get; set; }

    public string Alt { get; set; }

    public Trip()
    {
            
    }

}



