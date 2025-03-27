namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class SpaceNote : ChartCommandBase<(int kind, float x, float y, int tick)>
{
    protected bool SpecialCV { get; init; }
    
    protected override string[] GetArgumentNames()
    {
        return ["S", "SpaceNote"];
    }
    
    public override void Execute(ChartParser parser, (int kind, float x, float y, int tick) args)
    {
        if (args.kind >= 2 || args.kind < 0)
        {
            throw new ChartParserException("invalid note kind");
        }
        
        int pulse = parser.PulseOffset + args.tick;
        parser.Chart.Notes.Add(new Objects.SpaceNote(pulse, SpecialCV, args.kind, args.x, args.y));
    }
}