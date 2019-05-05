using NUnit.Framework;

namespace XnbConvert.Test
{
    [TestFixture]
    public class XnbIntTypeReaderTests
    {
        [Test]
        public void TestDeserialize()
        {
            // created with MonoGame pipeline
            // <?xml version="1.0" encoding="utf-8"?>
            // <XnaContent xmlns:ns="Microsoft.Xna.Framework">
            //   <Asset Type="int">
            //     1337
            //   </Asset>
            // </XnaContent>
            byte[] rawData =
            {
                0x58, 0x4E, 0x42, 0x77, 0x05, 0x00, 0x41, 0x00, 0x00, 0x00, 0x01, 0x2B,
                0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x58, 0x6E,
                0x61, 0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B, 0x2E,
                0x43, 0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E, 0x49, 0x6E, 0x74, 0x33,
                0x32, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x01, 0x39, 0x05, 0x00, 0x00
            };
            XnbFile<int> val = XnbConvert.Deserialize<int>(rawData);
            Assert.AreEqual(1337,val.Content);
        }

        [Test]
        public void TestSerialize()
        {
            byte[] rawData = XnbConvert.Serialize(1337);
            XnbFile<int> val = XnbConvert.Deserialize<int>(rawData);
            Assert.AreEqual(1337, val.Content);
        }
    }

   
}