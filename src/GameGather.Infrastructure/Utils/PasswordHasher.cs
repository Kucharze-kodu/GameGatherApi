using System.Security.Cryptography;
using GameGather.Application.Utils;

namespace GameGather.Infrastructure.Utils;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 10000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
    
    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes
            .Pbkdf2(password, salt, Iterations, Algorithm, KeySize);
        
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        var hashParts = passwordHash.Split('-');
        var hash = Convert.FromHexString(hashParts[0]);
        var salt = Convert.FromHexString(hashParts[1]);
        
        var inputHash = Rfc2898DeriveBytes
            .Pbkdf2(password, salt, Iterations, Algorithm, KeySize);
        
        return hash.SequenceEqual(inputHash);
    }
}