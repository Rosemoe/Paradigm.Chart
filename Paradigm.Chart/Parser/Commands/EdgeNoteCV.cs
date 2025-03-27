namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class EdgeNoteCV : EdgeNote
{

    public EdgeNoteCV()
    {
        SpecialCV = true;
    }
    
    protected override string[] GetArgumentNames()
    {
        return ["ECV"];
    }
    
}