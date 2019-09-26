using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Models
{
    public class Record
    {
        public string RecordName { get; set; }
        public DateTime DateTime { get; set; }
        public string Version { get; set; }
        public ObservableCollection<string> Features { get; set; }
    }
}
