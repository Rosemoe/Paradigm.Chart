namespace Paradigm.Chart;

public static class Specs
{

    public const string Sides = "LRUD";

    public const float OutOfSideRangeTolerance = 0.02f;
    
    public const int PPQ = 5040;
    
    /// <summary>
    /// Edge note side size.
    /// </summary>
    public static int SideSize(int edge)
    {
        if (edge > 1)
        {
            // Up = 2
            // Down = 3
            return 12;
        }

        // Left = 0
        // Right = 1
        return 9;
    }

}