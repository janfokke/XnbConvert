//using System;
//using System.Collections.Generic;
//using NUnit.Framework;
//using XnbConvert.Readers;

//namespace XnbConvert.Test
//{
//    [TestFixture]
//    public class XnbTypeReaderResolverTests
//    {
//        #region ReaderNames
//        private const string DictionaryStringStringReaderName = @"Microsoft.Xna.Framework.Content.XnbDictionaryReader`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
//        private const string StringReaderName = @"Microsoft.Xna.Framework.Content.XnbStringReader";
//        #endregion

//        [Test]
//        [TestCase(DictionaryStringStringReaderName, typeof(XnbDictionaryReader<string,string>))]
//        [TestCase(StringReaderName, typeof(XnbStringReader))]
//        public void ResolveTypeReaderFromNameTest(string readerName, Type expectedType)
//        {
//            Type readerType = XnbReaderTypeResolver.ResolveFromName(readerName);
//            Assert.AreEqual(expectedType, readerType);
//        }
        
//        [Test]
//        [TestCase(typeof(Dictionary<string, string>), typeof(XnbDictionaryReader<string,string>))]
//        [TestCase(typeof(string), typeof(XnbStringReader))]
//        public void ResolveTypeReaderFromTargetTypeTest(Type targetType, Type expectedReaderType)
//        {
//            Type readerType = XnbReaderTypeResolver.ResolveFromTargetType(targetType);
//            Assert.AreEqual(expectedReaderType, readerType);
//        }
//    }
//}
