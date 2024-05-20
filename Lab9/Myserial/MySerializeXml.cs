using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab9.Myserial
{
    public class MySerializeXml<T> : MySerializeBase where T : class
    {
        public override bool Serialize<T>(T t, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, t);
                }
                Console.WriteLine($"Serialized to XML: {fileName}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to serialize to XML: {e.Message}");
                return false;
            }
        }

        public override T Deserialize<T>(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    return (T)xmlSerializer.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to deserialize from XML: {e.Message}");
                return null;
            }
        }
    }
}
