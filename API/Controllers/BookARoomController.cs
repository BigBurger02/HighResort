using Microsoft.AspNetCore.Mvc;

using API.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookARoomController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomRepository _customRepository;
    
    public BookARoomController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _customRepository = _unitOfWork.GetCustomRepository();
    }
    
    [HttpGet]
    public IActionResult GetFreeRooms(DateTime checkIn, DateTime checkOut, [FromQuery]IEnumerable<string>? names, int capacity)
    {
        var query = _customRepository.GetFreeRooms(checkIn, checkOut, names, capacity);
        
        return new ObjectResult(query) { StatusCode = 200 };
    }
}