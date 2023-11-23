namespace famazo.HotelManagementApp.Model;

public class Room
{
    public RoomType Type { get; }
    public decimal PricePerNight { get; set; }
    private List<Stay> BookedStays { get; }
    
    public Room(RoomType type)
    {
        BookedStays = new List<Stay>();
        Type = type;
        PricePerNight = (decimal)(Type == RoomType.StreetFacing ? 100.0 : Type == RoomType.CourtyardFacing ? 120.0 : 200.0);
    }

    public bool CheckAvailability(Stay stayToCheck)
    {
        foreach (Stay bookedStay in BookedStays)
        {
            if (stayToCheck.CheckinDate >= bookedStay.CheckinDate && stayToCheck.CheckinDate < bookedStay.CheckoutDate)
            {
                return false;
            }
            if (stayToCheck.CheckoutDate > bookedStay.CheckinDate &&
                       stayToCheck.CheckoutDate <= bookedStay.CheckoutDate)
            {
                return false;
            }
        }

        return true;
    }

    public void BookRoom(Stay stayToCheck)
    {
        BookedStays.Add(stayToCheck);
    }
}