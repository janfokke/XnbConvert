using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace XnbConvert.Test
{
    [TestFixture]
    public class XnbListTypeReaderTests
    {
        [Test]
        public void TestDeserialize()
        {
            // created with MonoGame pipeline
            // <?xml version="1.0" encoding="utf-8" ?>
            // <XnaContent>
            //   <Asset Type="System.Collections.Generic.List[int]">
            //     <item>
            //       1
            //     </item>
            //   </Asset>
            // </XnaContent>
            byte[] rawData = {
                0x58, 0x4E, 0x42, 0x77, 0x05, 0x00, 0xD4, 0x00, 0x00, 0x00, 0x02, 0x89,
                0x01, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x58,
                0x6E, 0x61, 0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B,
                0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E, 0x4C, 0x69, 0x73,
                0x74, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x60, 0x31, 0x5B, 0x5B, 0x53,
                0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x49, 0x6E, 0x74, 0x33, 0x32, 0x2C,
                0x20, 0x6D, 0x73, 0x63, 0x6F, 0x72, 0x6C, 0x69, 0x62, 0x2C, 0x20, 0x56,
                0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34, 0x2E, 0x30, 0x2E, 0x30,
                0x2E, 0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D,
                0x6E, 0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C, 0x20, 0x50, 0x75, 0x62,
                0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F, 0x6B, 0x65, 0x6E, 0x3D,
                0x62, 0x37, 0x37, 0x61, 0x35, 0x63, 0x35, 0x36, 0x31, 0x39, 0x33, 0x34,
                0x65, 0x30, 0x38, 0x39, 0x5D, 0x5D, 0x00, 0x00, 0x00, 0x00, 0x2B, 0x4D,
                0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x58, 0x6E, 0x61,
                0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B, 0x2E, 0x43,
                0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E, 0x49, 0x6E, 0x74, 0x33, 0x32,
                0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
                0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00
            };

            var list = new List<int> {1};
            XnbFile<List<int>> deserialized = XnbConvert.Deserialize<List<int>>(rawData);
            Assert.AreEqual(list, deserialized.Content);
        }

        [Test]
        public void TestSerialize()
        {
            var list = new List<int>{1};
            byte[] rawData = XnbConvert.Serialize(list);
            var deserialized = XnbConvert.Deserialize<List<int>>(rawData);
            Assert.AreEqual(list, deserialized.Content);
        }
    }
}