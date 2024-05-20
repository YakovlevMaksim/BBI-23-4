using Lab9.Myserial;
using ProtoBuf;
using System;
using System.Runtime.Serialization;
[ProtoContract]
[DataContract]
public class Jumper
{
    [ProtoMember(1)]
    [DataMember]
    public string Surname { get; set; }

    [ProtoMember(2)]
    [DataMember]
    public string Team { get; set; }

    [ProtoMember(3)]
    [DataMember]
    public int Try1 { get; set; }

    [ProtoMember(4)]
    [DataMember]
    public int Try2 { get; set; }

    [ProtoIgnore]
    [IgnoreDataMember]
    public int Summa => Try1 + Try2;

    [ProtoMember(5)]
    [DataMember]
    public bool Disqualified { get; set; }

    public Jumper() { }

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
class Program1
{
    public static void Run()
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

        Directory.CreateDirectory("C:Users/Max/source/repos/Lab9/Results");

        MySerializeJson<List<Jumper>> jsonSerializer = new MySerializeJson<List<Jumper>>();
        jsonSerializer.Serialize(jumpers, "C:Users/Max/source/repos/Lab9/Results/jumpers.json");

        MySerializeBinary<List<Jumper>> binarySerializer = new MySerializeBinary<List<Jumper>>();
        binarySerializer.Serialize(jumpers, "C:Users/Max/source/repos/Lab9/Results/jumpers.bin");

        MySerializeXml<List<Jumper>> xmlSerializer = new MySerializeXml<List<Jumper>>();
        xmlSerializer.Serialize(jumpers, "C:Users/Max/source/repos/Lab9/Results/jumpers.xml");

        string jsonContent = File.ReadAllText("C:Users/Max/source/repos/Lab9/Results/jumpers.json");
        Console.WriteLine("JSON:");
        Console.WriteLine(jsonContent);

        string xmlContent = File.ReadAllText("C:Users/Max/source/repos/Lab9/Results/jumpers.xml");
        Console.WriteLine("\nXML:");
        Console.WriteLine(xmlContent);

        byte[] binaryContent = File.ReadAllBytes("C:Users/Max/source/repos/Lab9/Results/jumpers.bin");
        Console.WriteLine($"\nBinary (file size): {binaryContent.Length} bytes");

}
}

