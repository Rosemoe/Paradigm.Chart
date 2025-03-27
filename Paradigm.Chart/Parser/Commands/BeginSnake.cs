using Paradigm.Chart.Objects;

namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class BeginSnake : ChartCommandBase
{

    protected override void Execute(ChartParser parser)
    {
        if (parser.CurrentSnake != null)
        {
            throw new ChartParserException("trying to begin a new snake before last snake is committed");
        }

        parser.CurrentSnake = new SnakeGroup();
    }
    
}