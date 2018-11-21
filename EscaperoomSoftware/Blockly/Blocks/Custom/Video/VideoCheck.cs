using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscaperoomSoftware;
using LazyCache;

namespace Blockly.Blocks.Video
{
    class VideoCheck : IBlock
    {
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();
            Mqtt MqttClient = new Mqtt();


            string macAddress = (string)Values.Evaluate("macAddress", context);
            MqttClient.Publish("/to/"+macAddress+"/isVideoPlaying/", "");
            Device device = cache.Get<Dictionary<string, Device>>("devices")[macAddress];
            if(device.Misc["Playing"].ToString().Contains("1"))
            {
                Console.WriteLine("video is playing");
                return true;
            }
            else
            {
                Console.WriteLine("video is NOT playing");
                return false;
            }

        }
    }
}
