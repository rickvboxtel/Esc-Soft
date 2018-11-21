namespace EscaperoomSoftware
{
    public interface IMqtt
    {
        event Mqtt.MqttDevicesUpdate DevicesUpdate;

        int Connect(string broker, string name);
        int Disconnect();
        ushort Publish(string topic, string message);
        ushort Subscribe(string topic, byte qos);
    }
}