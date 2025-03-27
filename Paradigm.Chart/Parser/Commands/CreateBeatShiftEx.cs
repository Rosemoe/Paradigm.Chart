using Paradigm.Chart.Objects;

namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class CreateBeatShiftEx : ChartCommandBase<(double start, double bpm, int ppb, int hideBars)>
{
    public override void Execute(ChartParser parser, (double start, double bpm, int ppb, int hideBars) args)
    {
        if (parser.Chart.TimingManager.BeatShiftCount == 0)
        {
            throw new ChartParserException("InitBeat is not called");
        }

        int objectPulse = parser.Chart.TimingManager.TimeToPulse(args.start);
        if (objectPulse > parser.Chart.TimingManager.CurrentMaxPulse)
        {
            throw new ChartParserException("trying to create a BeatShift before last BeatShift");
        }
        parser.Chart.TimingManager.AddBeatShift(new BeatShift(objectPulse, args.bpm,  args.ppb, args.hideBars));
    }
}