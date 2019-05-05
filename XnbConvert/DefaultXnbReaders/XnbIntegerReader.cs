namespace XnbConvert.Readers
{
    public class XnbIntegerReader : XnbReader
    {
        public override int Version => 0;

        public override object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader)
        {
            return xnbStreamReader.ReadInt32();
        }

        public override void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value)
        {
            if (value is int number) xnbStreamWriter.Write(number);
        }

        public override string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider)
        {
            return "Microsoft.Xna.Framework.Content.Int32Reader";
        }
    }
}