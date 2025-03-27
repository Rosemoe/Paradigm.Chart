using Paradigm.Chart.Objects;

namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class InitBeatEx : ChartCommandBase<(double bpm, int ppb, int hideBars)>
{
    public override void Execute(ChartParser parser, (double bpm, int ppb, int hideBars) args)
    {
        if (parser.Chart.TimingManager.BeatShiftCount > 0)
        {
            throw new ChartParserException("InitBeat can be used only once");
        }
        parser.Chart.TimingManager.AddBeatShift(new BeatShift(0, args.bpm, args.ppb, args.hideBars));
    }
    
}