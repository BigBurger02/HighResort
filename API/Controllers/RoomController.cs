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
        private readonly IRepository _repo;

        public RoomController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Room
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRoom()
        {
            var rooms = _repo.GetAllRooms();
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
            var room = await _repo.GetRoomAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return new ObjectResult(room) { StatusCode = 200 };;
        }

        // PUT: api/Room/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            try
            {
                _repo.UpdateRoom(room);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        // POST: api/Room
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            _repo.CreateRoom(room);

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (await _repo.DeleteRoom(id) == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}