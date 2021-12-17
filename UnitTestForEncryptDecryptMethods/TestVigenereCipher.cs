using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using cw_wpf;
using System.IO;

namespace UnitTestForEncryptDecryptMethods
{
    [TestClass]
    public class TestVigenereCipher
    {
        [TestMethod]
        public void TestCourseMessageDecryptROT0()
        {
            VigenereCipher vigenereCipher = new VigenereCipher("абвгдеёжзийклмнопрстуфхцчшщъыьэюя", CipherMode.ROT0);

            string pathResult = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toEncrypt1.txt";
            string pathRequest = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toDecrypt1.txt";

            string result = File.ReadAllText(pathResult);
            string request = File.ReadAllText(pathRequest);

            Assert.AreEqual(vigenereCipher.Decrypt(request, "скорпион"), result);
        }


        [TestMethod]
        public void TestCourseMessageEncryptROT0()
        {
            VigenereCipher vigenereCipher = new VigenereCipher("абвгдеёжзийклмнопрстуфхцчшщъыьэюя", CipherMode.ROT0);

            string pathResult = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toDecrypt1.txt";
            string pathRequest = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toEncrypt1.txt";

            string result = File.ReadAllText(pathResult);
            string request = File.ReadAllText(pathRequest);

            Assert.AreEqual(vigenereCipher.Encrypt(request, "скорпион"), result);
        }

        [TestMethod]
        public void TestCourseMessageDecryptROT1()
        {
            VigenereCipher vigenereCipher = new VigenereCipher("абвгдеёжзийклмнопрстуфхцчшщъыьэюя", CipherMode.ROT1);

            string pathResult = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toEncrypt2.txt";
            string pathRequest = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toDecrypt2.txt";

            string result = File.ReadAllText(pathResult);
            string request = File.ReadAllText(pathRequest);

            Assert.AreEqual(vigenereCipher.Decrypt(request, "скорпион"), result);
        }

        [TestMethod]
        public void TestCourseMessageEncryptROT1()
        {
            VigenereCipher vigenereCipher = new VigenereCipher("абвгдеёжзийклмнопрстуфхцчшщъыьэюя", CipherMode.ROT1);
            
            string pathResult = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toDecrypt2.txt";
            string pathRequest = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toEncrypt2.txt";

            string result = File.ReadAllText(pathResult);
            string request = File.ReadAllText(pathRequest);

            Assert.AreEqual(vigenereCipher.Encrypt(request, "скорпион"), result);
        }


        [TestMethod]
        public void TestDecryptLongKey()
        {
            VigenereCipher vigenereCipher = new VigenereCipher("абвгдеёжзийклмнопрстуфхцчшщъыьэюя", CipherMode.ROT0);

            string pathResult = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toEncrypt3.txt";
            string pathRequest = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toDecrypt3.txt";

            string result = File.ReadAllText(pathResult);
            string request = File.ReadAllText(pathRequest);

            Assert.AreEqual(vigenereCipher.Decrypt(request, "абвгдеёжзийклмнопрстуфхцчшщъыьэюяааа"), result);
        }

        [TestMethod]
        public void TestEncryptLongKey()
        {
            VigenereCipher vigenereCipher = new VigenereCipher("абвгдеёжзийклмнопрстуфхцчшщъыьэюя", CipherMode.ROT0);

            string pathResult = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toDecrypt3.txt";
            string pathRequest = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toEncrypt3.txt";

            string result = File.ReadAllText(pathResult);
            string request = File.ReadAllText(pathRequest);

            Assert.AreEqual(vigenereCipher.Encrypt(request, "абвгдеёжзийклмнопрстуфхцчшщъыьэюяааа"), result);
        }


        [TestMethod]
        public void TestEngAlphabetMixedMessageEncrypt()
        {
            VigenereCipher vigenereCipher = new VigenereCipher("abcdefghijklmnopqrstuvwxyz", CipherMode.ROT0);

            string pathResult = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toDecrypt4.txt";
            string pathRequest = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toEncrypt4.txt";

            string result = File.ReadAllText(pathResult);
            string request = File.ReadAllText(pathRequest);

            Assert.AreEqual(vigenereCipher.Encrypt(request, "zaz"), result);
        }


        [TestMethod]
        public void TestEngAlphabetMixedMessageDecrypt()
        {
            VigenereCipher vigenereCipher = new VigenereCipher("abcdefghijklmnopqrstuvwxyz", CipherMode.ROT0);

            string pathResult = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toEncrypt4.txt";
            string pathRequest = @"..\..\..\UnitTestForEncryptDecryptMethods\tests\toDecrypt4.txt";

            string result = File.ReadAllText(pathResult);
            string request = File.ReadAllText(pathRequest);

            Assert.AreEqual(vigenereCipher.Decrypt(request, "zaz"), result);
        }


    }
}
