namespace Paradigm.Chart.Parser.Commands;

[RegisterCommand]
public class SpaceNoteCV : SpaceNote
{

    public SpaceNoteCV()
    {
        SpecialCV = true;
    }
    
    protected override string[] GetArgumentNames()
    {
        return ["SCV"];
    }
}