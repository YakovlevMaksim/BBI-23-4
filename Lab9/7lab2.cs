using Lab9.Myserial;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

abstract class Diving
{
    [DataMember]
    public abstract string DisciplineName { get; }

    [DataMember]
    public List<double> JudgeScores { get; private set; }

    public Diving()
    {
        JudgeScores = new List<double>();
    }

    public void AddJudgeScore(double score)
    {
        JudgeScores.Add(score);
    }

    public double CalculateTotalScore(double difficultyCoefficient)
    {
        JudgeScores.Sort();
        JudgeScores.RemoveAt(0);
        JudgeScores.RemoveAt(JudgeScores.Count - 1);
        double sum = JudgeScores.Sum();
        return sum * difficultyCoefficient;
    }
}

[Serializable]
class Diving3m : Diving
{
    public override string DisciplineName => "Diving from 3m";

    public Diving3m() : base() { }
}

[Serializable]
class Diving5m : Diving
{
    public override string DisciplineName => "Diving from 5m";

    public Diving5m() : base() { }
}

class Diver
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public List<Diving> _dives { get; set; }

    public Diver(string name)
    {
        Name = name;
        _dives = new List<Diving>();
    }

    public void AddDive(Diving dive)
    {
        _dives.Add(dive);
    }

    public double TotalScore
    {
        get
        {
            double total = 0;
            foreach (var dive in _dives)
            {
                total += dive.CalculateTotalScore(GetDifficultyCoefficient(dive));
            }
            return total;
        }
    }

    public double GetDifficultyCoefficient(Diving dive)
    {
        return dive is Diving3m ? 2.5 : 3.5;
    }

    public override string ToString()
    {
        return $"| {Name} | {TotalScore:F2} |";
    }
}

class Program2
{
    public static void Run()
    {
        List<Diver> divers = new List<Diver>();

        Diver diver1 = new Diver("John");
        diver1.AddDive(new Diving3m());
        diver1.AddDive(new Diving3m());
        diver1.AddDive(new Diving5m());
        diver1.AddDive(new Diving5m());

        Diver diver2 = new Diver("Alice");
        diver2.AddDive(new Diving5m());
        diver2.AddDive(new Diving5m());
        diver2.AddDive(new Diving3m());
        diver2.AddDive(new Diving3m());

        divers.Add(diver1);
        divers.Add(diver2);

        MySerializeJson<List<Diver>> jsonSerializer = new MySerializeJson<List<Diver>>();
        jsonSerializer.Serialize(divers, "Results/divers.json");

        MySerializeBinary<List<Diver>> binarySerializer = new MySerializeBinary<List<Diver>>();
        binarySerializer.Serialize(divers, "Results/divers.bin");

        MySerializeXml<List<Diver>> xmlSerializer = new MySerializeXml<List<Diver>>();
        xmlSerializer.Serialize(divers, "Results/divers.xml");

        string jsonContent = File.ReadAllText("Results/divers.json");
        Console.WriteLine("JSON:");
        Console.WriteLine(jsonContent);

        string xmlContent = File.ReadAllText("Results/divers.xml");
        Console.WriteLine("\nXML:");
        Console.WriteLine(xmlContent);

        byte[] binaryContent = File.ReadAllBytes("Results/divers.bin");
        Console.WriteLine($"\nBinary (file size): {binaryContent.Length} bytes");
    }
}
