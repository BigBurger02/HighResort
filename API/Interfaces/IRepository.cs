using API.Models;

namespace API.Interfaces;

public interface IRepository
{
    // Rooms
    IEnumerable<Room> GetAllRooms();
    Task<Room?> GetRoomAsync(int id);
    void UpdateRoom(Room room);
    void CreateRoom(Room room);
    Task<bool> DeleteRoom(int id);
    
    // Booking
    IQueryable<Room> GetFreeRooms(DateTime checkIn, DateTime checkOut, IEnumerable<string>? names = null, int capacity = default);
}