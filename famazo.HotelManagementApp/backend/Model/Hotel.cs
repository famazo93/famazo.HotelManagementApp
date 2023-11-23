using famazo.HotelManagementApp.Model;

namespace famazo.HotelManagementApp.backend.Model;

public class Hotel
{
    public List<Room> HotelRooms { get; }
    public List<Guest> HotelGuests { get; set; }

    public Hotel(List<Room> rooms)
    {
        HotelRooms = rooms;
        HotelGuests = new List<Guest>();
    }
}