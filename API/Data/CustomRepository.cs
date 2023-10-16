using System.Linq.Expressions;
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
        IQueryable<Room> availableRooms = _context.Room;
        
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
        
        availableRooms = availableRooms.Where(room =>
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
        
        return availableRooms;
    }

    public bool CheckIfRoomFree(DateTime checkIn, DateTime checkOut, int id)
    {
        return 
            !_context.Reservations.Any(r =>
                r.RoomId == id &&
                (
                    (r.CheckInDate >= checkIn && r.CheckOutDate <= checkOut) ||
                    (r.CheckInDate <= checkIn && r.CheckOutDate >= checkOut) ||
                    (checkIn >= r.CheckInDate && checkIn < r.CheckOutDate) ||
                    (checkOut > r.CheckInDate && checkOut <= r.CheckOutDate)
                )
            );
    }

    public decimal GetTotalPrice(int reservationId)
    {
        var reservation = _context.Reservations
            .Find(reservationId);
        if (reservation == null)
        {
            throw new KeyNotFoundException("Reservation with given id not found");
        }

        return reservation.TotalPrice;
    }

    public void ReservationPaid(int reservationId)
    {
        var reservation = _context.Reservations.Find(reservationId);
        reservation.ReservationPaid = true;
    }

    public int GetRoomImageId(int roomId)
    {
        var room = _context.Room.Find(roomId);
        if (room == null)
        {
            return 0;
        }

        return room.ImageId;
    }
}