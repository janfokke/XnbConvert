using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace XnbConvert.Test
{
    [TestFixture]
    public class XnbArrayTypeReaderTests
    {
        [Test]
        public void TestDeserialize()
        {
            // created with MonoGame pipeline
            // <?xml version="1.0" encoding="utf-8" ?>
            // <XnaContent>
            //   <Asset Type="int[]">
            //         1 2 3 4
            //   </Asset>
            // </XnaContent>
            byte[] rawData = {
    0x58, 0x4E, 0x42, 0x77, 0x05, 0x00, 0x4E, 0x03, 0x00, 0x00, 0x05, 0x80,
    0x02, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x58,
    0x6E, 0x61, 0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B,
    0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E, 0x41, 0x72, 0x72,
    0x61, 0x79, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x60, 0x31, 0x5B, 0x5B,
    0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x43, 0x6F, 0x6C, 0x6C, 0x65,
    0x63, 0x74, 0x69, 0x6F, 0x6E, 0x73, 0x2E, 0x47, 0x65, 0x6E, 0x65, 0x72,
    0x69, 0x63, 0x2E, 0x4C, 0x69, 0x73, 0x74, 0x60, 0x31, 0x5B, 0x5B, 0x53,
    0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x49, 0x6E, 0x74, 0x33, 0x32, 0x5B,
    0x5D, 0x5B, 0x5D, 0x2C, 0x20, 0x6D, 0x73, 0x63, 0x6F, 0x72, 0x6C, 0x69,
    0x62, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34,
    0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74,
    0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C,
    0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F,
    0x6B, 0x65, 0x6E, 0x3D, 0x62, 0x37, 0x37, 0x61, 0x35, 0x63, 0x35, 0x36,
    0x31, 0x39, 0x33, 0x34, 0x65, 0x30, 0x38, 0x39, 0x5D, 0x5D, 0x2C, 0x20,
    0x6D, 0x73, 0x63, 0x6F, 0x72, 0x6C, 0x69, 0x62, 0x2C, 0x20, 0x56, 0x65,
    0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34, 0x2E, 0x30, 0x2E, 0x30, 0x2E,
    0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E,
    0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C, 0x20, 0x50, 0x75, 0x62, 0x6C,
    0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F, 0x6B, 0x65, 0x6E, 0x3D, 0x62,
    0x37, 0x37, 0x61, 0x35, 0x63, 0x35, 0x36, 0x31, 0x39, 0x33, 0x34, 0x65,
    0x30, 0x38, 0x39, 0x5D, 0x5D, 0x00, 0x00, 0x00, 0x00, 0x8D, 0x01, 0x4D,
    0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x58, 0x6E, 0x61,
    0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B, 0x2E, 0x43,
    0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E, 0x4C, 0x69, 0x73, 0x74, 0x52,
    0x65, 0x61, 0x64, 0x65, 0x72, 0x60, 0x31, 0x5B, 0x5B, 0x53, 0x79, 0x73,
    0x74, 0x65, 0x6D, 0x2E, 0x49, 0x6E, 0x74, 0x33, 0x32, 0x5B, 0x5D, 0x5B,
    0x5D, 0x2C, 0x20, 0x6D, 0x73, 0x63, 0x6F, 0x72, 0x6C, 0x69, 0x62, 0x2C,
    0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34, 0x2E, 0x30,
    0x2E, 0x30, 0x2E, 0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72,
    0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C, 0x20, 0x50,
    0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F, 0x6B, 0x65,
    0x6E, 0x3D, 0x62, 0x37, 0x37, 0x61, 0x35, 0x63, 0x35, 0x36, 0x31, 0x39,
    0x33, 0x34, 0x65, 0x30, 0x38, 0x39, 0x5D, 0x5D, 0x00, 0x00, 0x00, 0x00,
    0x8C, 0x01, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E,
    0x58, 0x6E, 0x61, 0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72,
    0x6B, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E, 0x41, 0x72,
    0x72, 0x61, 0x79, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x60, 0x31, 0x5B,
    0x5B, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x49, 0x6E, 0x74, 0x33,
    0x32, 0x5B, 0x5D, 0x2C, 0x20, 0x6D, 0x73, 0x63, 0x6F, 0x72, 0x6C, 0x69,
    0x62, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34,
    0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74,
    0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C,
    0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F,
    0x6B, 0x65, 0x6E, 0x3D, 0x62, 0x37, 0x37, 0x61, 0x35, 0x63, 0x35, 0x36,
    0x31, 0x39, 0x33, 0x34, 0x65, 0x30, 0x38, 0x39, 0x5D, 0x5D, 0x00, 0x00,
    0x00, 0x00, 0x8A, 0x01, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66,
    0x74, 0x2E, 0x58, 0x6E, 0x61, 0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77,
    0x6F, 0x72, 0x6B, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E,
    0x41, 0x72, 0x72, 0x61, 0x79, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x60,
    0x31, 0x5B, 0x5B, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x49, 0x6E,
    0x74, 0x33, 0x32, 0x2C, 0x20, 0x6D, 0x73, 0x63, 0x6F, 0x72, 0x6C, 0x69,
    0x62, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34,
    0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74,
    0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C,
    0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F,
    0x6B, 0x65, 0x6E, 0x3D, 0x62, 0x37, 0x37, 0x61, 0x35, 0x63, 0x35, 0x36,
    0x31, 0x39, 0x33, 0x34, 0x65, 0x30, 0x38, 0x39, 0x5D, 0x5D, 0x00, 0x00,
    0x00, 0x00, 0x2B, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74,
    0x2E, 0x58, 0x6E, 0x61, 0x2E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F,
    0x72, 0x6B, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x65, 0x6E, 0x74, 0x2E, 0x49,
    0x6E, 0x74, 0x33, 0x32, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x00, 0x00,
    0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00, 0x00, 0x02, 0x02, 0x00, 0x00,
    0x00, 0x03, 0x01, 0x00, 0x00, 0x00, 0x04, 0x02, 0x00, 0x00, 0x00, 0x01,
    0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x03, 0x01, 0x00, 0x00, 0x00,
    0x04, 0x02, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00,
    0x00, 0x02, 0x02, 0x00, 0x00, 0x00, 0x03, 0x01, 0x00, 0x00, 0x00, 0x04,
    0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
    0x03, 0x01, 0x00, 0x00, 0x00, 0x04, 0x02, 0x00, 0x00, 0x00, 0x03, 0x00,
    0x00, 0x00, 0x04, 0x00, 0x00, 0x00
};

            int[] array = { 1, 2, 3, 4 };
            XnbFile<object> deserialized = XnbConvert.Deserialize<object>(rawData);
            string name = deserialized.Content.GetType().AssemblyQualifiedName;
            //Assert.AreEqual(array, deserialized.Content);
        }

        [Test]
        public void TestSerialize()
        {
            List<Dictionary<string, int[][]>>[] complex = new[]
            {
                new List<Dictionary<string, int[][]>>
                {
                    new Dictionary<string, int[][]>
                    {
                        {
                            "test1", new[] {new[] {1, 2, 3}}
                        }
                    }
                },
                new List<Dictionary<string, int[][]>>
                {
                    new Dictionary<string, int[][]>
                    {
                        {
                            "test2", new[] {new[] {4,5, 6}}
                        }
                    }
                }
            };

            string json = JsonConvert.SerializeObject(complex, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });


            var deserializeObject = JsonConvert.DeserializeObject(json, new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All });
            var value = deserializeObject.GetType();
            Console.WriteLine(value);
            byte[] rawData = XnbConvert.Serialize(deserializeObject);
            File.WriteAllBytes("output.xnb", rawData);
            XnbFile<object> deserialized = XnbConvert.Deserialize<object>(rawData);
            

        }
    }
}