namespace Paradigm.Chart.Objects;

public class EdgeNote(int pulse, bool specialCv, int kind, int edge, float pos) : Note(pulse, specialCv)
{

    public int Kind { get; } = kind;
    
    public int Edge { get; } = edge;

    public float Position { get;} = pos;

    public override float X
    {
        get
        {
            return Edge switch
            {
                0 => 0.0f,
                1 => 12.0f,
                _ => Position
            };
        }
    }

    public override float Y
    {
        get
        {
            return Edge switch
            {
                2 => 9.0f,
                3 => 0.0f,
                _ => Position
            };
        }
    }
    
}