using famazo.HotelManagementApp.Model;

namespace famazo.HotelManagementApp.Controller;

public class GuestGenerator : IGuestGenerator
{
    private readonly Random _random = new Random();
    public List<Guest> GenerateGuests(int numLeisure, int numBusiness)
    {
        List<Guest> generatedGuests = new();
        for (int i = 1; i <= numLeisure; i++)
        {
            Guest guest = new Guest(GuestType.Leisure, GenerateRandomRoomType(), GenerateRandomStay());
            generatedGuests.Add(guest);
        }
        
        for (int i = 1; i <= numBusiness; i++)
        {
            Guest guest = new Guest(GuestType.Business,GenerateRandomRoomType(), GenerateRandomStay());
            generatedGuests.Add(guest);
        }

        return generatedGuests;
    }

    private Stay GenerateRandomStay()
    {
        DateTime start = new DateTime(2023, 01, 01);
        int maxStay = 30;
        int range = 365 - maxStay;
        DateTime checkinDate = start.AddDays(_random.Next(range));
        DateTime checkoutDate = checkinDate.AddDays(_random.Next(1, maxStay));
        return new Stay(checkinDate, checkoutDate);
    }

    private RoomType GenerateRandomRoomType()
    {
        int roomTypeLength = Enum.GetNames(typeof(RoomType)).Length;
        return (RoomType)_random.Next(0, roomTypeLength);
    }
}