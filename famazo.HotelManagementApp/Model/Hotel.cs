namespace famazo.HotelManagementApp.Model;

public class Hotel
{
    public List<Room> HotelRooms { get; }
    public List<Guest> HotelGuests { get; set; }

    public Hotel(List<Room> rooms)
    {
        HotelRooms = rooms;
    }

    public int GetOccupancy()
    {
        IEnumerable<Room> occupiedRooms = HotelRooms.Where(room => room.Occupied);
        return occupiedRooms.Count() * 100 / HotelRooms.Count;
    }

    private Room? TryToPlaceGuest(Guest guest)
    {
        IEnumerable<Room> freeRooms = HotelRooms.Where(room => !room.Occupied);
        List<Room> freeRoomList = freeRooms.ToList();
        if (freeRoomList.Any())
        {
            // select first room that matches preference
            try
            {
                Room selectedRoom = freeRoomList.First(room => room.Type == guest.PreferredRoomType);
                selectedRoom.Occupied = true;
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
                        selectedRoom.Occupied = true;
                        selectedRoom.PricePerNight *= (decimal)0.8;
                        return selectedRoom;
                    }
                    catch (Exception e2)
                    {
                        // select first available Suit
                        if (e2.InnerException is ArgumentNullException)
                        {
                            Room selectedRoom = freeRoomList.First(room => room.Occupied);
                            selectedRoom.Occupied = true;
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
}