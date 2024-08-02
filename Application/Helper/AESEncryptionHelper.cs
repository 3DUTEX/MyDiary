using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Application.Providers;

public static class AESEncryptionHelper
{
    private static readonly string _encryptionKey = Environment.GetEnvironmentVariable("AES_ENCRYPTION_KEY")
        ?? throw new ArgumentNullException("<AES_ENCRYPTION_KEY> is missing variable!");

    /// <summary>
    /// Decrypt in AES Encryption
    /// </summary>
    /// <param name="content">content should be decrypted</param>
    /// <returns>content decrypted</returns>
    public static string Decrypt(string content)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentNullException("<content> param is required!");

        var key = Encoding.UTF8.GetBytes(_encryptionKey);

        string textDecrypted;

        var textEncrypted = Convert.FromBase64String(content);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = new byte[16];

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(textEncrypted);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);

            textDecrypted = srDecrypt.ReadToEnd();
        }

        return textDecrypted;
    }

    /// <summary>
    /// Ecnrypt in AES Encryption
    /// </summary>
    /// <param name="content">content should be encrypted</param>
    /// <returns>content encrypted in Base64</returns>
    public static string Encrypt(string content)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentNullException("<content> param is required!");

        var key = Encoding.UTF8.GetBytes(_encryptionKey);

        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = new byte[16];

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();

            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(content);
            }

            encrypted = msEncrypt.ToArray();
        }

        var encryptedBase64 = Convert.ToBase64String(encrypted);

        return encryptedBase64;
    }
}