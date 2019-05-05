using System;

namespace XnbConvert.Readers
{
    public class XnbArrayReader<T> : XnbReader
    {
        public XnbArrayReader()
        {
            Version = 0;
        }

        public override int Version { get; }

        public override string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider)
        {
            return $"Microsoft.Xna.Framework.Content.ArrayReader`1[[{typeNameProvider.GetAssemblyQualifiedName<T>()}]]";
        }

        public override object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader)
        {
            var count = xnbStreamReader.ReadInt32();
            var array = new T[count];

            for (var i = 0; i < count; i++)
            {
                var xnbValueTypeReader = GetXnbTypeReader<T>(xnbReaderManager, xnbStreamReader);
                array[i] = (T) xnbValueTypeReader.Read(xnbReaderManager, xnbStreamReader);
            }

            return array;
        }

        public override void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value)
        {
            if (value is T[] array)
            {
                xnbStreamWriter.Write(array.Length);
                foreach (var v in array)
                {
                    var (keyReaderIndex, keyReader) = xnbReaderManager.GetOrAddXnbTypeReaderFromTargetType<T>();
                    if (!typeof(T).IsValueType)
                        xnbStreamWriter.Write7BitEncodedInt(keyReaderIndex);
                    keyReader.Write(xnbReaderManager, xnbStreamWriter, v);
                }
            }
            else
            {
                throw new ArgumentException($"Expected {typeof(T[]).Name}, got {value.GetType().Name}");
            }
        }
    }
}