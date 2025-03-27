namespace Paradigm.Chart.Parser;

public interface IChartCommand
{

    string[] Names => [];
    
    int ArgumentCount => 0;

    void Invoke(ChartParser parser, string[] args);
    
}