namespace XnbConvert
{
    public abstract class XnbReader
    {
        public abstract int Version { get; }

        public abstract object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader);

        public abstract void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value);

        public abstract string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider);

        protected XnbReader GetXnbTypeReader<TTargetType>(XnbReaderManager xnbReaderManager,
            XnbStreamReader xnbStreamReader)
        {
            XnbReader xnbReader;

            if (typeof(TTargetType).IsValueType)
            {
                xnbReader = xnbReaderManager.GetOrAddXnbTypeReaderFromTargetType<TTargetType>().reader;
            }
            else
            {
                var keyReaderIndex = xnbStreamReader.Read7BitEncodedInt();
                xnbReader = xnbReaderManager.XnbReaderFromIndex(keyReaderIndex);
            }

            return xnbReader;
        }
    }
}