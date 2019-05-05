using System;

namespace XnbConvert
{
    public interface IXnbReaderTypeFactory
    {
        string SimplifiedReaderName { get; }
        string SimplifiedTargetName { get; }
        Type ReaderTypeDefinition { get; }
        Type TargetTypeDefinition { get; }
        Type GetReaderType(Type[] genericParams);
        string GetAssemblyQualifiedTargetName(TypeNameProvider typeNameProvider, Type[] genericParams);
    }
}