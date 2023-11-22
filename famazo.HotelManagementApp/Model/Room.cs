namespace famazo.HotelManagementApp.Model;

public class Room
{
    public RoomType Type { get; }
    public decimal PricePerNight { get; set; }
    public bool Occupied { get; set; }

    public Room(RoomType type)
    {
        Occupied = false;
        Type = type;
        PricePerNight = (decimal)(Type == RoomType.StreetFacing ? 100.0 : Type == RoomType.CourtyardFacing ? 120.0 : 200.0);
    }
}