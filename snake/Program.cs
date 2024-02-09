using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Bitte geben Sie den Pfad zum Ordner ein:");
        string folderPath = Console.ReadLine();

        if (Directory.Exists(folderPath))
        {
            int totalFileCount = 0;
            List<CustomFileInfo> filesWithLineBreaks = FindLineBreaksInFiles(folderPath, new List<string> { ".cs", ".razor" }, ref totalFileCount);

            Console.WriteLine($"Anzahl der Dateien im Ordner: {totalFileCount}");
            Console.WriteLine($"Gefundene Dateien mit Zeilenumbrüchen ({filesWithLineBreaks.Count}):");

            foreach (var fileInfo in filesWithLineBreaks)
            {
                Console.WriteLine($"{Path.GetFileName(fileInfo.Name)} - Anzahl der Zeilenumbrüche: {fileInfo.LineBreakCount}");
            }

            // Save information to a text file
            SaveToFile(filesWithLineBreaks, "output.txt");

            Console.WriteLine("Die Informationen wurden in die Datei 'output.txt' gespeichert.");
        }
        else
        {
            Console.WriteLine($"Der Ordner '{folderPath}' wurde nicht gefunden.");
        }

        Console.ReadLine();
    }

    static void SaveToFile(List<CustomFileInfo> filesWithLineBreaks, string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var fileInfo in filesWithLineBreaks)
                {
                    writer.WriteLine($"{Path.GetFileName(fileInfo.Name)} - Anzahl der Zeilenumbrüche: {fileInfo.LineBreakCount}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Speichern der Datei: {ex.Message}");
        }
    }

    static List<CustomFileInfo> FindLineBreaksInFiles(string folderPath, List<string> fileExtensions, ref int totalFileCount)
    {
        List<CustomFileInfo> filesWithLineBreaks = new List<CustomFileInfo>();

        try
        {
            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
            totalFileCount = files.Length;

            int currentFileCount = 0;

            foreach (var file in files)
            {
                string extension = Path.GetExtension(file);
                if (fileExtensions.Contains(extension))
                {
                    CustomFileInfo fileInfo = new CustomFileInfo(file);
                    int lineBreakCount = CountLineBreaks(file);

                    if (lineBreakCount > 0)
                    {
                        fileInfo.LineBreakCount = lineBreakCount;
                        filesWithLineBreaks.Add(fileInfo);
                    }
                }

                currentFileCount++;
                Console.WriteLine($"Datei {currentFileCount}/{totalFileCount} überprüft: {Path.GetFileName(file)}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Durchsuchen des Ordners: {ex.Message}");
        }

        return filesWithLineBreaks;
    }

    static int CountLineBreaks(string filePath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string content = reader.ReadToEnd();
                int lineBreakCount = content.Count(c => c == '\r');
                return lineBreakCount;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Zählen der Zeilenumbrüche in der Datei '{filePath}': {ex.Message}");
            return 0;
        }
    }
}

class CustomFileInfo
{
    public string Name { get; set; }
    public int LineBreakCount { get; set; }

    public CustomFileInfo(string name)
    {
        Name = name;
        LineBreakCount = 0;
    }
}
