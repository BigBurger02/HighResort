using Microsoft.AspNetCore.Mvc;

using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudGenericRepository<Room> _roomCrudGenericRepository;

        public RoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roomCrudGenericRepository = _unitOfWork.GetCrudGenericRepository<Room>();
        }

        // GET: api/Room
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRoom()
        {
            var rooms = _roomCrudGenericRepository.GetAll();
            if (rooms == null)
            {
                return NotFound();
            }
            return new ObjectResult(rooms) { StatusCode = 200 };
        }

        // GET: api/Room/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _roomCrudGenericRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return new ObjectResult(room) { StatusCode = 200 };
        }

        // PUT: api/Room/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }
            
            _roomCrudGenericRepository.Update(room);
            _unitOfWork.CommitAsync();
            
            return NoContent();
        }

        // POST: api/Room
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Room> PostRoom(Room room)
        {
            _roomCrudGenericRepository.CreateAsync(room);
            _unitOfWork.CommitAsync();

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _roomCrudGenericRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            
            _roomCrudGenericRepository.Delete(room);
            _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}