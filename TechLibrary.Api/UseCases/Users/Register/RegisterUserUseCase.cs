using TechLibrary.Communitcation.Requests;
using TechLibrary.Communitcation.Responses;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserUseCase
{
    public ResponseRegisterUserJson Execute(RequestUserJson request)
    {
        Validate(request);
        return new ResponseRegisterUserJson
        {

        };
    }
    private void Validate(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
          var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        }

        throw new Exception();
    }
}
