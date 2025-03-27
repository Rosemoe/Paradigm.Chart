namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class EdgeNote : ChartCommandBase<(int kind, int edge, float pos, int tick)>
{

    protected bool SpecialCV { get; init; }
    
    protected override string[] GetArgumentNames()
    {
        return ["E", "EdgeNote"];
    }

    public override void Execute(ChartParser parser, (int kind, int edge, float pos, int tick) args)
    {
        if (args.kind >= 2 || args.kind < 0)
        {
            throw new ChartParserException("invalid note kind");
        }

        if (args.edge >= Specs.Sides.Length || args.edge < 0)
        {
            throw new ChartParserException("invalid edge");
        }
        
        int pulse = parser.PulseOffset + args.tick;
        var note = new Objects.EdgeNote(pulse, SpecialCV, args.kind, args.edge, args.pos);
        parser.Chart.Notes.Add(note);
    }
}