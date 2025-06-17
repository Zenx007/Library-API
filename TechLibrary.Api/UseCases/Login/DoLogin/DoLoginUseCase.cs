using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Api.Infrastructure.Security.Cryptography;
using TechLibrary.Api.Infrastructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communitcation.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Login.DoLogin;

public class DoLoginUseCase
{
    public ResponseRegisterUserJson Execute (RequestLoginJson request)
    {

        var dbContext = new TechLibraryDbContext();
       var user = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));

        if (user is null)
            throw new InvalidLoginException();

        var cryptography = new BCryptAlgorithm();
        var passwordIsValid = cryptography.Verify(request.Password, user);
        if (passwordIsValid == false)
            throw new InvalidLoginException();

        JwtTokenGenerator tokenGenerator = new JwtTokenGenerator();

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            AcessToken = tokenGenerator.Generate(user)
        };
    }
}
