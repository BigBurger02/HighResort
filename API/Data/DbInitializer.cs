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
            new Room { Id = 1, Name = "Cosy", Capacity = 4, Price = 500 },
            new Room { Id = 2, Name = "Cosy", Capacity = 5, Price = 600 },
            new Room { Id = 3, Name = "Cosy", Capacity = 4, Price = 500 },
            new Room { Id = 4, Name = "Cream", Capacity = 4, Price = 400},
            new Room { Id = 5, Name = "Cream", Capacity = 4, Price = 400 },
            new Room { Id = 6, Name = "Cream", Capacity = 4, Price = 400 },
            new Room { Id = 7, Name = "Light", Capacity = 2, Price = 250 },
            new Room { Id = 8, Name = "Light", Capacity = 2, Price = 250 },
            new Room { Id = 9, Name = "Light", Capacity = 2, Price = 250 },
            new Room { Id = 10, Name = "Light", Capacity = 3, Price = 300 },
            new Room { Id = 11, Name = "Minimalistic", Capacity = 2, Price = 400 },
            new Room { Id = 12, Name = "Minimalistic", Capacity = 2, Price = 400 },
            new Room { Id = 13, Name = "Minimalistic", Capacity = 2, Price = 400 },
            new Room { Id = 14, Name = "Minimalistic", Capacity = 2, Price = 400 },
            new Room { Id = 15, Name = "Minimalistic", Capacity = 2, Price = 400 },
            new Room { Id = 16, Name = "OakForest", Capacity = 3, Price = 350 },
            new Room { Id = 17, Name = "OakForest", Capacity = 3, Price = 350 },
            new Room { Id = 18, Name = "OakForest", Capacity = 4, Price = 400 },
            new Room { Id = 19, Name = "Panoramic", Capacity = 4, Price = 600 },
            new Room { Id = 20, Name = "Panoramic", Capacity = 4, Price = 600 },
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
                CheckInDate = new DateTime(2023, 9, 01),
                CheckOutDate = new DateTime(2023, 9, 03), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 2, 
                CustomerId = 1, 
                RoomId = 1, 
                CheckInDate = new DateTime(2023, 9, 04), 
                CheckOutDate = new DateTime(2023, 9, 07), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 3, 
                CustomerId = 1, 
                RoomId = 1, 
                CheckInDate = new DateTime(2023, 9, 07), 
                CheckOutDate = new DateTime(2023, 9, 08), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 4, 
                CustomerId = 1, 
                RoomId = 1, 
                CheckInDate = new DateTime(2023, 9, 10), 
                CheckOutDate = new DateTime(2023, 9, 12), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 5, 
                CustomerId = 1, 
                RoomId = 2, 
                CheckInDate = new DateTime(2023, 9, 01), 
                CheckOutDate = new DateTime(2023, 9, 06), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 6, 
                CustomerId = 1, 
                RoomId = 2, 
                CheckInDate = new DateTime(2023, 9, 07), 
                CheckOutDate = new DateTime(2023, 9, 10), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 7, 
                CustomerId = 1, 
                RoomId = 2, 
                CheckInDate = new DateTime(2023, 9, 10), 
                CheckOutDate = new DateTime(2023, 9, 13), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 8, 
                CustomerId = 1, 
                RoomId = 3, 
                CheckInDate = new DateTime(2023, 9, 01), 
                CheckOutDate = new DateTime(2023, 9, 05), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 9, 
                CustomerId = 1, 
                RoomId = 3, 
                CheckInDate = new DateTime(2023, 9, 06), 
                CheckOutDate = new DateTime(2023, 9, 10), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 10, 
                CustomerId = 1, 
                RoomId = 3, 
                CheckInDate = new DateTime(2023, 9, 10), 
                CheckOutDate = new DateTime(2023, 9, 12), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 11, 
                CustomerId = 1, 
                RoomId = 3, 
                CheckInDate = new DateTime(2023, 9, 12), 
                CheckOutDate = new DateTime(2023, 9, 13), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 12, 
                CustomerId = 1, 
                RoomId = 3, 
                CheckInDate = new DateTime(2023, 9, 13), 
                CheckOutDate = new DateTime(2023, 9, 17), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 13, 
                CustomerId = 1, 
                RoomId = 4, 
                CheckInDate = new DateTime(2023, 9, 01), 
                CheckOutDate = new DateTime(2023, 9, 02), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 14, 
                CustomerId = 1, 
                RoomId = 4, 
                CheckInDate = new DateTime(2023, 9, 02), 
                CheckOutDate = new DateTime(2023, 9, 03), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 15, 
                CustomerId = 1, 
                RoomId = 4, 
                CheckInDate = new DateTime(2023, 9, 06), 
                CheckOutDate = new DateTime(2023, 9, 07), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 16, 
                CustomerId = 1, 
                RoomId = 4, 
                CheckInDate = new DateTime(2023, 9, 07), 
                CheckOutDate = new DateTime(2023, 9, 10), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 17, 
                CustomerId = 1, 
                RoomId = 4, 
                CheckInDate = new DateTime(2023, 9, 10), 
                CheckOutDate = new DateTime(2023, 9, 14), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 18, 
                CustomerId = 1, 
                RoomId = 5, 
                CheckInDate = new DateTime(2023, 9, 01), 
                CheckOutDate = new DateTime(2023, 9, 04), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 19, 
                CustomerId = 1, 
                RoomId = 5, 
                CheckInDate = new DateTime(2023, 9, 08), 
                CheckOutDate = new DateTime(2023, 9, 10), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 20, 
                CustomerId = 1, 
                RoomId = 5, 
                CheckInDate = new DateTime(2023, 9, 10), 
                CheckOutDate = new DateTime(2023, 9, 11), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
            new Reservations
            {
                Id = 21, 
                CustomerId = 1, 
                RoomId = 5, 
                CheckInDate = new DateTime(2023, 9, 14), 
                CheckOutDate = new DateTime(2023, 9, 16), 
                ReservationDate = new DateTime(2023, 5, 15, 15, 11, 56), 
                TotalPrice = 200, 
                ReservationCanceled = false,
                ReservationPaid = true,
            },
        };
        foreach (var item in reservations)
        {
            context.Reservations.Add(item);
        }
        context.SaveChanges();
    }
}