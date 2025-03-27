using System.Reflection;
using Paradigm.Chart.Objects;

namespace Paradigm.Chart.Parser;

public class ChartParser
{
    public int PulseOffset { get; set; }
    
    public int BeatShiftIndex { get; set; }

    public SnakeGroup? CurrentSnake { get; set; }

    public Chart Chart { get; } = new ();
    
    private Dictionary<Tuple<string, int>, IChartCommand> Commands { get; } = new();

    public ChartParser()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var commands = assembly.GetTypes().Where(type => type.GetCustomAttribute<RegisterCommand>() != null);
        foreach (var command in commands)
        {
            var instance = (IChartCommand) command.GetConstructors()[0].Invoke([]);
            foreach (var name in instance.Names)
            {
                Commands[Tuple.Create(name, instance.ArgumentCount)] = instance;
            }
        }
    }

    public void ExecuteCommand(string name, string[] args)
    {
        int argumentCount = args.Length;
        var signature = new Tuple<string, int>(name, argumentCount);
        if (!Commands.TryGetValue(signature, out var command))
        {
            Console.WriteLine($"Unknown command: {name}({argumentCount})");
        }
        else
        {
            command.Invoke(this, args);
        }
    }

    public void Parse(string chart)
    {
        var lines = chart.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var statement = line.Split(",");
            if (statement.Length == 0)
            {
                continue;
            }
            var name = statement[0].Trim();
            var arguments = statement.TakeLast(statement.Length - 1).ToArray();
            ExecuteCommand(name, arguments);
        }
    }
    
}