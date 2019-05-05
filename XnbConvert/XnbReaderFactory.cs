using System;
using System.Collections.Generic;

namespace XnbConvert
{
    public class XnbReaderFactory
    {
        private static readonly Dictionary<Type, WeakReference<XnbReader>> ReaderCache =
            new Dictionary<Type, WeakReference<XnbReader>>();

        private static readonly object ReaderCacheLock = new object();

        public XnbReader CreateReaderFromType<TXnbReader>()
        {
            return CreateReaderFromType(typeof(TXnbReader));
        }

        public static XnbReader CreateReaderFromType(Type type)
        {
            if (type.IsAssignableFrom(typeof(XnbReader))) throw new XnbException("Not a valid XnbReader");

            lock (ReaderCacheLock)
            {
                if (ReaderCache.TryGetValue(type, out var result))
                {
                    if (result.TryGetTarget(out var target)) return target;
                    //Renew
                    var instance = CreateInstance(type);
                    ReaderCache[type].SetTarget(instance);
                    return instance;
                }
                else
                {
                    var instance = CreateInstance(type);
                    ReaderCache[type] = new WeakReference<XnbReader>(instance);
                    return instance;
                }
            }
        }

        private static XnbReader CreateInstance(Type type)
        {
            return Activator.CreateInstance(type) as XnbReader;
        }
    }
}