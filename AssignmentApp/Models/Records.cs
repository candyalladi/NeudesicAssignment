using AssignmentApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AssignmentApp.Models
{
    public class Record : ViewModelBase
    {
        private string recordName;
        private DateTime dateTime = DateTime.Today;
        private ObservableCollection<string> features;
        private int nextId; 

        public Record()
        {
            Id = Interlocked.Increment(ref nextId);
        }

        [Key]
        public int Id
        {
            get;set;           
        }
        public string RecordName
        {
            get => recordName;
            set
            {
                if (recordName != value)
                {
                    recordName = value;
                    OnPropertyChanged("RecordName");
                }
            }
        }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public string Version { get; set; }
        public ObservableCollection<string> Features
        {
            get => features;
            set
            {
                if(features != value)
                {
                    features = value;
                    OnPropertyChanged("Features");
                }
            }
        }
    }

    [Serializable, XmlRoot("Records")]
    public class Records
    {
        public List<Record> RecordList { get; set; }
    }
}
