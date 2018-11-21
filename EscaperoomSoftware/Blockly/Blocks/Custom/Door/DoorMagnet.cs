using Blockly;
using Blockly.Blocks;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscaperoomSoftware;

namespace Blockly.Blocks.Door
{
    class DoorMagnet : IBlock
    {
        //NOT DONE!!
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();

            string macAddress = (string)Values.Evaluate("macAddress", context);
            bool state = (bool)Values.Evaluate("state", context);
            Device device = cache.Get<Dictionary<string, Device>>("devices")[macAddress];
            Console.WriteLine("in DoorMagnet");
            if (device != null)
            {
                Mqtt MqttClient = new Mqtt();
                if (state)
                {
                    device.Misc["Magnet"] = 1;
                    MqttClient.Publish("/to/" + macAddress + "/", "magnetOn");
                }
                else
                {
                    device.Misc["Magnet"] = 0;
                    MqttClient.Publish("/to/" + macAddress + "/", "magnetOff");
                }
                //lower the time to make it a lil threadsafe.
                Dictionary<string, Device> devices = cache.Get<Dictionary<string, Device>>("devices");
                devices[macAddress] = device;
                cache.Add("devices", devices);
            }
            else
            {
                return false;
            }

            return base.Evaluate(context);
        }
    }
}
