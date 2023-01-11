using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SignApp.Shared
{
    public static class SignMaster
    {
        private static int KeySizes = 2048;

        public static byte[] stringToByte(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string byteToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static string[]? getKeyPair()
        {
            var KeyPair = RSA.Create(KeySizes);
            var privateKey = KeyPair.ExportRSAPrivateKey();
            var publicKey = KeyPair.ExportRSAPublicKey();
            if (privateKey != null && publicKey != null)
                if (verifyData(privateKey, signData(privateKey, privateKey), publicKey))
                    return new string[] { byteToString(privateKey), byteToString(publicKey) };
            return null;
        }

        public static byte[] signData(byte[] data, byte[] privatekey)
        {
            var KeyPair = RSA.Create(KeySizes);
            KeyPair.ImportRSAPrivateKey(privatekey, out int bytesRead);
            return KeyPair.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public static bool verifyData(byte[] data, byte[] signature, byte[] publicKey)
        {
            var KeyPair = RSA.Create(KeySizes);
            KeyPair.ImportRSAPublicKey(publicKey, out int bytesRead);
            return KeyPair.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}