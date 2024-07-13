
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Backend_developer_Test.Helper
{
    public static class AesEncryption
    {

        private static readonly byte[] Key = Encoding.UTF8.GetBytes("767654321234567890987654321234567");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("0990987656785432");

        // Hardcoded AES key (example, replace with your own key)
        private static readonly byte[] aesKeyBytes = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef");

        public static byte[] GetAESKey()
        {
            // Ensure the key size is valid (128, 192, or 256 bits)
            if (aesKeyBytes.Length != 16 && aesKeyBytes.Length != 24 && aesKeyBytes.Length != 32)
            {
                throw new ArgumentException("AES key length must be 16, 24, or 32 bytes (128, 192, or 256 bits).");
            }

            return aesKeyBytes;
        }

        public static string EncryptString(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = GetAESKey();
            aes.IV = IV;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);

            var encrypted = ms.ToArray();
            return Convert.ToBase64String(encrypted);
        }
    }
}

