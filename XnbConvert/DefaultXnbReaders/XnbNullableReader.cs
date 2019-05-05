namespace XnbConvert.Readers
{
    public class XnbNullableReader<T> : XnbReader where T : struct

    {
        public override int Version => 0;


        public override object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader)
        {
            if (xnbStreamReader.ReadBoolean())
            {
                var read = new T?((T) GetXnbTypeReader<T>(xnbReaderManager, xnbStreamReader)
                    .Read(xnbReaderManager, xnbStreamReader));
                return read;
            }

            return null;
        }

        public override void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value)
        {
            var nullable = (T?) value;
            if (nullable.HasValue)
            {
                xnbStreamWriter.Write(true);
                var (index, reader) = xnbReaderManager.GetOrAddXnbTypeReaderFromTargetType<T>();
                if (!typeof(T).IsValueType)
                    xnbStreamWriter.Write7BitEncodedInt(index);
                reader.Write(xnbReaderManager, xnbStreamWriter, nullable.Value);
            }
            else
            {
                xnbStreamWriter.Write(false);
            }
        }

        public override string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider)
        {
            return
                $"Microsoft.Xna.Framework.Content.NullableReader`1[[{typeNameProvider.GetAssemblyQualifiedName<T>()}]]";
        }
    }
}