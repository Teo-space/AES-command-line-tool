namespace Aes.Exceptions;

internal class ArgumentIsNullOrEmptyException(string argumentName) : ArgumentException($"string {argumentName} is null or empty");
