using System.Security.Cryptography;
using static OnlineShop.Db.Helpers.FileHelper;

namespace OnlineShop.Db.Helpers;

public static class StringExtensions
{
    private static Aes AES { get; } = Aes.Create();

    public static string Encrypt(this string password)
    {
        using var aes = Aes.Create();
        if (Load<byte>("Key.json") == Array.Empty<byte>() || Load<byte>("IV.json") == Array.Empty<byte>())
        {
            aes.Key = AES.Key;
            aes.IV = AES.IV;
            Save(aes.Key, "Key.json");
            Save(aes.IV, "IV.json");
        }
        else
        {
            aes.Key = Load<byte>("Key.json");
            aes.IV = Load<byte>("IV.json");
        }

        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(password);
        }

        var encrypted = Convert.ToBase64String(memoryStream.ToArray());

        return encrypted;
    }

    public static string Decrypt(this string encryptedPassword)
    {
        using var aes = Aes.Create();
        if (Load<byte>("Key.json") == Array.Empty<byte>() || Load<byte>("IV.json") == Array.Empty<byte>())
        {
            aes.Key = AES.Key;
            aes.IV = AES.IV;
            Save(aes.Key, "Key.json");
            Save(aes.IV, "IV.json");
        }
        else
        {
            aes.Key = Load<byte>("Key.json");
            aes.IV = Load<byte>("IV.json");
        }

        try
        {
            var buffer = Convert.FromBase64String(encryptedPassword);
        }
        catch (FormatException)
        {
            encryptedPassword = encryptedPassword.Encrypt();
        }

        using var memoryStream = new MemoryStream(Convert.FromBase64String(encryptedPassword));
        using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        var decrypted = streamReader.ReadToEnd();

        return decrypted;
    }
}