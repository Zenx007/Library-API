using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infrastructure.Cryptography;
using TechLibrary.Api.Infrastructure.DataAccess;
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

        var cryptography = new BCryptAlgorithm();

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = cryptography.HashPassword(request.Password),
        };

        var dbContext = new TechLibraryDbContext();

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();

        return new ResponseRegisterUserJson
        {
            Name = entity.Name,    
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