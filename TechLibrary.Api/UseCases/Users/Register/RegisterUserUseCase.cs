using FluentValidation.Results;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infrastructure.Cryptography;
using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Api.Infrastructure.Security.Tokens.Access;
using TechLibrary.Communitcation.Requests;
using TechLibrary.Communitcation.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserUseCase
{
    private readonly List<string> errorMessages;

    public ResponseRegisterUserJson Execute(RequestUserJson request)
    {
        var dbContext = new TechLibraryDbContext();

        Validate(request, dbContext);

        var cryptography = new BCryptAlgorithm();

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = cryptography.HashPassword(request.Password),
        };

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();

        JwtTokenGenerator tokenGenerator = new JwtTokenGenerator();

        return new ResponseRegisterUserJson
        {
            Name = entity.Name,
            AcessToken = tokenGenerator.Generate(entity)
        };
    }
    private void Validate(RequestUserJson request, TechLibraryDbContext dbContext)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var existUserWithEmail = dbContext.Users.Any(user => user.Email.Contains(request.Email));
        if (existUserWithEmail)
            result.Errors.Add(new ValidationFailure("E-mail", "E-mail já registrado"));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
        }

    }
}