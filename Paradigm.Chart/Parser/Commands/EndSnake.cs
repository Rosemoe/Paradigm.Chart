namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class EndSnake : ChartCommandBase
{

    protected override void Execute(ChartParser parser)
    {
        if (parser.CurrentSnake == null)
        {
            throw new ChartParserException("redundant EndSnake");
        }
        parser.Chart.SnakeGroups.Add(parser.CurrentSnake);
        parser.CurrentSnake = null;
    }
    
}