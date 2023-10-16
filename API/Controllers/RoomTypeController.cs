using Microsoft.AspNetCore.Mvc;

using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudGenericRepository<RoomType> _roomCrudGenericRepository;

        public RoomTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roomCrudGenericRepository = _unitOfWork.GetCrudGenericRepository<RoomType>();
        }

        // GET: api/RoomType
        [HttpGet]
        public ActionResult<IEnumerable<RoomType>> GetRoomType()
        {
            var roomTypes = _roomCrudGenericRepository.GetAll();
            if (roomTypes == null)
            {
                return NotFound();
            }
            return new ObjectResult(roomTypes) { StatusCode = 200 };
        }

        // GET: api/RoomType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomType>> GetRoomTypes(int id)
        {
            var roomTypes = await _roomCrudGenericRepository.GetByIdAsync(id);
            if (roomTypes == null)
            {
                return NotFound();
            }
            return new ObjectResult(roomTypes) { StatusCode = 200 };
        }

        // PUT: api/RoomType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutRoomType(int id, RoomType roomType)
        {
            if (id != roomType.Id)
            {
                return BadRequest();
            }
            
            _roomCrudGenericRepository.Update(roomType);
            _unitOfWork.CommitAsync();
            
            return NoContent();
        }

        // POST: api/RoomType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<RoomType> PostRoomType(RoomType roomType)
        {
            _roomCrudGenericRepository.CreateAsync(roomType);
            _unitOfWork.CommitAsync();

            return CreatedAtAction("GetRoomTypes", new { id = roomType.Id }, roomType);
        }

        // DELETE: api/RoomType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var roomType = await _roomCrudGenericRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            
            _roomCrudGenericRepository.Delete(roomType);
            _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}