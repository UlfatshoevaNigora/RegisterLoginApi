using System.Security.Cryptography;
using System.Text;

namespace login.Infrastructure.Utilities;

public static class PasswordUtility
{
    public static string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        StringBuilder stringBuilder = new();
        foreach (var @byte in bytes)
        {
            stringBuilder.Append(@byte.ToString("x2"));
        }

        return stringBuilder.ToString();
    }
}