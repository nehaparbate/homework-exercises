using System;
using System.IO;
using System.Text.RegularExpressions;

public class FindInvalidCharacters
{
    public void Run(string filePath)
    {
        string content = File.ReadAllText(filePath);

        string pattern = @"[a-zA-Z0-9_\-:\.@\$=\/ ]"; 

        Regex regex = new Regex(pattern);

        for (int i = 0; i < content.Length; i++)
        {

            char c = content[i];

            if (c == '\r' || c == '\n')
                continue;

            if (!regex.IsMatch(c.ToString()))
            {
                Console.WriteLine($"Invalid character '{c}' found at position {i}");
            }
        }
    }
}
