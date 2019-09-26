using AssignmentApp.FileOperations;
using AssignmentApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AssignmentApp.ViewModels
{
    public class AssignmentAppViewModel : ViewModelBase
    {
        private ObservableCollection<Record> records;
        private Record selectedRecord;
        private string newFeature;
        private IReadWriteJsonFile<Record> readWriteJsonFile;

        public AssignmentAppViewModel(IReadWriteJsonFile<Record> readWriteJsonFile)
        {
            SaveCommand = new DelegateCommand(SaveRecord);
            EditCommand = new DelegateCommand(EditRecord, CanEditRecord);
            CreateNewFileCommand = new DelegateCommand(CreateNewFile);
            records = new ObservableCollection<Record>();
            this.readWriteJsonFile = readWriteJsonFile;
        }

        public void OpenExistingFile(string fileName)
        {
            records = readWriteJsonFile.ReadJsonFile(fileName);
            OnPropertyChanged("Records");
        }

        public void CreateNewFile(object parameter)
        {
            //If new file type is xml, then write a XML file
            //WriteXML();

            //if new type if JSON, then write to a JSON file.
            //WriteJson();
            //Writing a dummy data to a file. as of now JSON file
            List<Record> records = new List<Record>();
            Record record = new Record()
            {
                RecordName = "Record 1",
                DateTime = DateTime.Today,
                Version = "1.0.0.X",
                Features = new ObservableCollection<string>()
                    {
                        "Added Tools Section",
                        "Add Admin Section"
                    }
            };
            Record record2 = new Record()
            {
                RecordName = "Record 2",
                DateTime = DateTime.Today,
                Version = "1.0.0.X",
                Features = new ObservableCollection<string>()
                    {
                        "Added Tools Section",
                        "Add Admin Section"
                    }
            };
            records.Add(record);
            records.Add(record2);
            var fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//AssignmentApp.json";
            readWriteJsonFile.WriteJson(fileName, records);
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
            var fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//AssignmentApp.json";
            readWriteJsonFile.WriteJson(fileName, records);
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
                if(records !=value)
                {
                    records = value;
                    OnPropertyChanged("Records");
                }
            }
        }

        public Record SelectedItem
        {
            get
            {
                return selectedRecord;
            }
            set
            {
                if(selectedRecord != value)
                {
                    selectedRecord = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public string NewFeature
        {
            get
            {
                return newFeature;
            }
            set
            {
                if(newFeature != value)
                {
                    newFeature = value;
                    if(selectedRecord.Features == null)
                    {
                        selectedRecord.Features = new ObservableCollection<string>();
                    }
                    selectedRecord.Features.Add(newFeature);
                    OnPropertyChanged("Records");

                }
            }
        }

        private void ReadXmlFile(string fileName)
        {
            // Now we can read the serialized book ... 
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(Record));
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            Record record = (Record)reader.Deserialize(file);
            //FileContent = record.ToString();
            file.Close();
        } 

        public static void WriteXML()
        {
            Records records = new Records();
            records.RecordList = new List<Record>();

            Record record = new Record();
            record.RecordName = "Record 1";
            record.DateTime = DateTime.Today;
            record.Version = "1.0.0.x";
            record.Features = new ObservableCollection<string>()
            {
                "Added Tools Section",
                "Add Admin Section"
            };
            records.RecordList.Add(record);

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(Records));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//AssignmentApp.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, records);
            file.Close();
        }
    }
}
