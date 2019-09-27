using AssignmentApp.DialogService;
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
        private bool isEditable = true;

        public AssignmentAppViewModel(IReadWriteJsonFile<Record> readWriteJsonFile)
        {
            SaveCommand = new DelegateCommand(SaveRecord);
            EditRowCommand = new DelegateCommand(EditRecord, CanEditRecord);
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
            var fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AssignmentApp.json";
            readWriteJsonFile.WriteJson(fileName, records);
            //DisplayMessage($"Successfuly create file at location : {fileName}");  
            MessageBox_Show(null,$"Successfuly create file at location : {fileName}");
        }

        private bool CanEditRecord(object paramter)
        {
            ////ToDo: Check, if the records are selected for editing. As of now returning true by default
            //isEditable = Boolean.TryParse(paramter.ToString(),out isEditable);
            //OnPropertyChanged("IsEditable");
            return isEditable;
        }

        private void EditRecord(object isEditRow)
        {
            isEditable = !Boolean.TryParse(isEditRow.ToString(),out isEditable);
            OnPropertyChanged("IsEditable");
        }

        private void SaveRecord(object obj)
        {
            var fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AssignmentApp.json";
            readWriteJsonFile.WriteJson(fileName, records);
            //DisplayMessage($"File is successfully saved at : {fileName}");
            MessageBox_Show(null, $"File is successfully saved at location : {fileName}");
        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand EditRowCommand { get; }
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
                    this.newFeature = string.Empty;
                    OnPropertyChanged("Features");
                    OnPropertyChanged("Records");
                }
            }
        }

        public bool IsEditable
        {
            get
            {
                return isEditable;
            }
            set
            {
                if(isEditable != value)
                {
                    isEditable = value;
                    OnPropertyChanged("IsEditable");
                }
            }
        }

        private void DisplayMessage(string message)
        {
            var viewModel = new DialogViewModel(message);
            var view = new DialogWindow { DataContext = viewModel };

            bool? result = view.ShowDialog();
            if(result.HasValue)
            {
                view.Close();
            }
        }
    }
}
