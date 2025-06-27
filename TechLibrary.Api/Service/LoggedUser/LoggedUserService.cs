using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infrastructure.DataAccess;

namespace TechLibrary.Api.Service.LoggedUser;

public class LoggedUserService
{
    private readonly HttpContext _httpContext;

    public LoggedUserService(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    public User User()
    {
        var authentication = _httpContext.Request.Headers.Authorization.ToString();

        var token = authentication["Bearer ".Length..].Trim();

        var dbContext = new TechLibraryDbContext();
    }
}
