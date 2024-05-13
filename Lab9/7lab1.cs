using Lab9.Myserial;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
class Jumper
{
    [DataMember]
    public string Surname { get; set; }

    [DataMember]
    public string Team { get; set; }

    [DataMember]
    public int Try1 { get; set; }

    [DataMember]
    public int Try2 { get; set; }

    [DataMember]
    public int Summa
    {
        get { return Try1 + Try2; }
        private set { } 
    }

    [DataMember]
    public bool Disqualified { get; set; }

    public Jumper(string surname, string team, int try1, int try2)
    {
        Surname = surname;
        Team = team;
        Try1 = try1;
        Try2 = try2;
        Disqualified = false;
    }

    public void Disqualify()
    {
        Disqualified = true;
    }

    public override string ToString()
    {
        return $"| {Surname} | {Team} | {Try1} | {Try2} | {Summa} |";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Jumper> jumpers = new List<Jumper>()
        {
            new Jumper("Ivanov", "Russia", 8, 9),
            new Jumper("Nakamura", "Japan", 9, 9),
            new Jumper("Schmidt", "Germany", 8, 8),
            new Jumper("Koskinen", "Finland", 9, 8),
            new Jumper("Duplantis", "Sweden", 9, 9),
        };

        foreach (var jumper in jumpers)
        {
            double difficultyCoefficient = jumper.Try1 > jumper.Try2 ? 3.0 : 2.5;
            double totalScore = jumper.Try1 + jumper.Try2 + difficultyCoefficient;
            Console.WriteLine($"{jumper} - Total Score: {totalScore:F2}");
        }

        // Custom sorting function from previous task

        static void Quicksort(Jumper[] jumpers, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(jumpers, left, right);
                Quicksort(jumpers, left, pivotIndex - 1);
                Quicksort(jumpers, pivotIndex + 1, right);
            }
        }

        static int Partition(Jumper[] jumpers, int left, int right)
        {
            Jumper pivot = jumpers[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (jumpers[j].Summa >= pivot.Summa)
                {
                    i++;
                    Swap(jumpers, i, j);
                }
            }

            Swap(jumpers, i + 1, right);
            return i + 1;
        }

        static void Swap(Jumper[] jumpers, int i, int j)
        {
            Jumper temp = jumpers[i];
            jumpers[i] = jumpers[j];
            jumpers[j] = temp;
        }

        //Quicksort(jumpers, 0, jumpers.Length - 1);
        Directory.CreateDirectory("Results");

        MySerializeJson<List<Jumper>>.Serialize(jumpers, "Results/jumpers.json");

        
        MySerializeBinary<List<Jumper>>.Serialize(jumpers, "Results/jumpers.bin");
        
        MySerializeXml<List<Jumper>>.Serialize(jumpers, "Results/jumpers.xml");

        string jsonContent = File.ReadAllText("Results/jumpers.json");
        Console.WriteLine("JSON:");
        Console.WriteLine(jsonContent);

        string xmlContent = File.ReadAllText("Results/jumpers.xml");
        Console.WriteLine("\nXML:");
        Console.WriteLine(xmlContent);

        byte[] binaryContent = File.ReadAllBytes("Results/jumpers.bin");
    }
}