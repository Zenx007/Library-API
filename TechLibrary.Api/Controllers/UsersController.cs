﻿using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Communication.Responses;
using TechLibrary.Communitcation.Requests;
using TechLibrary.Communitcation.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register(RequestUserJson request)
     {
        try
        {
            var useCase = new RegisterUserUseCase();

            var response = useCase.Execute(request);


            return Created(string.Empty, response);
        }
        catch (TechLibraryException ex)
        {
            return BadRequest(new ResponseErrorMessagesJson
            {
                Errors = ex.GetErrorMessages()
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson {
                Errors = ["Erro Desconhecido"]
            });     
        }
    }

}