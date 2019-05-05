using System;
using XnbConvert.Readers;

namespace XnbConvert.DefaultXnbReaderTypeFactories
{
    internal class XnbBooleanReaderTypeFactory : XnbReaderTypeFactory<bool, XnbBooleanReader>
    {
        private static readonly Lazy<XnbBooleanReaderTypeFactory> _lazyInstance =
            new Lazy<XnbBooleanReaderTypeFactory>();

        public static XnbBooleanReaderTypeFactory Instance => _lazyInstance.Value;

        public override string SimplifiedReaderName => "Microsoft.Xna.Framework.Content.BooleanReader";
        public override string SimplifiedTargetName => "System.Boolean";
    }
}