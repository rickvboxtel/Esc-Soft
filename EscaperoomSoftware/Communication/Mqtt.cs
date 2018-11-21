using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace EscaperoomSoftware
{
    public class Mqtt : IMqtt
    {
        public static MqttClient Client;
        public delegate void MqttDevicesUpdate(object sender, EventArgs e);
        public event MqttDevicesUpdate DevicesUpdate;
        IAppCache cache = new CachingService();
        JsonParser jsonParser = new JsonParser();

        public int Connect(string broker, string name)
        {
            Client = new MqttClient(broker);
            int connectCode = 1;
            try { connectCode = Client.Connect(name); } catch (Exception e) { Console.WriteLine(e.Message); }

            Client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            return connectCode;
        }
        public int Disconnect()
        {
            if(Client == null) return -1;
            else if(Client.IsConnected)
            {
                Client.Disconnect();
                return 0;
            }
            return 1;
        }

        public ushort Subscribe(string topic,  byte qos)
        {
            return Client.Subscribe(new string[] { topic },new byte[] { qos });
        }
        public ushort Publish(string topic, string message)
        {
            return Client.Publish(topic, Encoding.ASCII.GetBytes(message));
        }

        void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (e.Topic.Contains("new") && Encoding.UTF8.GetString(e.Message).Contains("Mac"))
            {
                UpdateDevices(Encoding.UTF8.GetString(e.Message));
            }
            Console.WriteLine("msgReceived");
            if(e.Topic.Contains("from"))
            {
                //get macAddr out of message /from/MACADDR/
                string macAddr =string.Join("/", e.Topic.Split('/').Skip(2).ToArray())
                    .Remove(string.Join("/", e.Topic.Split('/').Skip(2).ToArray()).Length - 1);
                Dictionary<string, Device> devices = cache.Get<Dictionary<string, Device>>("devices");
                devices[macAddr].Misc = jsonParser.ToDict(Encoding.UTF8.GetString(e.Message));
                cache.Add("devices", devices);

            }
        }
        private void UpdateDevices(string message)
        {
            Dictionary<string, Device> devices = cache.Get<Dictionary<string, Device>>("devices");
            if (devices == null)
            {
                devices = new Dictionary<string, Device>();
            }
            devices[jsonParser.ToDevice(message).Mac] = jsonParser.ToDevice(message);
            Subscribe("/from/"+ jsonParser.ToDevice(message).Mac+"/", 0);
            cache.Add("devices", devices);
            OnDeviceUpdated();
        }

        protected virtual void OnDeviceUpdated()
        {
            //EventHandler
            Console.WriteLine("Invoke OnDeviceUpdate");
            DevicesUpdate?.Invoke(this, EventArgs.Empty);
        }


    }
}
