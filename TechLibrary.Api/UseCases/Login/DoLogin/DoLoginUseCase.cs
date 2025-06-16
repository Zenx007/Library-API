using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communitcation.Responses;

namespace TechLibrary.Api.UseCases.Login.DoLogin;

public class DoLoginUseCase
{
    public ResponseRegisterUserJson Execute (RequestLoginJson request)
    {

        var dbContext = new TechLibraryDbContext();

       var user = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
        if (user is null)
        {
            throw Exception();
        }

        return new ResponseRegisterUserJson
        {

        };
    }
}
