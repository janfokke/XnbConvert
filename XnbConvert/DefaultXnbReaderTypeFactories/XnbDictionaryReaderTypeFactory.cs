using System;
using System.Collections.Generic;
using XnbConvert.Readers;

namespace XnbConvert.DefaultXnbReaderTypeFactories
{
    internal class XnbDictionaryReaderTypeFactory : XnbReaderTypeFactory
    {
        private static readonly Lazy<XnbDictionaryReaderTypeFactory> _lazyInstance =
            new Lazy<XnbDictionaryReaderTypeFactory>();

        public XnbDictionaryReaderTypeFactory() : base(typeof(Dictionary<,>), typeof(XnbDictionaryReader<,>))
        {
        }

        public static XnbDictionaryReaderTypeFactory Instance => _lazyInstance.Value;

        public override string SimplifiedReaderName => "Microsoft.Xna.Framework.Content.DictionaryReader";
        public override string SimplifiedTargetName => "System.Collections.Generic.Dictionary";
    }
}