using System;
using System.Collections.Generic;

namespace XnbConvert
{
    /// <summary>
    ///     Not all XNB types reside in .NET framework, for example, the Vector type.
    ///     This library contains its own implementation of those types.
    ///     That's why the GetAssemblyQualifiedName property cannot be used on every type.
    /// </summary>
    public class TypeNameProvider
    {
        private readonly Dictionary<Type, Func<TypeNameProvider, Type[], string>> _resolvers =
            new Dictionary<Type, Func<TypeNameProvider, Type[], string>>();

        internal void AddTargetTypeNameResolver(Type targetTypeDefinition,
            Func<TypeNameProvider, Type[], string> resolveAssemblyQualifiedTargetName)
        {
            _resolvers.Add(targetTypeDefinition, resolveAssemblyQualifiedTargetName);
        }

        public string GetAssemblyQualifiedName<TTarget>()
        {
            var type = typeof(TTarget);
            var genericParams = type.IsGenericType ? type.GetGenericArguments() : Array.Empty<Type>();
            var genericTypeDefinition = type.IsGenericType ? type.GetGenericTypeDefinition() : type;

            return _resolvers.TryGetValue(genericTypeDefinition, out var resolver)
                ? resolver(this, genericParams)
                : type.AssemblyQualifiedName;
        }
    }
}