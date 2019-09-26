using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.FileOperations
{
    public interface IReadWriteJsonFile<T>
    {
        ObservableCollection<T> ReadJsonFile(string fileName);
        void WriteJson(string fileName, object data);
    }

    public class ReadWriteJsonFile<T> : IReadWriteJsonFile<T>
    {
        public ObservableCollection<T> ReadJsonFile(string fileName)
        {
            var jsonResults = JsonConvert.DeserializeObject(File.ReadAllText(fileName)).ToString();
            var records = JsonConvert.DeserializeObject<ObservableCollection<T>>(jsonResults);
            return records;
        }

        public void WriteJson(string fileName, object data)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                var result = JsonConvert.SerializeObject(data);
                serializer.Serialize(file, result);
            }
        }
    }

    public interface IReadWriteXmlFile<T>
    {
        ObservableCollection<T> ReadXmlFile(string fileName);
        void WriteXml(string fileName, object data);
    }

    public class ReadWriteXmlFile<T> : IReadWriteXmlFile<T>
    {
        public ObservableCollection<T> ReadXmlFile(string fileName)
        {
            // Now we can read the serialized book ... 
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(T));
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            T record = (T)reader.Deserialize(file);
            file.Close();
            return new ObservableCollection<T>() { record };
        }

        public void WriteXml(string fileName, object data)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(T));

            System.IO.FileStream file = System.IO.File.Create(fileName);

            writer.Serialize(file, data);
            file.Close();
        }
    }
}
