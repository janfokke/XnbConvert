using System;

namespace XnbConvert
{
    public class XnbConvert
    {
        public static Func<XnbConvertSettings> DefaultSettings { get; set; } = () => XnbConvertSettings.Default;

        public static XnbFile<T> Deserialize<T>(byte[] data)
        {
            return Deserialize<T>(data, DefaultSettings());
        }

        public static XnbFile<T> Deserialize<T>(byte[] data, XnbConvertSettings xnbConvertSettings)
        {
            var deserializer = new XnbDeserializer(xnbConvertSettings);
            return deserializer.Deserialize<T>(data);
        }

        public static byte[] Serialize<T>(T instance,
            XnbTargetOs xnbTargetOs = XnbTargetOs.MicrosoftWindow,
            byte formatVersion = 5,
            XnbFlags flags = 0)
        {
            return Serialize(instance, DefaultSettings(), xnbTargetOs, formatVersion, flags);
        }

        public static byte[] Serialize<T>(T instance, XnbConvertSettings xnbConvertSettings,
            XnbTargetOs xnbTargetOs = XnbTargetOs.MicrosoftWindow,
            byte formatVersion = 5,
            XnbFlags flags = 0)
        {
            var serializer = new XnbSerializer(xnbConvertSettings);
            return serializer.Serialize(instance, xnbTargetOs, formatVersion, flags);
        }
    }
}