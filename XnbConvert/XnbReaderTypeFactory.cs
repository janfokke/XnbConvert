using System;

namespace XnbConvert
{
    public abstract class XnbReaderTypeFactory : IXnbReaderTypeFactory
    {
        public XnbReaderTypeFactory(Type targetType, Type readerType)
        {
            if (targetType.IsGenericType && !targetType.IsGenericTypeDefinition)
                throw new XnbException($"{nameof(targetType)} can only be a GenericTypeDefinition or a NonGenericType");
            TargetTypeDefinition = targetType;

            if (readerType.IsGenericType && !readerType.IsGenericTypeDefinition)
                throw new XnbException($"{nameof(readerType)} can only be a GenericTypeDefinition or a NonGenericType");
            ReaderTypeDefinition = readerType;
            SimplifiedTargetName = XnbUtils.GetSimplifiedName(targetType.AssemblyQualifiedName, out _);
        }

        public virtual string SimplifiedTargetName { get; }

        public Type TargetTypeDefinition { get; }

        public Type ReaderTypeDefinition { get; }

        /// <example>System.Int32</example>
        /// <returns>The full Type name including namespace of the <see cref="XnbReader" /> without any generic and array symbols</returns>
        public abstract string SimplifiedReaderName { get; }

        /// <summary>
        ///     Should return the assembly qualified name of <see cref="TTarget" />
        /// </summary>
        /// <remarks>
        ///     Make sure to use <see cref="TypeNameProvider.GetAssemblyQualifiedName{TTarget}" /> when dealing with generic names
        /// </remarks>
        /// <seealso cref="Type.AssemblyQualifiedName" />
        public virtual string GetAssemblyQualifiedTargetName(TypeNameProvider typeNameProvider, Type[] genericParams)
        {
            if (TargetTypeDefinition.IsGenericType)
                return TargetTypeDefinition.MakeGenericType(genericParams).AssemblyQualifiedName;
            return TargetTypeDefinition.AssemblyQualifiedName;
        }

        public virtual Type GetReaderType(Type[] genericParams)
        {
            if (ReaderTypeDefinition.IsGenericType)
                return ReaderTypeDefinition.MakeGenericType(genericParams);
            return ReaderTypeDefinition;
        }
    }

    public abstract class XnbReaderTypeFactory<TTarget, TReader> : XnbReaderTypeFactory where TReader : XnbReader
    {
        protected XnbReaderTypeFactory() : base(typeof(TTarget), typeof(TReader))
        {
        }
    }
}