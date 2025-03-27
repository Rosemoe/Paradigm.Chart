namespace Paradigm.Chart.Objects;

public abstract class Note(int pulse, bool isSpecialCv) : ChartObject
{

    public int Pulse { get; set; } = pulse;

    public bool SpecialCv { get; set; } = isSpecialCv;
    
    public abstract float X { get; }
    
    public abstract float Y { get; }

    public float NormalizedX => X / 12.0f;
    
    public float NormalizedY => Y / 9.0f;
    
}