namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class SpeedChangeQ : ChartCommandBase<(double startTime, double duration, float startSpeed, float endSpeed, float curvature)>
{
    public override void Execute(ChartParser parser,
        (double startTime, double duration, float startSpeed, float endSpeed, float curvature) args)
    {
        
    }
}