namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class SnakeSubQCV : SnakeSubQ
{

    public SnakeSubQCV()
    {
        SpecialCV = true;
    }
    
    protected override string[] GetArgumentNames()
    {
        return ["SQCV"];
    }

}