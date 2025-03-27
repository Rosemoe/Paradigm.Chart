using Paradigm.Chart.Parser;

int CalculateNoteCount(string path)
{
    var parser = new ChartParser();
    var chartText = File.ReadAllText(path);
    parser.Parse(chartText);
    return parser.Chart.CalculateNoteCount();
}

// 'songs' directory path from paradigm-reboot-extractor
var extractorSongsDir = "./songs";

var dirs = Directory.GetDirectories(extractorSongsDir);
var result = new Dictionary<string, Dictionary<string, int>> ();
foreach (var dir in dirs)
{
    var songId = Path.GetFileName(dir);
    result.Add(songId, new Dictionary<string, int>());
    var charts = Directory.GetFiles(dir, $"*.txt", SearchOption.TopDirectoryOnly);
    foreach (var chartPath in charts)
    {
        var difficulty = Path.GetFileNameWithoutExtension(chartPath)!;
        result[songId][difficulty] = CalculateNoteCount(chartPath);
        Console.WriteLine($"{songId} {difficulty} {result[songId][difficulty]}");
    }
}


