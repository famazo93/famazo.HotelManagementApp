using famazo.HotelManagementApp.Model;

namespace famazo.HotelManagementApp.Controller;

public interface IGuestGenerator
{
    List<Guest> GenerateGuests(int numLeisure, int numBusiness);
}