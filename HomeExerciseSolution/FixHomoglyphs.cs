using System;
using System.IO;
using System.Text;

public class FixHomoglyphs
{
    public void Run(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide input file path as first argument.");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "-fixed" + Path.GetExtension(inputPath));

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Input file does not exist.");
            return;
        }

        string content = File.ReadAllText(inputPath, Encoding.UTF8);

        var fixedContent = new StringBuilder(content.Length);

        foreach (char c in content)
        {
            if (HomoglyphMap.Map.TryGetValue(c, out char replacement))
            {
                fixedContent.Append(replacement);
            }
            else
            {
                fixedContent.Append(c);
            }
        }

        File.WriteAllText(outputPath, fixedContent.ToString(), Encoding.UTF8);
        Console.WriteLine($"File processed. Output written to: {outputPath}");
    }
}
