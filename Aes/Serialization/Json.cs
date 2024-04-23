using Aes.Exceptions;
using System.Text.Json;

namespace Aes.Serialization;

internal static class Json
{
    public static string Serealize<T>(T o)
    {
        if (ReferenceEquals(o, default(T)))
        {
            throw new ArgumentIsNullOrEmptyException("T o");
        }

        return JsonSerializer.Serialize(o);
    }
    public static T Deserealize<T>(string json) where T : class
    {
        if (string.IsNullOrEmpty(json))
        {
            throw new ArgumentNullException("string json");
        }

        return JsonSerializer.Deserialize<T>(json);
    }
}
