using System.Diagnostics;
using Paradigm.Chart.Objects;

namespace Paradigm.Chart;

public class Chart
{
    public TimingManager TimingManager { get; } = new();

    public List<Note> Notes { get; } = new();

    public List<SnakeGroup> SnakeGroups { get; } = new();

    public double StartTime { get; set; } = 0.0;

    public double Offset { get; set; } = 0.0;


    public int CalculateNoteCount()
    {
        var count = Notes.Count;
        var state = new ChartHighlightState();
        foreach (var note in Notes)
        {
            var kind = 0;
            if (note is EdgeNote edgeNote) kind = edgeNote.Kind;
            if (note is SpaceNote spaceNote) kind = spaceNote.Kind;
            state.Add(note.Pulse, kind, note);
        }
        foreach (var snakeGroup in SnakeGroups)
        {
            state.Add(snakeGroup.Points.First().Pulse, 4, snakeGroup.Points.First());
        }
        
        foreach (var snakeGroup in SnakeGroups)
        {
            count += snakeGroup.GetJudgeTimings(this, state).Count;
        }

        return count;
    }
}

public class ChartHighlightState
{
    public SortedList<int, List<Note>> HighlightLists { get; } = new();

    public SortedList<int, List<Note>> HighlightCandidateLists { get; } = new();

    public HashSet<int> HighlightTapTimings { get; } = new();

    public void Add(int pulse, int kind, Note note)
    {
        kind |= 2;
        for (int currentPulse = pulse - 1; currentPulse <= pulse + 1; currentPulse++)
        {
            if (!HighlightLists.ContainsKey(currentPulse))
            {
                HighlightLists[currentPulse] = new List<Note>();
                HighlightCandidateLists[currentPulse] = new List<Note>();
            }

            if (kind == 2)
            {
                HighlightLists[currentPulse].Add(note);
                HighlightTapTimings.Add(currentPulse);
                foreach (var candidate in HighlightCandidateLists[currentPulse])
                {
                    bool shouldAdd = false;
                    foreach (var inList in HighlightLists[currentPulse])
                    {
                        if (Math.Abs(inList.NormalizedX - candidate.NormalizedX) < 0.01 &&
                            Math.Abs(inList.NormalizedY - candidate.NormalizedY) < 0.01)
                        {
                            shouldAdd = true;
                            break;
                        }
                    }

                    if (shouldAdd)
                    {
                        HighlightLists[currentPulse].Add(candidate);
                    }
                }

                HighlightCandidateLists[currentPulse].Clear();
            }
            else
            {
                SortedList<int, List<Note>> target = HighlightCandidateLists;
                bool skip = false;
                if (HighlightTapTimings.Contains(currentPulse))
                {
                    foreach (var inList in HighlightLists[currentPulse])
                    {
                        if (Math.Abs(inList.NormalizedX - note.NormalizedX) < 0.01 &&
                            Math.Abs(inList.NormalizedY - note.NormalizedY) < 0.01)
                        {
                            skip = true;
                            break;
                        }
                    }

                    target = HighlightLists;
                }

                if (!skip)
                {
                    target[currentPulse].Add(note);
                }
            }
        }
    }


    public bool ShouldSnakeSkipFirst(SnakeGroup snakeGroup)
    {
        var first = snakeGroup.Points.First();
        var pulse = first.Pulse;
        var success = HighlightLists.TryGetValue(pulse, out var notes);
        if (!success || notes == null)
        {
            return false;
        }

        foreach (var note in notes)
        {
            if (note == first) continue;
            if (Math.Abs(note.NormalizedX - first.NormalizedX) < 0.01 &&
                Math.Abs(note.NormalizedY - first.NormalizedY) < 0.01)
            {
                return true;
            }
        }

        return false;
    }
}