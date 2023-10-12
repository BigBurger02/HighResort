using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomRepository _customRepository;

    public ImageController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _customRepository = _unitOfWork.GetCustomRepository();
    }
    
    [HttpGet]
    public IActionResult Get(int roomId)
    {
        var imageId = _customRepository.GetRoomImageId(roomId);

        if (imageId == 0)
        {
            return NotFound("Room not found");
        }
        
        string imagePath = "img/" + imageId + ".jpg";
        
        if (!System.IO.File.Exists(imagePath))
        {
            return NotFound("Image not found");
        }

        byte[] imageData = null;
        
        imageData = System.IO.File.ReadAllBytes(imagePath);
        
        if (imageData == null)
        {
            return NotFound("Image not found");
        }
        
        Response.ContentType = "image/jpeg";
        
        return File(imageData, Response.ContentType);
    }
}