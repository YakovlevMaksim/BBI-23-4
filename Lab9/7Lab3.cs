using Lab9.Myserial;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

abstract class Country
{
    [DataMember]
    public abstract string Name { get; }

    [DataMember]
    protected List<string> Answers { get; private set; }

    public Country()
    {
        Answers = new List<string>();
    }

    public void AddAnswer(string answer)
    {
        Answers.Add(answer);
    }

    public List<string> GetTopAnswers(int n)
    {
        Dictionary<string, int> answerCounts = new Dictionary<string, int>();

        foreach (string answer in Answers)
        {
            if (!answerCounts.ContainsKey(answer))
            {
                answerCounts[answer] = 1;
            }
            else
            {
                answerCounts[answer]++;
            }
        }

        var sortedAnswers = answerCounts.OrderByDescending(x => x.Value).Take(n);
        return sortedAnswers.Select(x => x.Key).ToList();
    }

    public double GetPercentage(string answer)
    {
        int count = Answers.Count;
        int answerCount = Answers.FindAll(x => x == answer).Count;
        return (double)answerCount / count * 100;
    }
}

[Serializable]
class Russia : Country
{
    public override string Name => "Russia";
}

[Serializable]
class Japan : Country
{
    public override string Name => "Japan";
}

class Program3
{
    public static void Run()
    {
        Russia russia = new Russia();
        Japan japan = new Japan();

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Enter answers for Question {i + 1}:");
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine($"Enter answer for {(j == 0 ? "Russia" : "Japan")}");
                string answer = Console.ReadLine();
                if (j == 0)
                {
                    russia.AddAnswer(answer);
                }
                else
                {
                    japan.AddAnswer(answer);
                }
            }
        }

        Directory.CreateDirectory("C:Users/Max/source/repos/Lab9/Results");


        MySerializeJson<Russia> jsonSerializer = new MySerializeJson<Russia>();
        MySerializeJson<Japan> jsonSerializer2 = new MySerializeJson<Japan>();

        MySerializeXml<Russia> XmlSerializer = new MySerializeXml<Russia>();
        MySerializeXml<Japan> XmlSerializer2 = new MySerializeXml<Japan>();

        MySerializeBinary<Russia> binSerializer = new MySerializeBinary<Russia>();
        MySerializeBinary<Japan> binSerializer2 = new MySerializeBinary<Japan>();


        string russiaJsonContent = File.ReadAllText("Results/russia.json");
        Console.WriteLine("Russia (JSON):");
        Console.WriteLine(russiaJsonContent);

        string japanJsonContent = File.ReadAllText("Results/japan.json");
        Console.WriteLine("\nJapan (JSON):");
        Console.WriteLine(japanJsonContent);

        byte[] russiaBinaryContent = File.ReadAllBytes("Results/russia.bin");
        Console.WriteLine("\nRussia (binary file size): {0} bytes", russiaBinaryContent.Length);

        byte[] japanBinaryContent = File.ReadAllBytes("Results/japan.bin");
        Console.WriteLine("\nJapan (binary file size): {0} bytes", japanBinaryContent.Length);
    }
}
