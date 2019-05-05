using System.IO;
using NUnit.Framework;

namespace XnbConvert.Test
{
    [TestFixture]
    public class XnbStringTypeReaderTests
    {
        [Test]
        public void TestDeserialize()
        {
            // created with MonoGame pipeline
            // <?xml version="1.0" encoding="utf-8"?>
            // <XnaContent xmlns:ns="Microsoft.Xna.Framework">
            //   <Asset Type="string">
            //     test
            //   </Asset>
            // </XnaContent>
            byte[] rawData = {
                0x58, 0x4E, 0x42, 0x77, 0x05, 0x00, 0x43, 0x00, 0x00, 0x00, 0x01, 0x2C,
                0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x58, 0x6E,
                0x61, 0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B, 0x2E,
                0x43, 0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E, 0x53, 0x74, 0x72, 0x69,
                0x6E, 0x67, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x01, 0x04, 0x74, 0x65, 0x73, 0x74
            };

            //var r = XnbConvert.Serialize<string>("test");
          
            XnbFile<string> val = XnbConvert.Deserialize<string>(rawData);
            Assert.AreEqual("test", val.Content);
        }

        [Test]
        public void TestSerialize()
        {
            byte[] rawData = XnbConvert.Serialize("test");
            XnbFile<string> val = XnbConvert.Deserialize<string>(rawData);
            Assert.AreEqual("test", val.Content);
        }
    }
}