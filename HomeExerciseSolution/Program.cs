using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Swivel Homework Exercises ===\n");
        Console.WriteLine("Select an option:");
        Console.WriteLine("1. ACH - Find Invalid Characters");
        Console.WriteLine("2. ACH - Toggle Line Endings");
        Console.WriteLine("3. CSV - Remove Duplicate Records");
        Console.WriteLine("4. CSV - Convert to Fixed Width");
        Console.WriteLine("5. CSV - File Editor (Merge Names & Format)");
        Console.WriteLine("6. Fix Homoglyph Characters");
        Console.WriteLine("7. XKCD Web Scraper");

        Console.Write("\nEnter your choice (1-7): ");
        string choice = Console.ReadLine();

        Console.Clear();

        switch (choice)
        {
            case "1":
                Console.WriteLine("ACH - Find Invalid Characters");
                Console.Write("Enter ACH file path: ");
                string achPath = Console.ReadLine();
                new FindInvalidCharacters().Run(achPath);
                break;

            case "2":
                Console.WriteLine("ACH - Toggle Line Endings");
                Console.Write("Enter ACH file path: ");
                string togglePath = Console.ReadLine();
                new AchToggleLineEndings().Run(togglePath);
                break;

            case "3":
                Console.WriteLine("CSV - Duplicate Record Remover");
                Console.Write("Enter input CSV file path: ");
                string dupInput = Console.ReadLine();
                Console.Write("Enter output CSV file path: ");
                string dupOutput = Console.ReadLine();
                new CsvDuplicateRemover().Run(dupInput, dupOutput);
                break;

            case "4":
                Console.WriteLine("CSV - To Fixed Width Converter");
                Console.Write("Enter input CSV file path: ");
                string fixedInput = Console.ReadLine();
                Console.Write("Enter output TXT file path: ");
                string fixedOutput = Console.ReadLine();
                new CsvToFixedWidth().Run(fixedInput, fixedOutput);
                break;

            case "5":
                Console.WriteLine("CSV - File Editor");
                Console.Write("Enter input CSV file path: ");
                string editorInput = Console.ReadLine();
                new CsvFileEditor().Run(editorInput);
                break;

            case "6":
                Console.WriteLine("Fix Homoglyphs");
                Console.Write("Enter input file path: ");
                string homoInput = Console.ReadLine();
                Console.Write("Enter output file path (leave empty to use default): ");
                string homoOutput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(homoOutput))
                    homoOutput = homoInput.Replace(".txt", "_fixed.txt");
                new FixHomoglyphs().Run(new string[] { homoInput, homoOutput });
                break;

            case "7":
                Console.WriteLine("XKCD Comic Scraper Starting...");

                await new XKCDScraper().RunAsync();
                break;

            default:
                Console.WriteLine("Invalid choice. Please run again.");
                break;
        }

        Console.WriteLine("\nDone. Press any key to exit...");
        Console.ReadKey();
    }
}
