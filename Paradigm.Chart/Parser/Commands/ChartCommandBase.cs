using System.Runtime.CompilerServices;

namespace Paradigm.Chart.Parser.Commands;

public abstract class ChartCommandBase : IChartCommand
{
    public string[] Names => [GetType().Name];

    public int ArgumentCount => 0;

    protected abstract void Execute(ChartParser parser);

    public void Invoke(ChartParser parser, string[] args)
    {
        if (args.Length != ArgumentCount)
        {
            throw new ArgumentException($"invalid argument length, expected {ArgumentCount}, got {args.Length}");
        }
        Execute(parser);
    }
}

public abstract class ChartCommandBase<T> : IChartCommand
{
    public string[] Names => GetArgumentNames();

    public int ArgumentCount => GetArgumentCount();

    public abstract void Execute(ChartParser parser, T args);

    protected virtual string[] GetArgumentNames()
    {
        return [GetType().Name];
    }

    private int GetArgumentCount()
    {
        if (typeof(T).GetInterface(nameof(ITuple)) == null)
        {
            return 1;
        }
        return typeof(T).GenericTypeArguments.Length;
    }
    
    private T GetConvertedArguments(string[] args)
    {
        if (typeof(T).GetInterface(nameof(ITuple)) == null)
        {
            return (T) Convert.ChangeType(args[0], typeof(T));
        }
        var constructor = typeof(T).GetConstructors()[0];
        var parameters = new object[args.Length];
        var parameterTypes = constructor.GetParameters();
        for (int i = 0; i < args.Length; i++)
        {
            parameters[i] = Convert.ChangeType(args[i], parameterTypes[i].ParameterType);
        }

        return (T)constructor.Invoke(parameters);
    }

    public void Invoke(ChartParser parser, string[] args)
    {
        if (args.Length != ArgumentCount)
        {
            throw new ArgumentException($"invalid argument length, expected {ArgumentCount}, got {args.Length}");
        }
        Execute(parser, GetConvertedArguments(args));
    }
    
}