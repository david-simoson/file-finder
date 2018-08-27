using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finder
{
    class FindingEventArgs : EventArgs
    {
        public FindingEventTypes EventType { get; set; }

        public string Display { get; set; }

        public int TotalFound { get; set; }

        public string FileName { get; set; }
    }
}
