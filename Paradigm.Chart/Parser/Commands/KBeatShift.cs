namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class KBeatShift : ChartCommandBase<(double start, double bpm, int bpb)>
{
    public override void Execute(ChartParser parser, (double start, double bpm, int bpb) args)
    {
        new CreateBeatShift().Execute(parser, args);
        new ActivateBeatShift().Execute(parser, parser.Chart.TimingManager.BeatShiftCount - 2);
    }
    
}