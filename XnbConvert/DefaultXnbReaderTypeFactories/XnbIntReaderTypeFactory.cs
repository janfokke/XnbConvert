using System;
using XnbConvert.Readers;

namespace XnbConvert.DefaultXnbReaderTypeFactories
{
    internal class XnbIntReaderTypeFactory : XnbReaderTypeFactory<int, XnbIntegerReader>
    {
        private static readonly Lazy<XnbIntReaderTypeFactory> _lazyInstance = new Lazy<XnbIntReaderTypeFactory>();
        public static XnbIntReaderTypeFactory Instance => _lazyInstance.Value;

        public override string SimplifiedReaderName => "Microsoft.Xna.Framework.Content.Int32Reader";
        public override string SimplifiedTargetName => "System.Int32";
    }
}