using System;
using System.Collections.Generic;

namespace XnbConvert.Readers
{
    public class XnbListReader<T> : XnbReader
    {
        public XnbListReader()
        {
            Console.WriteLine(typeof(int[]).AssemblyQualifiedName);
            Version = 0;
        }

        public override int Version { get; }

        public override string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider)
        {
            return $"Microsoft.Xna.Framework.Content.ListReader`1[[{typeNameProvider.GetAssemblyQualifiedName<T>()}]]";
        }

        public override object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader)
        {
            var count = xnbStreamReader.ReadInt32();
            var list = new List<T>(count);

            for (var i = 0; i < count; i++)
            {
                var xnbValueTypeReader = GetXnbTypeReader<T>(xnbReaderManager, xnbStreamReader);
                list.Add((T) xnbValueTypeReader.Read(xnbReaderManager, xnbStreamReader));
            }

            return list;
        }

        public override void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value)
        {
            if (value is List<T> list)
            {
                xnbStreamWriter.Write(list.Count);
                foreach (var v in list)
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