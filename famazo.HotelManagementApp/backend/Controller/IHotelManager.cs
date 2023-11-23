using famazo.HotelManagementApp.Model;

namespace famazo.HotelManagementApp.Controller;

public interface IHotelManager
{
    public int GetOccupancy();
    public int GetOccupancy(DateTime date);
    public void HandleNewGuest(Guest guest);
}