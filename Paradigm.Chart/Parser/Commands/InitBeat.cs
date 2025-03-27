using Paradigm.Chart.Objects;

namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class InitBeat : ChartCommandBase<(double bpm, int bpb)>
{
    public override void Execute(ChartParser parser, (double bpm, int bpb) args)
    {
        if (parser.Chart.TimingManager.BeatShiftCount > 0)
        {
            throw new ChartParserException("beat is already initialized");
        }
        parser.Chart.TimingManager.AddBeatShift(new BeatShift(0, args.bpm, Specs.PPQ * args.bpb, parser.BeatShiftIndex++));
    }
    
}