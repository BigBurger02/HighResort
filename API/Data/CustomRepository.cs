using Microsoft.EntityFrameworkCore;

using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Data;

public class CustomRepository : ICustomRepository
{
    private readonly APIContext _context;

    public CustomRepository(APIContext context)
    {
        _context = context;
    }

    public IQueryable<Room> GetFreeRooms(DateTime checkIn, DateTime checkOut, IEnumerable<string>? names = null, int capacity = default)
    {
        var availableRooms = _context.Room
            .Where(room =>
                !_context.Reservations.Any(r =>
                    room.Id == r.RoomId &&
                    (
                        (r.CheckInDate >= checkIn && r.CheckOutDate <= checkOut) ||
                        (r.CheckInDate <= checkIn && r.CheckOutDate >= checkOut) ||
                        (checkIn >= r.CheckInDate && checkIn < r.CheckOutDate) ||
                        (checkOut > r.CheckInDate && checkOut <= r.CheckOutDate)
                    )
                )
            );

        if (names != null && names.Count() != 0)
        {
            availableRooms = availableRooms.Where(room =>
                names.Any(n =>
                    room.Name == n
                )
            );
        }

        if (capacity != 0)
        {
            availableRooms = availableRooms.Where(room => room.Capacity >= capacity);
        }
        
        return availableRooms;
    }
}