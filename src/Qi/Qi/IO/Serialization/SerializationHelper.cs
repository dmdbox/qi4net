using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Qi.IO.Serialization
{
    public static class SerializationHelper
    {
        public static void SerializeBinary(this object obj, Stream outputSteam)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(outputSteam, obj);
        }

        public static void SerializeBinary(this object obj, string filePath)
        {
            using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                obj.SerializeBinary(stream);
            }
        }

        public static object UnserializeBinary(Stream objectStream)
        {
            IFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(objectStream);
        }

        public static object UnserializeBinary(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("Can't unserialize from  file.", file);
            }
            using (FileStream stream = File.OpenRead(file))
            {
                return UnserializeBinary(stream);
            }
        }
    }
}