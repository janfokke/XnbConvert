using System;
using XnbConvert.DefaultXnbReaderTypeFactories;

namespace XnbConvert
{
    public class XnbConvertSettings
    {
        private static readonly Lazy<XnbConvertSettings> XnbConvertSettingsLazy = new Lazy<XnbConvertSettings>(() =>
        {
            return new XnbConvertSettings();
        });

        public XnbConvertSettings()
        {
            RegisterDefaultXnbReaderTypeFactories();
        }

        public XnbReaderTypeResolver XnbReaderTypeResolver { get; } = new XnbReaderTypeResolver();
        internal TypeNameProvider TypeNameProvider { get; } = new TypeNameProvider();

        public static XnbConvertSettings Default => XnbConvertSettingsLazy.Value;

        public void RegisterXnbReaderTypeFactory(IXnbReaderTypeFactory xnbReaderTypeFactory)
        {
            XnbReaderTypeResolver.RegisterTypeReaderFactory(xnbReaderTypeFactory);
            if (xnbReaderTypeFactory.TargetTypeDefinition != null)
                TypeNameProvider.AddTargetTypeNameResolver(
                    xnbReaderTypeFactory.TargetTypeDefinition,
                    xnbReaderTypeFactory.GetAssemblyQualifiedTargetName);
        }

        private void RegisterDefaultXnbReaderTypeFactories()
        {
            RegisterXnbReaderTypeFactory(XnbBooleanReaderTypeFactory.Instance);
            RegisterXnbReaderTypeFactory(XnbDictionaryReaderTypeFactory.Instance);
            RegisterXnbReaderTypeFactory(XnbIntReaderTypeFactory.Instance);
            RegisterXnbReaderTypeFactory(XnbListReaderTypeFactory.Instance);
            RegisterXnbReaderTypeFactory(XnbStringReaderTypeFactory.Instance);
            RegisterXnbReaderTypeFactory(XnbSingleReaderTypeFactory.Instance);
        }
    }
}