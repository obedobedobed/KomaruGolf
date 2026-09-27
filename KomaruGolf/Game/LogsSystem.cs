using System;
using System.Collections.Generic;
using System.IO;

namespace KomaruGolf;

public static class LogsSystem
{
    public const string SAVE_PATH = "LastLogs.txt";
    private static List<string> totalLogs = new List<string>();

    public static void Log(string text)
    {
        string finalLogString = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} [L] > {text}";
        Console.WriteLine(finalLogString);
        totalLogs.Add(finalLogString);
        
        WriteLog();
    }

    public static void Warn(string text)
    {
        string finalLogString = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} [W] > {text}";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(finalLogString);
        Console.ResetColor();
        totalLogs.Add(finalLogString);

        WriteLog();
    }

    public static void Error(string text)
    {
        string finalLogString = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} [E] > {text}";
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(finalLogString);
        Console.ResetColor();
        totalLogs.Add(finalLogString);

        WriteLog();
    }

    public static void WriteLog()
    {
        File.WriteAllLines(SAVE_PATH, totalLogs);
    }
}