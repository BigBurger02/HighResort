using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookARoomController : ControllerBase
{
    [HttpGet]
    public string Get1()
    {
        return "1";
    }
    
    [HttpPost]
    public string Get2()
    {
        return "2";
    }
}