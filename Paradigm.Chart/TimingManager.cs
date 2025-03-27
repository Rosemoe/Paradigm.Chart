using Paradigm.Chart.Objects;

namespace Paradigm.Chart;

public class TimingManager
{

    /// <summary>
    /// BeatShiftEvent to its double time
    /// </summary>
    private readonly SortedList<BeatShift, double> _beatShifts = new();
    
    private static readonly BeatShift EmptyShift = new(0, 120.0, 0, 0);

    public void AddBeatShift(BeatShift beatShift)
    {
        var previous = _beatShifts.Keys.LastOrDefault(EmptyShift);
        var previousTime = _beatShifts.Count == 0 ? 0.0 : _beatShifts.Last().Value;
        var deltaTime = (beatShift.ChangePulse - previous.ChangePulse) / previous.Bpm / (Specs.PPQ / 60.0);
        var time = previousTime + deltaTime;
        _beatShifts.Add(beatShift, time);
    }
    
    public int CurrentMaxPulse => _beatShifts.Count == 0 ? 0 : _beatShifts.Last().Key.ChangePulse;
    
    public int BeatShiftCount => _beatShifts.Count;

    public BeatShift GetBeatShift(int index)
    {
        return _beatShifts.ElementAt(index).Key;
    }

    public int TimeToPulse(double time)
    {
        var previous = _beatShifts.LastOrDefault((x) => x.Value <= time, _beatShifts.First());
        var deltaTime = time - previous.Value;
        return previous.Key.TimeToPulse(deltaTime) + previous.Key.ChangePulse;
    }

    public double PulseToTime(int pulse)
    {
        var previous = _beatShifts.LastOrDefault((x) => x.Key.ChangePulse <= pulse, _beatShifts.First());
        var deltaPulse = pulse - previous.Key.ChangePulse;
        return previous.Key.PulseToTime(deltaPulse) + previous.Value;
    }

    public float PulseToTimeFloat(int pulse)
    {
        return (float) PulseToTime(pulse);
    }

}