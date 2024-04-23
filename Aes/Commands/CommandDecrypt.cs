namespace Aes.Commands;

using Aes.Cryptography;

internal record CommandDecrypt(params string[] CommandKeys) : CommandBase(CommandKeys)
{
    public override void Execute(params string[] commandParams)
    {
        if (commandParams.Length != 2)
        {
            print($"Команда Decrypt ожидает на входе 2 параметра:");
            print($"1) Key");
            print($"2) Data for decrypt");
            return;
        }

        var key = commandParams[0];
        var value = commandParams[1];

        if (key.Length != 32)
        {
            key = MD5.Compute(key);
        }

        var result = Aes.DecryptFromBase64ToString(value, key);

        print("Result:");
        print(result);
        Console.WriteLine();
    }
}
