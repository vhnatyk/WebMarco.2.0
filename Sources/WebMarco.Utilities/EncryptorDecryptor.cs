
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace WebMarco.Utilities.Encryption {
    public class EncryptorDecryptor {

        //can be any string
        private static string PassPhrase {//TODO: from Task setting or Project level setting
            get {
                return "gAffk1908";
                //throw new NotImplementedException("You must set your custom pass phrase, anything like \"gAffk1908\" for example.");
            }
        }

        //must be 16 bytes, for 128 bit key size
        public static string InitVector {//TODO: from Task setting or Project level setting
            get {

                return "Fg65djskkHHkgyrl";
                //throw new NotImplementedException("You must set your custom int vector, anything like \"Fg65djskkHHkgyrl\" for example. Must be 16 bytes!");
            }
        }

        //can be 256 
        private const int keySize = 128;

        private readonly static byte[] initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
        private static ICryptoTransform decryptor;
        private static ICryptoTransform Decryptor {
            get {
                if ((decryptor == null)) {
                    decryptor = SymmetricKey.CreateDecryptor(KeyBytes, initVectorBytes);
                }
                return decryptor;
            }
        }

        private static ICryptoTransform encryptor;
        private static ICryptoTransform Encryptor {
            get {
                if ((encryptor == null)) {
                    encryptor = SymmetricKey.CreateEncryptor(KeyBytes, initVectorBytes);
                }
                return encryptor;
            }
        }

        private static byte[] keyBytes;
        private static byte[] KeyBytes {
            get {
                if ((keyBytes == null)) {
                    //throw new NotImplementedException("You must set your init key bytes. Must be 16 bytes for 128bit key size and 32 for 256 respectively!");
                    ////var password = new Rfc2898DeriveBytes(passPhrase, 16, 1)
                    keyBytes = Convert.FromBase64String("np3hPWpwkMZYaXaxxqB86A==");//hard coded                    
                    ////password.GetBytes(keySize / 8)                    
                }
                return keyBytes;
            }
        }

        private static RijndaelManaged symmetricKey;
        private static RijndaelManaged SymmetricKey {
            get {
                if ((symmetricKey == null)) {
                    symmetricKey = new RijndaelManaged();
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;//on errors, check this and experiment with padding type
                    //symmetricKey.Padding = PaddingMode.None;
                }

                return symmetricKey;
            }
        }

        public static string Encrypt(string plainText) {

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cypherTextBytes = null;

            using (MemoryStream theMemoryStream = new MemoryStream()) {
                using (CryptoStream cryptoStream = new CryptoStream(theMemoryStream, Encryptor, CryptoStreamMode.Write)) {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                    cryptoStream.FlushFinalBlock();

                    cypherTextBytes = theMemoryStream.ToArray();

                    cryptoStream.Close();
                }
                theMemoryStream.Close();
            }

            string cypherText = Convert.ToBase64String(cypherTextBytes);

            return cypherText;
        }


        public static string Decrypt(string cypherText) {

            byte[] cypherTextBytes = Convert.FromBase64String(cypherText);
            byte[] plainTextBytes = null;
            int decryptedByteCount = 0;

            using (MemoryStream theMemoryStream1 = new MemoryStream(cypherTextBytes)) {
                using (CryptoStream cryptoStream1 = new CryptoStream(theMemoryStream1, Decryptor, CryptoStreamMode.Read)) {
                    plainTextBytes = new byte[cypherTextBytes.Length];
                    decryptedByteCount = cryptoStream1.Read(plainTextBytes, 0, plainTextBytes.Length);
                    //cryptoStream1.Flush();
                    cryptoStream1.Close();


                }
                theMemoryStream1.Close();
            }

            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;
        }



        /*public static void Test()
        {
            string plainText = "input text field from database";

            ApplicationHelper.WriteLineToDebug(string.Format("Key as Base64 : {0}", Convert.ToBase64String(KeyBytes)));
            Debug.WriteLine(Convert.ToBase64String(KeyBytes));

            ApplicationHelper.WriteLineToDebug(string.Format("Plaintext : {0}", plainText));

            //this has to be written back to DB field
            string cypherText = EncryptorDecryptor.Encrypt(plainText);

            ApplicationHelper.WriteLineToDebug(string.Format("cypherText as Base64 : {0}", cypherText));
            Debug.WriteLine(string.Format("cypherText as Base64 : ***{0}***", cypherText));


            plainText = EncryptorDecryptor.Decrypt(cypherText);

            ApplicationHelper.WriteLineToDebug(string.Format("Decrypted : {0}", plainText));
        }*/

    }

}