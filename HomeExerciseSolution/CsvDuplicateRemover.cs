using System;

public class CsvDuplicateRemover
{
    public void Run(string inputPath, string outputPath)
    {
        var lines = File.ReadAllLines(inputPath);

        if (lines.Length == 0)
        {
            Console.WriteLine("Input CSV is empty.");
            return;
        }

        var header = lines[0];
        var uniqueRows = new HashSet<string>();
        var outputLines = new List<string> { header };

        for (int i = 1; i < lines.Length; i++)
        {
            string row = lines[i];
            if (!uniqueRows.Contains(row))
            {
                uniqueRows.Add(row);
                outputLines.Add(row);
            }
        }

        File.WriteAllLines(outputPath, outputLines);
        Console.WriteLine($"Output written to {outputPath}");
    }
}