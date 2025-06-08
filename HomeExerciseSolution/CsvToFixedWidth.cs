using System;

public class CsvToFixedWidth
{
        public void Run(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            var lines = File.ReadAllLines(inputPath);
            if (lines.Length == 0)
            {
                Console.WriteLine("CSV is empty.");
                return;
            }

            List<string> outputLines = new List<string>();

            foreach (var line in lines)
            {
                var fields = line.Split(',');
                string fixedLine = "";

                foreach (var field in fields)
                {
                    fixedLine += FormatField(field);
                }

                outputLines.Add(fixedLine);
            }

            File.WriteAllLines(outputPath, outputLines);
            Console.WriteLine($"Fixed width file written to: {outputPath}");
        }

        private string FormatField(string field)
        {
            return field.Length > 9 ? field.Substring(0, 9) : field.PadRight(9);
        }
}
