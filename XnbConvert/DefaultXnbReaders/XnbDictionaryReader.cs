using System;
using System.Collections.Generic;

namespace XnbConvert.Readers
{
    public class XnbDictionaryReader<TKeyType, TValueType> : XnbReader
    {
        public XnbDictionaryReader()
        {
            Version = 0;
        }

        public override int Version { get; }

        public override string GetAssemblyQualifiedName(TypeNameProvider typeNameProvider)
        {
            return
                $"Microsoft.Xna.Framework.Content.DictionaryReader`2[[{typeNameProvider.GetAssemblyQualifiedName<TKeyType>()}],[{typeNameProvider.GetAssemblyQualifiedName<TValueType>()}]]";
        }

        public override object Read(XnbReaderManager xnbReaderManager, XnbStreamReader xnbStreamReader)
        {
            var dictionary = new Dictionary<TKeyType, TValueType>();
            var count = xnbStreamReader.ReadInt32();

            for (var i = 0; i < count; i++)
            {
                var xnbKeyTypeReader = GetXnbTypeReader<TKeyType>(xnbReaderManager, xnbStreamReader);
                var key = (TKeyType) xnbKeyTypeReader.Read(xnbReaderManager, xnbStreamReader);

                var xnbValueTypeReader = GetXnbTypeReader<TValueType>(xnbReaderManager, xnbStreamReader);
                var value = (TValueType) xnbValueTypeReader.Read(xnbReaderManager, xnbStreamReader);

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public override void Write(XnbReaderManager xnbReaderManager, XnbStreamWriter xnbStreamWriter, object value)
        {
            if (value is Dictionary<TKeyType, TValueType> dictionary)
            {
                xnbStreamWriter.Write(dictionary.Count);
                foreach (var keyValuePair in dictionary)
                {
                    var (keyReaderIndex, keyReader) = xnbReaderManager.GetOrAddXnbTypeReaderFromTargetType<TKeyType>();
                    if (!typeof(TKeyType).IsValueType)
                        xnbStreamWriter.Write7BitEncodedInt(keyReaderIndex);
                    keyReader.Write(xnbReaderManager, xnbStreamWriter, keyValuePair.Key);

                    var (valueReaderIndex, valueReader) =
                        xnbReaderManager.GetOrAddXnbTypeReaderFromTargetType<TValueType>();
                    if (!typeof(TValueType).IsValueType)
                        xnbStreamWriter.Write7BitEncodedInt(valueReaderIndex);
                    valueReader.Write(xnbReaderManager, xnbStreamWriter, keyValuePair.Value);
                }
            }
            else
            {
                throw new ArgumentException(
                    $"Expected {typeof(Dictionary<TKeyType, TValueType>).Name}, got {value.GetType().Name}");
            }
        }
    }
}