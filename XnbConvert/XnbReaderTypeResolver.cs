using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using XnbConvert.Exceptions;
using XnbConvert.Readers;

namespace XnbConvert
{
    public class XnbReaderTypeResolver
    {
        private readonly Dictionary<Type, Func<Type[], Type>> _typeReaderFactories =
            new Dictionary<Type, Func<Type[], Type>>();

        private readonly Dictionary<string, Type> TypesFromTypeNames = new Dictionary<string, Type>
        {
            {"Microsoft.Xna.Framework.Content.ArrayReader", typeof(XnbArrayReader<>)},

            {"System.Nullable", typeof(Nullable<>)},
            {"Microsoft.Xna.Framework.Content.NullableReader", typeof(XnbNullableReader<>)}
        };

        internal void RegisterTypeReaderFactory(IXnbReaderTypeFactory xnbReaderTypeFactory)
        {
            _typeReaderFactories.Add(xnbReaderTypeFactory.TargetTypeDefinition, xnbReaderTypeFactory.GetReaderType);
            TypesFromTypeNames.Add(xnbReaderTypeFactory.SimplifiedReaderName,
                xnbReaderTypeFactory.ReaderTypeDefinition);
            TypesFromTypeNames.Add(xnbReaderTypeFactory.SimplifiedTargetName,
                xnbReaderTypeFactory.TargetTypeDefinition);
        }

        private int GetGenericCountFromTypeName(string fullname)
        {
            var split = fullname.Split(',', '`');
            if (split.Length <= 1)
                return 0;

            if (split[1].StartsWith(" ")) return 0;

            var numberResolver = new Regex(@"[0-9]+");
            var match = numberResolver.Match(split[1]);
            return match.Success ? int.Parse(match.Value) : 0;
        }


        public Type ResolveFromTargetType<TTargetType>()
        {
            return ResolveFromTargetType(typeof(TTargetType));
        }

        public Type ResolveFromTargetType(Type targetType)
        {
            if (targetType.IsArray) return typeof(XnbArrayReader<>).MakeGenericType(targetType.GetElementType());

            var targetTypeGenericDefinition =
                targetType.IsGenericType ? targetType.GetGenericTypeDefinition() : targetType;

            if (targetTypeGenericDefinition == typeof(Nullable<>))
                return typeof(XnbNullableReader<>).MakeGenericType(Nullable.GetUnderlyingType(targetType));

            if (targetTypeGenericDefinition.IsArray)
            {
                if (targetType.GetArrayRank() != 1)
                    throw new NotSupportedException(
                        "Multidimensional arrays are not supported, use a jagged array instead");
                return typeof(XnbArrayReader<>).MakeGenericType(targetTypeGenericDefinition.GetElementType());
            }


            if (!_typeReaderFactories.TryGetValue(targetTypeGenericDefinition, out var factoryFunc))
                throw new NotSupportedException($"{targetType.Name} is not supported");

            return factoryFunc(targetType.GenericTypeArguments);
        }

        public Type ResolveFromName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) throw new InvalidXnbTypeReaderNameException(fullName);

            var simplifiedName = XnbUtils.GetSimplifiedName(fullName, out var arrayCount);
            if (!TypesFromTypeNames.TryGetValue(simplifiedName, out var mainType))
                throw new XnbException($"{simplifiedName} is not supported");

            var genericCount = GetGenericCountFromTypeName(fullName);
            var genericType = mainType;
            if (genericCount != 0)
            {
                var genericTypesString = GetStringsBetweenMostOuterBrackets(fullName).First();
                var innerTypes = new Type[genericCount];
                var innerTypeFullNames = GetStringsBetweenMostOuterBrackets(genericTypesString);
                for (var index = 0; index < innerTypeFullNames.Count; index++)
                {
                    var innerType = innerTypeFullNames[index];
                    innerTypes[index] = ResolveFromName(innerType);
                }

                genericType = mainType.MakeGenericType(innerTypes.ToArray());
            }

            for (var i = 0; i < arrayCount; i++) genericType = genericType.MakeArrayType();

            return genericType;
        }

        private List<string> GetStringsBetweenMostOuterBrackets(string value)
        {
            var regex = new Regex(@"(?:[\[])(?:[^\[\]]|(?<Open>[\[])|(?<-Open>[\]]))*\]");
            var genericMatch = regex.Match(value);

            var matches = new List<string>();
            while (true)
            {
                if (!genericMatch.Success)
                    break;
                var matchWithBrackets = genericMatch.Value;
                matches.Add(RemoveFirstAndLastChar(matchWithBrackets));
                genericMatch = genericMatch.NextMatch();
                if (!genericMatch.Success)
                    break;
            }

            return matches;
        }

        private string RemoveFirstAndLastChar(string matchWithBrackets)
        {
            return matchWithBrackets.Substring(1, matchWithBrackets.Length - 2);
        }
    }
}