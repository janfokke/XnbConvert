namespace XnbConvert.Readers
{
    public class XnbSingleReader : XnbReader
    {
        public override int Version => 0;

        public override object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader)
        {
            return xnbStreamReader.ReadSingle();
        }

        public override void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value)
        {
            if (value is float number) xnbStreamWriter.Write(number);
        }

        public override string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider)
        {
            return "Microsoft.Xna.Framework.Content.SingleReader";
        }
    }
}