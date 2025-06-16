using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult DoLogin(RequestLoginJson request)
    {

    }
}
