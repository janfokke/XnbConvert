using System;
using XnbConvert.Readers;

namespace XnbConvert.DefaultXnbReaderTypeFactories
{
    internal class XnbSingleReaderTypeFactory : XnbReaderTypeFactory<string, XnbStringReader>
    {
        private static readonly Lazy<XnbSingleReaderTypeFactory> _lazyInstance = new Lazy<XnbSingleReaderTypeFactory>();
        public static XnbSingleReaderTypeFactory Instance => _lazyInstance.Value;

        public override string SimplifiedReaderName => "Microsoft.Xna.Framework.Content.SingleReader";
    }
}