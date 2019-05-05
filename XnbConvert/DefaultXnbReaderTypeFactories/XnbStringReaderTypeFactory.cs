using System;
using XnbConvert.Readers;

namespace XnbConvert.DefaultXnbReaderTypeFactories
{
    internal class XnbStringReaderTypeFactory : XnbReaderTypeFactory<float, XnbStringReader>
    {
        private static readonly Lazy<XnbStringReaderTypeFactory> _lazyInstance = new Lazy<XnbStringReaderTypeFactory>();
        public static XnbStringReaderTypeFactory Instance => _lazyInstance.Value;

        public override string SimplifiedReaderName => "Microsoft.Xna.Framework.Content.StringReader";
    }
}