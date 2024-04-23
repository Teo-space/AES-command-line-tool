using Aes.Commands;

if(!args.Any())
{
    return;
}

var commandName = args[0];
List<CommandBase> Commands = [
    new CommandEncrypt("e", "encrypt"), 
    new CommandDecrypt("d", "decrypt")
];

var command = Commands.FirstOrDefault(x => x.CommandKeys.Contains(commandName));

if(command == null)
{
    print($"Команда: {commandName} не найдена");
    return ;
}

string[] commandParams = new string[args.Length - 1];
if(args.Length > 1)
{
    Array.Copy(args, 1, commandParams, 0, args.Length - 1);
}


command.Execute(commandParams);

