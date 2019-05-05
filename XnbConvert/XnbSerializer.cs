using System.IO;

namespace XnbConvert
{
    public class XnbSerializer
    {
        public XnbSerializer(XnbConvertSettings xnbConvertSettings)
        {
            XnbConvertSettings = xnbConvertSettings;
        }

        public XnbConvertSettings XnbConvertSettings { get; }

        public byte[] Serialize<T>(T value,
            XnbTargetOs xnbTargetOs = XnbTargetOs.MicrosoftWindow,
            byte formatVersion = 5,
            XnbFlags flags = 0
        )
        {
            var xnbStreamWriter = new XnbStreamWriter(new MemoryStream());

            // write XNB default header
            xnbStreamWriter.Write('X');
            xnbStreamWriter.Write('N');
            xnbStreamWriter.Write('B');

            // write XNB target OS
            xnbStreamWriter.Write((byte) xnbTargetOs);

            // write XNB format version
            xnbStreamWriter.Write(formatVersion);

            // write XNB flags
            // remove compression flag
            flags = flags & ~XnbFlags.ContentCompressedLzx;
            xnbStreamWriter.Write((byte) flags);

            //tmp file size
            var sizeIndex = xnbStreamWriter.BaseStream.Position;
            xnbStreamWriter.Write(0);

            //TODO: encoding here
            var readerType = XnbConvertSettings.XnbReaderTypeResolver.ResolveFromTargetType(value.GetType());
            var reader = XnbReaderFactory.CreateReaderFromType(readerType);
            var xnbReaderManager = new XnbReaderManager(reader, XnbConvertSettings.XnbReaderTypeResolver);
            var tmpStream = new XnbStreamWriter(new MemoryStream());
            reader.Write(xnbReaderManager, tmpStream, value);
            var xnbTypeReaders = xnbReaderManager.XnbTypeReaders;

            // write reader count
            xnbStreamWriter.Write7BitEncodedInt(xnbTypeReaders.Count);

            // write readers and version
            foreach (var xnbTypeReader in xnbTypeReaders)
            {
                xnbStreamWriter.Write(xnbTypeReader.GetAssemblyQualifiedName(XnbConvertSettings.TypeNameProvider));
                xnbStreamWriter.Write(xnbTypeReader.Version);
            }

            // no shared resources
            xnbStreamWriter.Write7BitEncodedInt(0);

            // write initial reader index
            xnbStreamWriter.Write7BitEncodedInt(1);

            tmpStream.BaseStream.Position = 0;
            tmpStream.BaseStream.CopyTo(xnbStreamWriter.BaseStream);

            xnbStreamWriter.BaseStream.Position = sizeIndex;
            xnbStreamWriter.Write((int) xnbStreamWriter.BaseStream.Length);

            using (var ms = new MemoryStream())
            {
                xnbStreamWriter.BaseStream.Position = 0;
                xnbStreamWriter.BaseStream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}