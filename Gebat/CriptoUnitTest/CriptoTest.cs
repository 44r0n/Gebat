using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cripto.Util;
using LLCryptoLib.Crypto;
using System.Security.Cryptography;

namespace CriptoUnitTest
{
    [TestClass]
    public class CriptoTest
    {
        [TestMethod]
        public void TestBlowFish()
        {
            String tocrypt = "hola mundo";
            String cifrado = Cipher.Encrypt<BlowfishManaged>(tocrypt, "contra", "saltedea");
            String descifrado = Cipher.Decrypt<BlowfishManaged>(cifrado, "contra", "saltedea");
            Assert.AreEqual(tocrypt, descifrado);
        }

        [TestMethod]
        public void TestAES()
        {
            String tocrypt = "hola mundo";
            String cifrado = Cipher.Encrypt<AesManaged>(tocrypt, "contra", "saltedea");
            String descifrado = Cipher.Decrypt<AesManaged>(cifrado, "contra", "saltedea");
            Assert.AreEqual(tocrypt, descifrado);
        }

        [TestMethod]
        public void TestAll()
        {
            String tocrypt = "12345678A";
            String cifrado = Cipher.Encrypt<BlowfishManaged>(tocrypt, "contra", "saltedea");
            cifrado = Cipher.Encrypt<TripleDESCryptoServiceProvider>(cifrado, "otracon", "otrasalde");
            cifrado = Cipher.Encrypt<AesManaged>(cifrado, "terceracon", "saldeaes");
            cifrado = Cipher.Encrypt<RijndaelManaged>(cifrado, "conderijn", "salderij");

            String descifrado = Cipher.Decrypt<RijndaelManaged>(cifrado, "conderijn", "salderij");
            descifrado = Cipher.Decrypt<AesManaged>(descifrado, "terceracon", "saldeaes");
            descifrado = Cipher.Decrypt<TripleDESCryptoServiceProvider>(descifrado, "otracon", "otrasalde");
            descifrado = Cipher.Decrypt<BlowfishManaged>(descifrado, "contra", "saltedea");

            Assert.AreEqual(tocrypt, descifrado);
        }
    }
}
