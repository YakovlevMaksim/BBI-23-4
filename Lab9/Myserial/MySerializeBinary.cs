namespace Lab9.Myserial;
using ProtoBuf;

internal class MySerializeBinary<T>:MySerializeBase where T : class
{
    public override bool Serialize<T>(T t, string fileName)
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

    public override T Deserialize<T>(string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            try
            {
                return Serializer.Deserialize<T>(fs);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
    }
}