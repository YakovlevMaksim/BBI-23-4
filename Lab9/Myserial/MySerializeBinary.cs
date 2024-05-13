namespace Lab9.Myserial;
using ProtoBuf;

public class MySerializeBinary<T> where T : class
{
    public static bool Serialize(T t, string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            try
            {
                Serializer.Serialize(fs, t);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        return true;
    }

    public static T Deserialize(string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            try
            {
                return Serializer.Deserialize<T>(fs);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                return null;
            }
        }
    }
}