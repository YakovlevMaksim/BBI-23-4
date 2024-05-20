using System;
using System.IO;
using System.Text.Json;

namespace Lab9.Myserial
{
    public class MySerializeJson<T> : MySerializeBase where T : class
    {
        public override bool Serialize<T>(T t, string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    JsonSerializer.Serialize(fs, t);
                }
                Console.WriteLine($"Serialized to JSON: {fileName}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to serialize to JSON: {e.Message}");
                return false;
            }
        }

        public override T Deserialize<T>(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    return JsonSerializer.Deserialize<T>(fs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to deserialize from JSON: {e.Message}");
                return null;
            }
        }
    }
}
