using Paradigm.Chart.Objects;

namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class CreateBeatShift : ChartCommandBase<(double start, double bpm, int bpb)>
{
    public override void Execute(ChartParser parser, (double start, double bpm, int bpb) args)
    {
        if (parser.Chart.TimingManager.BeatShiftCount == 0)
        {
            throw new ChartParserException("beat is not initialized");
        }

        int objectPulse = parser.Chart.TimingManager.TimeToPulse(args.start);
        if (objectPulse < parser.Chart.TimingManager.CurrentMaxPulse)
        {
            throw new ChartParserException("trying to create a BeatShift before last BeatShift");
        }
        parser.Chart.TimingManager.AddBeatShift(new BeatShift(objectPulse, args.bpm, Specs.PPQ * args.bpb, parser.BeatShiftIndex++));
    }
    
}