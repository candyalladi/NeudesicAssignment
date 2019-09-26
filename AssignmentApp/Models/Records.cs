using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AssignmentApp.Models
{
    public class Record
    {
        private string recordName;

        public string RecordName { get => recordName; set => recordName = value; }
        public DateTime DateTime { get; set; }
        public string Version { get; set; }
        public ObservableCollection<string> Features { get; set; }
    }

    [Serializable, XmlRoot("Records")]
    public class Records
    {
        public List<Record> RecordList { get; set; }
    }
}
