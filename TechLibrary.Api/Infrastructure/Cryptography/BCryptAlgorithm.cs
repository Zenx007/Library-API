namespace TechLibrary.Api.Infrastructure.Cryptography;

public class BCryptAlgorithm
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
}
