using System.ComponentModel.DataAnnotations;

namespace UBB_Trips.Models;


public enum TripTypeModel
{
    Family, Honeymoon, Luxury, SelfDrive, Holidays, Vacation
}

public enum FoodAcoomodationModel
{
    None, RoomOnly, HalfBoard, FullBoard
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

    public string ImageURL { get; set; }

    public string Alt { get; set; }

}



