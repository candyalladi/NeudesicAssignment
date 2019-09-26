using AssignmentApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.ViewModels
{
    public class AssignmentAppViewModel
    {
        private ObservableCollection<Record> records;

        public AssignmentAppViewModel()
        {
            SaveCommand = new DelegateCommand(SaveRecord);
            EditCommand = new DelegateCommand(EditRecord, CanEditRecord);
            CreateNewFileCommand = new DelegateCommand(CreateNewFile);
        }

        public void OpenExistingFile(string fileName)
        {
            //Process the selected file.'
            ReadXmlFile(fileName);
        }

        public void CreateNewFile(object parameter)
        {
            //If new file type is xml, then write a XML file
            WriteXML();

            //if new type if JSON, then write to a JSON file.
            WriteJson();
        }

        private void WriteJson()
        {
            List<Record> recods = new List<Record>();
            //open file stream
            using (StreamWriter file = File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//AssignmentApp.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, recods);
            }
        }

        private bool CanEditRecord(object obj)
        {
            //ToDo: Check, if the records are selected for editing. As of now returning true by default
            return true;
        }

        private void EditRecord(object obj)
        {

        }

        private void SaveRecord(object obj)
        {

        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand CreateNewFileCommand { get; }

        public ObservableCollection<Record> Records
        {
            get
            {
                return records;
            }
            set
            {
                records = value;                
            }
        }

        public string FileContent { get; private set; }

        private void ReadXmlFile(string fileName)
        {
            // Now we can read the serialized book ...  
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(Record));
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            Record record = (Record)reader.Deserialize(file);
            FileContent = record.ToString();
            file.Close();
        } 
        
        private void ReadJsonFile(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                List<Record> items = JsonConvert.DeserializeObject<List<Record>>(json);
            }
        }

        public static void WriteXML()
        {
            Record record = new Record();
            record.RecordName = "Record 1";
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(Record));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//AssignmentApp.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, record);
            file.Close();
        }
    }
}
