using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscaperoomSoftware
{
    public enum ControllerStatus
    {
        Connected,
        Disconnected,
        Error
    }

    public enum ControllerType
    {
        Door,
        Radio,
        VideoMaster
    }

    public enum ErrorState
    {
        None,
        LooseWire
    }
    public interface IDevice
    {
        string Mac { get; set; }
        ControllerType Type { get; set; }
        ControllerStatus Status { get; set; }
        ErrorState Error { get; set; }
        Dictionary<string,object> Misc { get; set; }
    }
}
