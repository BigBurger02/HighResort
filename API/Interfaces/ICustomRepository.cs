using API.Models;

namespace API.Interfaces;

public interface ICustomRepository
{
    IQueryable<Room> GetFreeRooms(DateTime checkIn, DateTime checkOut, IEnumerable<string>? names = null, int capacity = default);
    bool CheckIfRoomFree(DateTime checkIn, DateTime checkOut, int id);
    decimal GetTotalPrice(int reservationId);
    void ReservationPaid(int reservationId);
    int GetRoomImageId(int roomId);
}