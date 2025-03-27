using Paradigm.Chart.Objects;

namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class SnakeSubQ : ChartCommandBase<(float x, float y, float curvature, int tick)>
{
    protected bool SpecialCV { get; init; }
    
    protected override string[] GetArgumentNames()
    {
        return ["SQ", "SnakeSubQ"];
    }

    public override void Execute(ChartParser parser, (float x, float y, float curvature, int tick) args)
    {
        if (parser.CurrentSnake == null)
        {
            throw new ChartParserException("trying to add snake point before starting a snake");
        }
        
        int pulse = parser.PulseOffset + args.tick;
        parser.CurrentSnake.Points.Add(new SnakePoint(pulse, SpecialCV, args.x, args.y, args.curvature));
    }
    
}