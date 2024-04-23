namespace Aes.Commands;

internal abstract record CommandBase(params string[] CommandKeys)
{
    public abstract void Execute(params string[] commandParams);

}