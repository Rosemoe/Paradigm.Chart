namespace Paradigm.Chart.Objects;

public class SnakePoint(int pulse, bool specialCv, float x, float y, float curvature) : Note(pulse, specialCv), IComparable<SnakePoint>
{
    
    public override float X { get; } = x;
    
    public override float Y { get; } = y;
    
    public float Curvature { get; } = curvature;

    public int CompareTo(SnakePoint? other)
    {
        if (other == null) return 1;
        return Pulse.CompareTo(other.Pulse);
    }
}