using API.Models;

namespace API.Interfaces;

public interface IRepository
{
    IEnumerable<Room> GetAllRooms();
    Task<Room?> GetRoomAsync(int id);
    void UpdateRoom(Room room);
    void CreateRoom(Room room);
    Task<bool> DeleteRoom(int id);
}