using System;

public class AchToggleLineEndings
{
    public void Run(string filePath)
    {
        string content = File.ReadAllText(filePath);

        string normalized = content.Replace("\r", "").Replace("\n", "");

        if (content.Contains("\n"))
        {
            File.WriteAllText(filePath, normalized);
            Console.WriteLine("Line breaks removed.");
        }
        else
        {
            var result = InsertLineBreaksEvery94Chars(normalized);
            File.WriteAllText(filePath, result);
            Console.WriteLine("Line breaks inserted.");
        }
    }

    private string InsertLineBreaksEvery94Chars(string input)
    {
        var lines = new List<string>();
        for (int i = 0; i < input.Length; i += 94)
        {
            int length = Math.Min(94, input.Length - i);
            lines.Add(input.Substring(i, length));
        }
        return string.Join("\n", lines);
    }
}