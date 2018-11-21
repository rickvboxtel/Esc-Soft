using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscaperoomSoftware
{
    class Device : IDevice
    {
        public string Mac { get; set; }
        public ErrorState Error { get; set; }
        public ControllerStatus Status { get; set; }
        public ControllerType Type { get; set; }
        public Dictionary<string, object> Misc { get; set; }

    }
}
