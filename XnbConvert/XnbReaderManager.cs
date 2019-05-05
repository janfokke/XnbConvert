using System;
using System.Collections.Generic;
using System.Linq;

namespace XnbConvert
{
    public class XnbReaderManager
    {
        private readonly XnbReaderTypeResolver _xnbReaderTypeResolver;
        private readonly List<XnbReader> _xnbTypeReaders = new List<XnbReader>();
        private bool _resolvedReaders;

        public XnbReaderManager(XnbReaderTypeResolver xnbReaderTypeResolver)
        {
            _xnbReaderTypeResolver = xnbReaderTypeResolver;
        }

        public XnbReaderManager(XnbReader initialReader, XnbReaderTypeResolver xnbReaderTypeResolver) : this(
            xnbReaderTypeResolver)
        {
            _xnbTypeReaders.Add(initialReader);
        }

        public List<XnbReader> XnbTypeReaders => _xnbTypeReaders.ToList();

        public void ReadReaders(XnbStreamReader streamReader)
        {
            lock (_xnbTypeReaders)
            {
                if (_resolvedReaders)
                    throw new XnbException("Already resolved readers");
                _resolvedReaders = true;

                var readerCount = streamReader.Read7BitEncodedInt();
                for (var i = 0; i < readerCount; i++)
                {
                    var readerName = streamReader.ReadString();
                    var version = streamReader.ReadInt32();
                    var readerType = _xnbReaderTypeResolver.ResolveFromName(readerName);
                    var reader = XnbReaderFactory.CreateReaderFromType(readerType);
                    _xnbTypeReaders.Add(reader);
                }
            }
        }


        public (int index, XnbReader reader) GetOrAddXnbTypeReaderFromTargetType<TTargetType>()
        {
            return GetOrAddXnbTypeReaderFromTargetType(typeof(TTargetType));
        }

        public (int index, XnbReader reader) GetOrAddXnbTypeReaderFromTargetType(Type targetType)
        {
            var readerType = _xnbReaderTypeResolver.ResolveFromTargetType(targetType);
            return XnbTypeReaderFromReaderType(readerType);
        }

        public (int index, XnbReader reader) XnbTypeReaderFromReaderType<TReader>() where TReader : XnbReader
        {
            return XnbTypeReaderFromReaderType(typeof(TReader));
        }

        public (int index, XnbReader reader) XnbTypeReaderFromReaderType(Type readerType)
        {
            lock (_xnbTypeReaders)
            {
                var index = _xnbTypeReaders.FindIndex(x => x.GetType().IsAssignableFrom(readerType));
                if (index == -1)
                {
                    var readerInstance = XnbReaderFactory.CreateReaderFromType(readerType);
                    _xnbTypeReaders.Add(readerInstance);
                    index = _xnbTypeReaders.Count - 1;
                }

                var reader = _xnbTypeReaders[index];
                return (index + 1, reader);
            }
        }

        public XnbReader XnbReaderFromIndex(int index)
        {
            return index == 0 ? null : _xnbTypeReaders[index - 1];
        }
    }
}