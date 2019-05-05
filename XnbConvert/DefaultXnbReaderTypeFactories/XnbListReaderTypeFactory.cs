using System;
using System.Collections.Generic;
using XnbConvert.Readers;

namespace XnbConvert.DefaultXnbReaderTypeFactories
{
    internal class XnbListReaderTypeFactory : XnbReaderTypeFactory
    {
        private static readonly Lazy<XnbListReaderTypeFactory> _lazyInstance = new Lazy<XnbListReaderTypeFactory>();

        public XnbListReaderTypeFactory() : base(typeof(List<>), typeof(XnbListReader<>))
        {
        }

        public static XnbListReaderTypeFactory Instance => _lazyInstance.Value;

        public override string SimplifiedReaderName => "Microsoft.Xna.Framework.Content.ListReader";
        public override string SimplifiedTargetName => "System.Collections.Generic.List";
    }
}