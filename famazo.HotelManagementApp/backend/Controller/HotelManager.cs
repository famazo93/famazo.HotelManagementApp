using famazo.HotelManagementApp.backend.Model;
using famazo.HotelManagementApp.Controller;
using famazo.HotelManagementApp.Model;

namespace famazo.HotelManagementApp.backend.Controller;

public class HotelManager : IHotelManager
{
    private readonly Hotel _hotel;

    public HotelManager(Hotel hotel)
    {
        _hotel = hotel;
    }
    
    public int GetOccupancy()
    {
        IEnumerable<Room> occupiedRooms = _hotel.HotelRooms.Where(room => room.CheckAvailability(new Stay(DateTime.Today, DateTime.Today)));
        return occupiedRooms.Count() * 100 / _hotel.HotelRooms.Count;
    }

    public int GetOccupancy(DateTime date)
    {
        IEnumerable<Room> occupiedRooms = _hotel.HotelRooms.Where(room => room.CheckAvailability(new Stay(date, date)));
        return occupiedRooms.Count() * 100 / _hotel.HotelRooms.Count;
    }

    private Room? TryToPlaceGuest(Guest guest)
    {
        IEnumerable<Room> freeRooms = _hotel.HotelRooms.Where(room => room.CheckAvailability(guest.GuestStay));
        List<Room> freeRoomList = freeRooms.ToList();
        if (freeRoomList.Any())
        {
            // select first room that matches preference
            try
            {
                Room selectedRoom = freeRoomList.First(room => room.Type == guest.PreferredRoomType);
                selectedRoom.BookRoom(guest.GuestStay);
                return selectedRoom;
            }
            catch (Exception e)
            {
                if (e.InnerException is ArgumentNullException)
                {
                    // select first room that is not a Suit
                    try
                    {
                        Room selectedRoom = freeRoomList.First(room => room.Type != RoomType.Suit);
                        selectedRoom.BookRoom(guest.GuestStay);
                        selectedRoom.PricePerNight *= (decimal)0.8;
                        return selectedRoom;
                    }
                    catch (Exception e2)
                    {
                        // select first available Suit
                        if (e2.InnerException is ArgumentNullException)
                        {
                            Room selectedRoom = freeRoomList.First(room => room.CheckAvailability(guest.GuestStay));
                            selectedRoom.BookRoom(guest.GuestStay);
                            selectedRoom.PricePerNight *= (decimal)0.5;
                            return selectedRoom;
                        }
                    } 
                }
                else
                {
                    Console.WriteLine(e);
                }
            }
        }
        return null;
    }

    public void HandleNewGuest(Guest guest)
    {
        Room? room = TryToPlaceGuest(guest);
        if (room == null)
        {
            Console.WriteLine("Unfortunately we do not have any available rooms.");
        }
        else
        {
            Console.WriteLine($"Welcome! We have a {room.Type} room for you for a price of {room.PricePerNight} per night.");
        }
    }

    public Dictionary<GuestType, Stay> GetCurrentStays()
    {
        return _hotel.HotelGuests.ToDictionary(guest => guest.Type, guest => guest.GuestStay);
    }
}