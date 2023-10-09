using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PayController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICrudGenericRepository<Reservations> _reservationCrudGenericRepository;
    private readonly ICustomRepository _customRepository;

    public PayController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _reservationCrudGenericRepository = _unitOfWork.GetCrudGenericRepository<Reservations>();
        _customRepository = _unitOfWork.GetCustomRepository();
    }

    [HttpGet]
    public IActionResult Get(int reservationId)
    {
        decimal totalPrice;

        try
        {
            totalPrice = _customRepository.GetTotalPrice(reservationId);
        }
        catch (KeyNotFoundException)
        {
            return new ObjectResult("Reservation with given id not found") { StatusCode = 404 };
        }

        return new ObjectResult(totalPrice) { StatusCode = 200 };
    }

    [HttpPut]
    public IActionResult Paid(int reservationId)
    {
        _customRepository.ReservationPaid(reservationId);
        _unitOfWork.CommitAsync();

        return new ObjectResult(null) { StatusCode = 204 };
    }
}