namespace XnbConvert.Readers
{
    public class XnbBooleanReader : XnbReader
    {
        public override int Version => 0;


        public override object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader)
        {
            return xnbStreamReader.ReadBoolean();
        }

        public override void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value)
        {
            if (value is bool boolean) xnbStreamWriter.Write(boolean);
        }

        public override string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider)
        {
            return "Microsoft.Xna.Framework.Content.BooleanReader";
        }
    }
}