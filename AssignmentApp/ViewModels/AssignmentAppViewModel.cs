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
        private Record selectedRecord;
        private Record editedRecord;
        private string newFeature;
        private IReadWriteJsonFile<Record> readWriteJsonFile;
        private bool isEditable = false;
        private bool canUserAddRecord = false;
        private ObservableCollection<Record> records;
        private ObservableCollection<Record> editedRecords;

        public AssignmentAppViewModel(IReadWriteJsonFile<Record> readWriteJsonFile)
        {
            SaveCommand = new DelegateCommand(SaveRecord);
            CreateNewFileCommand = new DelegateCommand(CreateNewFile);
            NewRecordCommand = new DelegateCommand(AddNewRecord,CanAddNewRecord);
            EditRecordCommand = new DelegateCommand(EditRecord, CanEditRecord);
            records = new ObservableCollection<Record>();
            editedRecords = new ObservableCollection<Record>();
            this.readWriteJsonFile = readWriteJsonFile;
        }

        private bool CanEditRecord(object obj)
        {
            return SelectedItem !=null;
        }

        private bool CanAddNewRecord(object obj)
        {
            return true;
        }

        private void AddNewRecord(object obj)
        {
            CanUserAddRecord = true;
            SelectedEditedRecords.Clear();
            MessageBox_Show(null,$"Successfully added a new record");
        }

        public void OpenExistingFile(string fileName)
        {
            Records = readWriteJsonFile.ReadJsonFile(fileName);
            editedRecords.Clear();
        }

        public void CreateNewFile(object parameter)
        {
            //Writing a dummy data to a file. as of now JSON file
            List<Record> records = new List<Record>();
            Record record = new Record()
            {
                Id = 1,
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
                Id=2,
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
            MessageBox_Show(null,$"Successfuly create file at location : {fileName}", "Create File", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private void EditRecord(object isEditRow)
        {
            SelectedEditedRecords.Add(selectedRecord);
        }

        private void SaveRecord(object obj)
        {
            var fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AssignmentApp.json";           
            readWriteJsonFile.WriteJson(fileName, records);
            SelectedEditedRecords.Clear();
            MessageBox_Show(null, $"File is successfully saved at location : {fileName}", "File Save", System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Information);
        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand EditRowCommand { get; }
        public DelegateCommand CreateNewFileCommand { get; }

        public DelegateCommand NewRecordCommand { get; }
        public DelegateCommand EditRecordCommand { get; private set; }

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
                    EditRecordCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public Record EditedRecord
        {
            get
            {
                return editedRecord;
            }
            set
            {
                if (editedRecord != value)
                {
                    editedRecord = value;
                    OnPropertyChanged("EditedRecord");
                }
            }
        }

        public ObservableCollection<Record> SelectedEditedRecords
        {
            get
            {
                return editedRecords;
            }
            set
            {
                if (editedRecords != value)
                {
                    editedRecords = value;
                    foreach (var item in editedRecords)
                    {
                        records.Add(item);
                    }
                    editedRecords.Clear();
                    OnPropertyChanged("Records");
                    OnPropertyChanged("SelectedEditedRecords");
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

        public bool CanUserAddRecord
        {
            get
            {
                return canUserAddRecord;
            }
            set
            {
                if(canUserAddRecord != value)
                {
                    canUserAddRecord = value;
                    OnPropertyChanged("CanUserAddRecord");
                }
            }
        }
    }
}
