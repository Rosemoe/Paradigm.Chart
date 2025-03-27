namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class Offset : ChartCommandBase<double>
{
    public override void Execute(ChartParser parser, double offset)
    {
        parser.Chart.Offset = offset;
    }
}