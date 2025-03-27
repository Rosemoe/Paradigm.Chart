namespace Paradigm.Chart.Objects;

public class BeatShift(int changePulse, double bpm, int pulsePerBeat, int beatShiftIndex, int hideBars = 0) : ChartObject, IComparable<BeatShift>
{
    public int ChangePulse { get; set; } = changePulse;

    public double Bpm { get; set; } = bpm;

    public int PulsePerBeat { get; set; } = pulsePerBeat;
    
    public bool HideBars { get; set; } = hideBars != 0;

    private int BeatShiftIndex { get; set; } = beatShiftIndex;

    public int TimeToPulse(double time)
    {
        return (int) Math.Round(Bpm * time * 84.0);
    }

    public double PulseToTime(int pulses)
    {
        return pulses / 84.0 / Bpm;
    }

    public int CompareTo(BeatShift? other)
    {
        if (other is null)
        {
            return 1;
        }
        int result = ChangePulse.CompareTo(other.ChangePulse);
        return result != 0 ? result : BeatShiftIndex.CompareTo(other.BeatShiftIndex);
    }
}