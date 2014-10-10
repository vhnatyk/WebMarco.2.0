using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using WebMarco.Utilities.Encryption;

namespace WebMarco.Utilities.Test {
    [TestFixture]
    public class EncryptorDecryptorTest {
        [Test]
        public void TestEncryptionOfString() {

            string testSample = "this text is decrypted ok";
            string result = EncryptorDecryptor.Encrypt(testSample);
            result = EncryptorDecryptor.Decrypt(result);
            Assert.True(result.Equals(testSample), "encryption/decryption failed");
        }
    }
}

