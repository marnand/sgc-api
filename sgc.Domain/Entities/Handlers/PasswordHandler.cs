using System.Security.Cryptography;
using System.Text;

namespace sgc.Domain.Entities.Handlers;

public class PasswordHandler
{
    private const int SaltSize = 16; // Tamanho do salt em bytes
    private const int HashSize = 32; // Tamanho do hash em bytes
    private const int Iterations = 10000; // Número de iterações para PBKDF2

    /// <summary>
    /// Gera um hash seguro para a senha fornecida.
    /// </summary>
    /// <param name="password">A senha para ser encriptada.</param>
    /// <returns>Uma string no formato SALT|HASH.</returns>
    public static string HashPassword(string password)
    {
        // Gera um salt aleatório
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Gera o hash da senha usando PBKDF2
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize
        );

        // Retorna o salt e o hash concatenados em Base64
        return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Verifica se a senha fornecida corresponde ao hash armazenado.
    /// </summary>
    /// <param name="password">A senha a ser validada.</param>
    /// <param name="storedHash">O hash armazenado no formato SALT|HASH.</param>
    /// <returns>Verdadeiro se a senha for válida, falso caso contrário.</returns>
    public static (bool, string) VerifyPassword(string password, string storedHash)
    {
        // Divide o hash armazenado em SALT e HASH
        var parts = storedHash.Split('|');
        if (parts.Length != 2)
            return (false, "Formato do hash armazenado é inválido.");

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedPasswordHash = Convert.FromBase64String(parts[1]);

        // Gera o hash da senha fornecida com o mesmo salt
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize
        );

        // Compara os hashes
        var isValid = CryptographicOperations.FixedTimeEquals(hash, storedPasswordHash);

        return (isValid, isValid ? "Sucesso!" : "Senha inválida!");
    }
}
