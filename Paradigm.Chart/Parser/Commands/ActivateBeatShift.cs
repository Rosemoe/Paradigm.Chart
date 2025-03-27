namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class ActivateBeatShift : ChartCommandBase<int>
{
    public override void Execute(ChartParser parser, int index)
    {
        if (index < -1 || parser.Chart.TimingManager.BeatShiftCount - 1 <= index)
        {
            throw new ChartParserException("invalid BeatShift index");
        }
        parser.PulseOffset = parser.Chart.TimingManager.GetBeatShift(index + 1).ChangePulse;
    }
}