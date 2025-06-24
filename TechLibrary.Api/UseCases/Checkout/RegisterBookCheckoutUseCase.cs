using TechLibrary.Api.Infrastructure.DataAccess;

namespace TechLibrary.Api.UseCases.Checkout;

public class RegisterBookCheckoutUseCase
{
    private const int MAX_LOAN_DAYS = 7;
    public void Execute (Guid bookId)
    {
        var dbContext = new TechLibraryDbContext();

        var entity = new Domain.Entities.Checkout()
        {
            BookId = bookId,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS),

        };

    }
}
