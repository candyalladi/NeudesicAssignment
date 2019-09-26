using AssignmentApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.ViewModels
{
    public class AssignmentAppViewModel
    {
        private ObservableCollection<Records> records;

        public AssignmentAppViewModel()
        {
            SaveCommand = new DelegateCommand(SaveRecord);
            EditCommand = new DelegateCommand(EditRecord, CanEditRecord);
        }

        public void OpenExistingFile(string fileName)
        {
            //Process the selected file.
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

        public ObservableCollection<Records> Records
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
    }
}
