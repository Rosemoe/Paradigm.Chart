namespace Paradigm.Chart.Objects;

public class SpaceNote(int pulse, bool specialCv, int kind, float x, float y) : Note(pulse, specialCv)
{
 
    public int Kind { get; } = kind;

    public override float X { get; } = x;
    
    public override float Y { get; } = y;
    
}