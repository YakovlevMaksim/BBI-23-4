using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;


class Task
{
    public virtual int SolveTask1(string text)
    {
        return 0;
    }

    public virtual List<string> SolveTask2(string text)
    {
        return new List<string>();
    }
}

class Task1 : Task
{
    public override int SolveTask1(string text)
    {
        string[] prepositionsAndConjunctions = { "и", "а", "но", "или", "чтобы", "как", "чем", "так", "также", "либо", "в" };
        int count = 0;

        foreach (string word in text.Split(' '))
        {
            string lowerWord = word.ToLower();
            if (prepositionsAndConjunctions.Contains(lowerWord) && !lowerWord.Any(x => "aoеёиуыэюя".Contains(x)))
            {
                count++;
            }
        }

        return count;
    }
}

class Task2 : Task
{
    public override List<string> SolveTask2(string text)
    {
        Dictionary<char, int> charFrequency = new Dictionary<char, int>();
        List<string> output = new List<string>();

        foreach (char c in text.ToLower())
        {
            if (Char.IsLetter(c))
            {
                if (charFrequency.ContainsKey(c))
                {
                    charFrequency[c]++;
                }
                else
                {
                    charFrequency[c] = 1;
                }
            }
        }

        char mostCommonChar = charFrequency.OrderByDescending(x => x.Value).First().Key;

        foreach (string word in text.Split(' '))
        {
            if (word.ToLower().Contains(mostCommonChar))
            {
                output.Add(word);
            }
        }

        return output;
    }
}
class Program
{
    public static void Main(string[] args)
    {
        string text = "здравствуйте дорогие студенты и любимый марк анатольевичь, поставьте максмальный балл пожалуйста я хочу четверочку в семестре";
        Task task = new Task1();
        Console.WriteLine("Task 1:");
        Console.WriteLine(task.SolveTask1(text));

        task = new Task2();
        Console.WriteLine("\nTask 2:");
        List<string> wordsWithMostCommonChar = task.SolveTask2(text);
        foreach (string word in wordsWithMostCommonChar)
        {
            Console.WriteLine(word);
        }
        string userFolder = "C:/Users/m2213144";

        string testFolder = Path.Combine(userFolder, "Test");

        string task1Json = Path.Combine(testFolder, "task_1.json");

        string task2Json = Path.Combine(testFolder, "task_2.json");

        if (!Directory.Exists(testFolder))
        {
            Directory.CreateDirectory(testFolder);
        }

        if (!File.Exists(task1Json))
        {
            File.Create(task1Json).Close();
        }

        if (!File.Exists(task2Json))
        {
            File.Create(task2Json).Close();
        }

        var task1Data = new { Name = "Task 1", Description = task.SolveTask1(text) };
        string task1JsonData = JsonSerializer.Serialize(task1Data);
        File.WriteAllText(task1Json, task1JsonData);
        string task1JsonDataFromFile = File.ReadAllText(task1Json);

        Console.WriteLine("Данные задачи 1 из файла:");
        Console.WriteLine(task1JsonDataFromFile);

        var task2Data = new { Name = "Task 2", Description = task.SolveTask2(text) };
        string task2JsonData = JsonSerializer.Serialize(task2Data);
        File.WriteAllText(task2Json, task2JsonData);
        string task2JsonDataFromFile = File.ReadAllText(task2Json);

        Console.WriteLine("Данные задачи 2 из файла:");
        Console.WriteLine(task2JsonDataFromFile);
    }
}
