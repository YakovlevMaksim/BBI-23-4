using System.Text.Json;

namespace Lab9.Myserial;

internal class MySerializeJson<T>: MySerializeBase where T : class
{
    public override bool Serialize<T>(T t, string fileName)
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

    public override T Deserialize<T>(string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            try
            {
                return JsonSerializer.Deserialize<T>(fs);
            }
            catch (Exception e)
            {
                return default(T); 
            }
        }
    }
}