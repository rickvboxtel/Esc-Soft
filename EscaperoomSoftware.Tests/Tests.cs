using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EscaperoomSoftware.Tests
{
    [TestClass]
    public class MqttTesting
    {
        /* Only testing own implemented code.
            MQTT Library has its own tests.
            https://github.com/eclipse/paho.mqtt.testing
           I need to use a real online MQTT broker because
            I cannot mock it perfectly. This will need to be
            updated in the near future.
        */

        [TestMethod]
        [ExpectedException(typeof(System.Net.Sockets.SocketException))]
        public void Mqtt_connectFail()
        {
            Mqtt client = new Mqtt();
            int returnCode = client.Connect("broker", "client");
        }

        [TestMethod]
        public void Mqtt_disconnectNull()
        {
            Mqtt client = new Mqtt();
            int returnCode = client.Disconnect();
            int failCode = -1;
            Assert.AreEqual(returnCode, failCode);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Mqtt_subscribeFail()
        {
            Mqtt client = new Mqtt();
            int returnCode = client.Subscribe("Topic", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Mqtt_publishFail()
        {
            Mqtt client = new Mqtt();
            int returnCode = client.Publish("Topic", "Message");
        }

        [TestMethod]
        public void Mqtt_connectSucces()
        {
            Mqtt client = new Mqtt();
            int returnCode = client.Connect("localhost", "testClient");
            int succesCode = 0;
            Assert.AreEqual(returnCode, succesCode);
        }

        [TestMethod]
        public void Mqtt_subscribeSucces()
        {
            Mqtt client = new Mqtt();
            client.Connect("localhost", "testClient");
            int returnCode = client.Subscribe("Topic", 0);
            int succesCode = 1;
            Assert.AreEqual(returnCode, succesCode);
        }

        [TestMethod]
        public void Mqtt_publishSucces()
        {
            Mqtt client = new Mqtt();
            client.Connect("localhost", "testClient");
            int returnCode = client.Publish("Topic", "Message");
            int succesCode = 1;
            Assert.AreEqual(returnCode, succesCode);
        }
    }

    [TestClass]
    public class JsonParserTesting
    {
        /* 
        */

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JsonParser_toDeviceFail()
        {
            JsonParser jsonParser = new JsonParser();
            jsonParser.ToDevice("failing string with no json");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JsonParser_toDictFail()
        {
            JsonParser jsonParser = new JsonParser();
            jsonParser.ToDict("failing string with no json");
        }

        [TestMethod]
        public void JsonParser_toDeviceSucces()
        {
            JsonParser jsonParser = new JsonParser();
            Device jsonConverted = jsonParser.ToDevice("{'Mac': '00:11:22:33:44','Error': 0,'Type': 1,'Status': 2,'Misc': {'Playing':0,'Random':'random'}}");

            Assert.AreEqual(jsonConverted.Mac, "00:11:22:33:44");
            Assert.AreEqual(jsonConverted.Error, ErrorState.None);
            Assert.AreEqual(jsonConverted.Type, ControllerType.Radio);
            Assert.AreEqual(jsonConverted.Status, ControllerStatus.Error);
            Assert.AreEqual(jsonConverted.Misc["Playing"], (Int64)0); //Int64?
            Assert.AreEqual(jsonConverted.Misc["Random"], "random");
        }

        [TestMethod]
        public void JsonParser_toDictSucces()
        {
            JsonParser jsonParser = new JsonParser();
            jsonParser.ToDict("{'Playing':0,'Random':'random'}");
        }
    }
}

