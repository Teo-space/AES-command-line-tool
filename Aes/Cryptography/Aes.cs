﻿using Aes.Exceptions;
using Aes.Serialization;
using System.Security.Cryptography;



namespace Aes.Cryptography;


public static class Aes
{
    private static RijndaelManaged CreateAes256Algorithm()
    {
        return new RijndaelManaged { KeySize = 512, BlockSize = 128 };
    }

    public static byte[] Encrypt(byte[] bytes, byte[] key)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException(nameof(bytes));
        }
        if (key == null || key.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException(nameof(key));
        }

        if (key.Length != 32)
        {
            throw new ArgumentException($"Key length does not match the algorithm (32). Length: {key.Length}");
        }

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key = key;
            aes.GenerateIV();

            ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] bytesEncoded = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);

            byte[] result = new byte[aes.IV.Length + bytesEncoded.Length];

            Array.Copy(aes.IV, 0, result, 0, aes.IV.Length);
            Array.Copy(bytesEncoded, 0, result, aes.IV.Length, bytesEncoded.Length);

            return result;
        }
    }


    public static string EncryptFromBytesToBase64(byte[] bytes, string key)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException(nameof(bytes));
        }
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(key));
        }

        var byteKey = Encoding.GetBytes(key);

        if (byteKey.Length != 32)
        {
            throw new ArgumentException("Key length does not match the algorithm");
        }

        var encrypted = Encrypt(bytes, byteKey);

        return Base64.To(encrypted);
    }

    public static string EncryptFromStringToBase64(string data, string key)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(key));
        }
        var byteKey = Encoding.GetBytes(key);
        var bytes = Encoding.GetBytes(data);

        var encrypted = Encrypt(bytes, byteKey);

        return Base64.To(encrypted);
    }








    public static byte[] Decrypt(byte[] bytes, byte[] key)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException(nameof(bytes));
        }
        if (key == null || key.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException(nameof(key));
        }
        if (key.Length != 32)
        {
            throw new ArgumentException("Key length does not match the algorithm");
        }

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            byte[] vector = new byte[aes.BlockSize / 8];
            Array.Copy(bytes, 0, vector, 0, vector.Length);

            byte[] bytesEncoded = new byte[bytes.Length - vector.Length];
            Array.Copy(bytes, vector.Length, bytesEncoded, 0, bytesEncoded.Length);

            aes.Key = key;
            aes.IV = vector;

            ICryptoTransform cryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] result = cryptoTransform.TransformFinalBlock(bytesEncoded, 0, bytesEncoded.Length);

            return result;
        }
    }


    public static string DecryptFromBase64ToString(string data, string key)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(key));
        }

        var byteKey = Encoding.GetBytes(key);
        var bytes = Base64.BytesFrom(data);

        var decrypted = Decrypt(bytes, byteKey);

        return Encoding.GetString(decrypted);
    }

    public static byte[] DecryptFromBase64ToBytes(string data, string key)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(key));
        }

        var byteKey = Encoding.GetBytes(key);
        var bytes = Base64.BytesFrom(data);

        return Decrypt(bytes, byteKey);
    }








}
