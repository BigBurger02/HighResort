using Microsoft.AspNetCore.Mvc;

using API.Interfaces;
using API.Models;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookARoomController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomRepository _customRepository;
    private readonly ICrudGenericRepository<Reservations> _reservationCrudGenericRepository;
    private readonly ICrudGenericRepository<Room> _roomCrudGenericRepository;
    
    public BookARoomController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _customRepository = _unitOfWork.GetCustomRepository();
        _reservationCrudGenericRepository = _unitOfWork.GetCrudGenericRepository<Reservations>();
        _roomCrudGenericRepository = _unitOfWork.GetCrudGenericRepository<Room>();
    }
    
    [HttpGet]
    public IActionResult GetFreeRooms(DateTime checkIn, DateTime checkOut, [FromQuery]IEnumerable<string>? names, int capacity)
    {
        var query = _customRepository.GetFreeRooms(checkIn, checkOut, names, capacity);
        
        return new ObjectResult(query) { StatusCode = 200 };
    }

    // [HttpGet("{id}")]
    // public IActionResult CheckIfRoomFree(DateTime checkIn, DateTime checkOut, int id)
    // {
    //     var available = _customRepository.CheckIfRoomFree(checkIn, checkOut, id);
    //     
    //     return new ObjectResult(available) { StatusCode = 200 };
    // }

    [HttpPost]
    public async Task<IActionResult> BookARoom(DateTime checkIn, DateTime checkOut, int roomId)
    {
        if (!_customRepository.CheckIfRoomFree(checkIn, checkOut, roomId))
        {
            return new ObjectResult("Room not available.") { StatusCode = 400 };
        }

        var room = await _roomCrudGenericRepository.GetByIdAsync(roomId);
        if (room == null)
        {
            return new ObjectResult("Room not found.") { StatusCode = 404 };
        }
        
        var totalPrice = room.Price * ((checkOut - checkIn).Days);

        var newReservation = new Reservations()
        {
            RoomId = roomId,
            CheckInDate = checkIn,
            CheckOutDate = checkOut,
            ReservationDate = DateTime.Now,
            TotalPrice = totalPrice,
            ReservationCanceled = false,
            ReservationPaid = false,
        };
        
        _reservationCrudGenericRepository.CreateAsync(newReservation);
        _unitOfWork.CommitAsync();

        return new ObjectResult(null) { StatusCode = 201 };
    }
}