using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cripto.Util;
using System.Security.Cryptography;

namespace CriptoUnitTest
{
    [TestClass]
    public class CriptoTest
    {

        [TestMethod]
        public void TestAll()
        {
            String tocrypt = "12345678A";
            Cipher c = new Cipher("contra", "saltedea", "conderijn", "salderijn");
            String cifrado = c.Encrypt(tocrypt);

            String descifrado = c.Decrypt(cifrado);

            Assert.AreEqual(tocrypt, descifrado);
        }

        [TestMethod]
        public void Hashing()
        {
            
        }
    }
}
