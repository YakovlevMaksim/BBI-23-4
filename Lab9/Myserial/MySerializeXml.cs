using System.Xml.Serialization;

namespace Lab9.Myserial;

public static class MySerializeXml<T> where T : class
{
    public static bool Serialize(T t, string fileName)
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

    public static T Deserialize(string fileName)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            try
            {
                T t = xmlSerializer.Deserialize(fs) as T;
                return t;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                return null;
            }
        }
    }
}