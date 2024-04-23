using Aes.Exceptions;

namespace Aes.Serialization;


internal static class Encoding
{
    public readonly static System.Text.Encoding encoding = System.Text.Encoding.UTF8;

    public static byte[] GetBytes(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException("string data");
        }
        return encoding.GetBytes(data);
    }

    public static string GetString(byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentIsNullOrEmptyException("byte[] bytes");
        }
        return encoding.GetString(bytes);
    }
}
