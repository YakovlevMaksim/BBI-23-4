using System.Xml.Serialization;

namespace Lab9.Myserial
{
    internal class MySerializeXml<T> : MySerializeBase where T : class
    {
        public override bool Serialize<T>(T t, string fileName) 
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    xmlSerializer.Serialize(fs, t);
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
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    return (T)xmlSerializer.Deserialize(fs); 
                }
                catch (Exception e)
                {
                    return default(T); 
                }
            }
        }
    }
}
