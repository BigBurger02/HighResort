using API.Models;

namespace API.Data;

public static class DbInitializer
{
    public static void Initialize(APIContext context)
    {
        context.Database.EnsureCreated();
        
        if (context.Room.Any())
        {
            return;
        }

        var rooms = new[]
        {
            new Room { Id = 1, Name = "Dark Gray", Capacity = 3 },
            new Room { Id = 2, Name = "Dark Gray", Capacity = 3 },
            new Room { Id = 3, Name = "Dark Gray", Capacity = 4 },
            new Room { Id = 4, Name = "Dark Gray", Capacity = 1 },
            new Room { Id = 5, Name = "Dark Gray", Capacity = 6 },
            new Room { Id = 6, Name = "Dark Gray", Capacity = 1 },
            new Room { Id = 7, Name = "Dark Gray", Capacity = 4 },
            new Room { Id = 8, Name = "Dark Gray", Capacity = 4 },
            new Room { Id = 9, Name = "Dark Gray", Capacity = 6 },
            new Room { Id = 10, Name = "Dark Gray", Capacity = 3 },
        };
        foreach (var item in rooms)
        {
            context.Room.Add(item);
        }
        context.SaveChanges();

        var reservations = new[]
        {
            new Reservations
            {
                Id = 1, 
                CustomerId = 1, 
                RoomId = 1, 
                CheckInDate = new DateTime(2023, 5, 15), 
                CheckOutDate = new DateTime(2023, 5, 15), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationStatus = ReservationStatus.Pending, 
                PaymentStatus = PaymentStatus.NotPaid,
            },
            new Reservations
            {
                Id = 2, 
                CustomerId = 2, 
                RoomId = 2, 
                CheckInDate = new DateTime(2023, 5, 15), 
                CheckOutDate = new DateTime(2023, 5, 15), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 400, 
                ReservationStatus = ReservationStatus.Pending, 
                PaymentStatus = PaymentStatus.NotPaid,
            },
        };
        foreach (var item in reservations)
        {
            context.Reservations.Add(item);
        }
        context.SaveChanges();
    }
}