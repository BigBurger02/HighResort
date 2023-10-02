using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using API.Interfaces;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudGenericRepository<Reservations> _reservationsCrudGenericRepository;

        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _reservationsCrudGenericRepository = _unitOfWork.GetCrudGenericRepository<Reservations>();
        }

        // GET: api/Reservation
        [HttpGet]
        public ActionResult<IEnumerable<Reservations>> GetReservations()
        {
            var reservations = _reservationsCrudGenericRepository.GetAll();
            if (reservations == null)
            {
                return NotFound();
            }
            return new ObjectResult(reservations) { StatusCode = 200 };
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservations>> GetReservation(int id)
        {
            var reservation = await _reservationsCrudGenericRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return new ObjectResult(reservation) { StatusCode = 200 };
        }

        // PUT: api/Reservation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutReservations(int id, Reservations reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }
            
            _reservationsCrudGenericRepository.Update(reservation);
            _unitOfWork.CommitAsync();
            
            return NoContent();
        }

        // POST: api/Reservation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Reservations> PostReservations(Reservations reservation)
        {
            _reservationsCrudGenericRepository.CreateAsync(reservation);
            _unitOfWork.CommitAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservations(int id)
        {
            var reservation = await _reservationsCrudGenericRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            
            _reservationsCrudGenericRepository.Delete(reservation);
            _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
