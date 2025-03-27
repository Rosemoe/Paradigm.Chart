namespace Paradigm.Chart.Objects;

public class SnakeGroup : ChartObject
{
    public SortedSet<SnakePoint> Points { get; } = new();

    public List<double> GetJudgeTimings(Chart chart, ChartHighlightState state)
    {
        var timings = new List<double>();
        var previousPoint = Points.First();
        foreach (var point in Points.Skip(1))
        {
            var skipFirst = Points.First() == previousPoint && state.ShouldSnakeSkipFirst(this);
            var result = GetJudgeTimings(chart, previousPoint, point, skipFirst, Points.Last() == point);
            timings.AddRange(result);
            previousPoint = point;
        }

        return timings;
    }

    private static List<double> GetJudgeTimings(Chart chart, SnakePoint p1, SnakePoint p2, bool skipFirst, bool isLast)
    {
        var result = new List<double>();
        var manager = chart.TimingManager;
        double offsetTime = manager.PulseToTime(p1.Pulse + Specs.PPQ / 2);
        double noteTime = manager.PulseToTime(p1.Pulse);
        float intervalTime = (float)(offsetTime - noteTime);
        float limitTime = manager.PulseToTimeFloat(p2.Pulse);
        if (isLast)
        {
            limitTime = Math.Max(limitTime - intervalTime + 0.01f, manager.PulseToTimeFloat(p1.Pulse) + 0.01f);
        }

        float currentTime = manager.PulseToTimeFloat(p1.Pulse);
        while (currentTime <= limitTime)
        {
            result.Add(currentTime);
            currentTime += intervalTime;
        }

        if (skipFirst && result.Count >= 2)
        {
            result.RemoveAt(0);
        }

        return result;
    }
}