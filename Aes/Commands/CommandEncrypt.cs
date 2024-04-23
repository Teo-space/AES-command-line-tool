namespace Aes.Commands;

using Aes.Cryptography;

internal record CommandEncrypt(params string[] CommandKeys) : CommandBase(CommandKeys)
{
    public override void Execute(params string[] commandParams)
    {
        if(commandParams.Length != 2)
        {
            print($"Команда Encrypt ожидает на входе 2 параметра:");
            print($"1) Key");
            print($"2) Data for encrypt");
            return;
        }
        var key = commandParams[0];
        var value = commandParams[1];

        if (key.Length != 32)
        {
            key = MD5.Compute(key);
        }

        var result = Aes.EncryptFromStringToBase64(value, key);

        print("Result:");
        print(result);
        Console.WriteLine();
    }
}
