using API.Models;

namespace API.Interfaces;

public interface ICustomRepository
{
    IQueryable<Room> GetFreeRooms(DateTime checkIn, DateTime checkOut, IEnumerable<string>? names = null, int capacity = default);
}