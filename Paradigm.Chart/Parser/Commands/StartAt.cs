namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class StartAt : ChartCommandBase<double>
{
    public override void Execute(ChartParser parser, double time)
    {
        parser.Chart.StartTime = time;
    }
}