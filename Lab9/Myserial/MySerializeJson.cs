using System.Text.Json;

namespace Lab9.Myserial;

public static class MySerializeJson<T> where T : class
{
    public static bool Serialize(T t, string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            try
            {
                JsonSerializer.Serialize(fs, t);
            }
            catch (Exception e)
            {
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
                return JsonSerializer.Deserialize<T>(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}