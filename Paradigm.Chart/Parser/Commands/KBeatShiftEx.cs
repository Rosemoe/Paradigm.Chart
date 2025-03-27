namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class KBeatShiftEx : ChartCommandBase<(double start, double bpm, int ppb, int hideBars)>
{
    public override void Execute(ChartParser parser, (double start, double bpm, int ppb, int hideBars) args)
    {
        new CreateBeatShiftEx().Execute(parser, args);
        new ActivateBeatShift().Execute(parser, parser.Chart.TimingManager.BeatShiftCount - 2);
    }
    
}