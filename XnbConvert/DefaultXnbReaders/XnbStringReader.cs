namespace XnbConvert.Readers
{
    public class XnbStringReader : XnbReader
    {
        public override int Version => 0;

        public override object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader)
        {
            return xnbStreamReader.ReadString();
        }

        public override void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value)
        {
            if (value is string text) xnbStreamWriter.Write(text);
        }

        public override string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider)
        {
            return "Microsoft.Xna.Framework.Content.StringReader";
        }
    }
}