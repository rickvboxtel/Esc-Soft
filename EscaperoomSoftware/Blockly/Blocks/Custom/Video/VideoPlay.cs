using Blockly;
using Blockly.Blocks;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscaperoomSoftware;

namespace Blockly.Blocks.Video
{
    class VideoPlay : IBlock
    {
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();

            string macAddress = (string)Values.Evaluate("macAddress", context);
            string fileName = (string)Values.Evaluate("state", context);
            Device device = cache.Get<Dictionary<string, Device>>("devices")[macAddress];
            Console.WriteLine("in Videoplay");

            if (device != null)
            {
                device.Misc["Playing"] = 1;
                Mqtt MqttClient = new Mqtt();
                MqttClient.Publish("/to/" + macAddress + "/startVideo/", fileName);
            }
            else
            {
                return false;
            }
            //lower the time to make it a lil threadsafe.
            Dictionary<string, Device> devices = cache.Get<Dictionary<string, Device>>("devices");
            devices[macAddress] = device;
            cache.Add("devices", devices);
            Console.WriteLine("video Out");
            return base.Evaluate(context);
        }
    }
}
