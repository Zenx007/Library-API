using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Communitcation.Requests;
using TechLibrary.Communitcation.Responses;

namespace TechLibrary.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), 201)]
    public IActionResult Create (RequestUserJson request)
    {

        var useCase = new RegisterUserUseCase();

       var response = useCase.Execute(request);


        return Created(string.Empty, response);
    }
    
}
