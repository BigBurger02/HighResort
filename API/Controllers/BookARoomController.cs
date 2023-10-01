using Microsoft.AspNetCore.Mvc;

using API.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookARoomController : ControllerBase
{
    private readonly IRepository _repo;
    
    public BookARoomController(IRepository repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public IActionResult GetFreeRooms(DateTime checkIn, DateTime checkOut, [FromQuery]IEnumerable<string>? names, int capacity)
    {
        var query = _repo.GetFreeRooms(checkIn, checkOut, names, capacity);
        
        return new ObjectResult(query) { StatusCode = 200 };
    }
}