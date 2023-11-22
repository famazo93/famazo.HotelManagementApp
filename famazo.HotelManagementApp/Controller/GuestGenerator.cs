using famazo.HotelManagementApp.Model;

namespace famazo.HotelManagementApp.Controller;

public class GuestGenerator : IGuestGenerator
{
    public List<Guest> GenerateGuests(int numLeisure, int numBusiness)
    {
        List<Guest> generatedGuests = new();
        for (int i = 1; i <= numLeisure; i++)
        {
            Random random = new Random();
            int roomTypeLength = Enum.GetNames(typeof(RoomType)).Length;
            Guest guest = new Guest(GuestType.Leisure, (RoomType)random.Next(0, roomTypeLength));
            generatedGuests.Add(guest);
        }
        
        for (int i = 1; i <= numBusiness; i++)
        {
            Random random = new Random();
            int roomTypeLength = Enum.GetNames(typeof(RoomType)).Length;
            Guest guest = new Guest(GuestType.Business, (RoomType)random.Next(0, roomTypeLength));
            generatedGuests.Add(guest);
        }

        return generatedGuests;
    }
}