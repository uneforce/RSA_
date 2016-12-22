using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Numerics;
using System.Text;

namespace RSA_test
{
    [TestClass]
    public class RSA_test
    {
        RSA_lib.RSAclass bufferRSA = new RSA_lib.RSAclass();
        RSA_lib.RSA RSA = new RSA_lib.RSA();
        RSA_lib.key publicKey = new RSA_lib.key(17, 249);
        RSA_lib.key privateKey = new RSA_lib.key(29, 249);
        MemoryStream EncipherStream = new MemoryStream();
        string value = "test";
        string encrypted = "_ [_";

        [TestMethod]
        public void DecipherEncipher()
        {
            MemoryStream DecipherStream = new MemoryStream();
            MemoryStream SourceStream = new MemoryStream(Encoding.Default.GetBytes(value));
            bufferRSA.DataToRSA(SourceStream, EncipherStream, publicKey);
            EncipherStream.Position = 0;
            bufferRSA.RSAToData(EncipherStream, DecipherStream, privateKey);
            StreamReader reader = new StreamReader(DecipherStream);
            DecipherStream.Position = 0;
            string decryptedDataString = reader.ReadToEnd();
            Assert.AreEqual(value, decryptedDataString);

        }
    }
}
