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
        public void TestEncryptDecrypt()
        {
            String tocrypt = "12345678A";
            Cipher.SetPassWords("contra1", "salt1", "contra2", "salt2");
            String encripado = Cipher.Encrypt(tocrypt);
            String descifrado = Cipher.Decrypt(encripado);

            Assert.AreEqual(tocrypt, descifrado);
        }

        [TestMethod]
        public void HashingCorrecto()
        {
            String hash1 = Hasher.CreateHash("contradehash");
            bool hashed = Hasher.ValidatePassword("contradehash", hash1);

            Assert.IsTrue(hashed);
        }

        [TestMethod]
        public void HashingIncorrecto()
        {
            String hash1 = Hasher.CreateHash("controdehash");
            bool hashed = Hasher.ValidatePassword("contradehash", hash1);

            Assert.IsFalse(hashed);
        }

        [TestMethod]
        public void HashingIncorrecto2()
        {
            String hash1 = Hasher.CreateHash("unacontra");
            bool hashed = Hasher.ValidatePassword("estaesotracontra",hash1);

            Assert.IsFalse(hashed);
        }
    }
}
