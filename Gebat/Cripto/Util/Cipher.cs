using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Cripto.Util
{
    public class Cipher
    {
        #region//Attributes

        private static string[] passs;
        private static string[] salts;

        #endregion

        #region//Pivate Methods

        private static string Encrypt<T>(string value, string password, string salt)
            where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new T();
            byte[] rgbkey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateEncryptor(rgbkey, rgbIV);
            using (MemoryStream buffer = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        private static String Decrypt<T>(String ciphered, string password, string salt)
            where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new T();
            byte[] rgbkey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateDecryptor(rgbkey, rgbIV);
            using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(ciphered)))
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Establece las contraseñas.
        /// </summary>
        /// <param name="pass1">Contraseña de la primera ronda.</param>
        /// <param name="salt1">Sal de la primera ronda</param>
        /// <param name="pass2">Contraseña de la segunda ronda.</param>
        /// <param name="salt2">Sal de la segunda ronda.</param>
        /// <param name="pass3">Contraseña de la tercera ronda.</param>
        /// <param name="salt3">Sal de la tercera ronda.</param>
        public static void SetPassWords(string pass1, string salt1, string pass2, string salt2)
        {
            passs = new string[2];
            salts = new string[2];
            passs[0] = pass1;
            passs[1] = pass2;
            salts[0] = salt1;
            salts[1] = salt2;
        }

        /// <summary>
        /// Método para encriptar el texto entrante, produce una salida encriptada.
        /// </summary>
        /// <param name="toCrypt">Texto a encriptar.</param>
        /// <returns>Texto encriptado</returns>
        public static string Encrypt(string toCrypt)
        {
            string cifrado = Encrypt<TripleDESCryptoServiceProvider>(toCrypt, passs[0], salts[0]);
            cifrado = Encrypt<RijndaelManaged>(cifrado, passs[1], salts[1]);
            return cifrado;
        }

        /// <summary>
        /// Método para desencriptar el texto entrante, produce la salida desencriptada.
        /// </summary>
        /// <param name="toDecrypt">Texto a desencriptar.</param>
        /// <returns>Texto desencriptado.</returns>
        public static string Decrypt(string toDecrypt)
        {
            string descifrado = Decrypt<RijndaelManaged>(toDecrypt, passs[1], salts[1]);
            descifrado = Decrypt<TripleDESCryptoServiceProvider>(descifrado, passs[0], salts[0]);
            return descifrado;
        }

        #endregion
    }
}
