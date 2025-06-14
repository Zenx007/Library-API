using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infrastructure;
using TechLibrary.Communitcation.Requests;
using TechLibrary.Communitcation.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserUseCase
{
    private readonly List<string> errorMessages;

    public ResponseRegisterUserJson Execute(RequestUserJson request)
    {
        Validate(request);

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
        };

        var dbContext = new TechLibraryDbContext();

        dbContext.Users.Add(entity);

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

        throw new ErrorOnValidationException(errorMessages);
        }

    }
}