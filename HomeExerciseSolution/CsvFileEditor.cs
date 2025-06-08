using System;
using System.Globalization;
using System.IO;

public class CsvFileEditor
{
    public void Run(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("Input CSV file not found.");
            return;
        }

        string outputFilePath = Path.Combine(
            Path.GetDirectoryName(inputFilePath) ?? "",
            Path.GetFileNameWithoutExtension(inputFilePath) + "-edited" + Path.GetExtension(inputFilePath)
        );

        var lines = File.ReadAllLines(inputFilePath);

        if (lines.Length == 0)
        {
            Console.WriteLine("CSV file is empty.");
            return;
        }

        string header = "AccountNumber,LoanId,Name,AmountDue,DateDue,SocialLastFour";
        using var writer = new StreamWriter(outputFilePath);
        writer.WriteLine(header);

        for (int i = 1; i < lines.Length; i++)
        {
            var fields = lines[i].Split(',');

            if (fields.Length < 7)
            {
                Console.WriteLine($"Skipping malformed line {i + 1}");
                continue;
            }

            string accountNumber = fields[0];
            string loanId = fields[1];
            string lastName = fields[2];
            string firstName = fields[3];
            string amountDueCents = fields[4];
            string dateDue = fields[5];
            string socialLastFour = fields[6];

            // Concatenate FirstName and LastName
            string name = $"{firstName} {lastName}";

            // Convert amount from cents to dollars and format as currency
            if (!decimal.TryParse(amountDueCents, out decimal amountCents))
            {
                Console.WriteLine($"Invalid AmountDue on line {i + 1}");
                continue;
            }
            decimal amountDollars = amountCents / 100m;
            string amountFormatted = amountDollars.ToString("C2", CultureInfo.CurrentCulture);

            // Write new CSV line
            string newLine = $"{accountNumber},{loanId},{name},{amountFormatted},{dateDue},{socialLastFour}";
            writer.WriteLine(newLine);
        }

        Console.WriteLine($"Processed file saved to: {outputFilePath}");
    }
}
