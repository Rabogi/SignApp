using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SignApp.Shared
{

    public abstract class HashMaster
    {
        public static string Hash256Str(string input)
        {
            var hasher = SHA256.Create();
            Encoding enc = Encoding.UTF8;
            byte[] output = hasher.ComputeHash(enc.GetBytes(input));
            var hash = new StringBuilder();
            foreach (byte b in output)
            {
                hash.Append(b.ToString("x2"));
            }
            return hash.ToString();
        }
        
        public static string Hash256File(byte[] input)
        {   
            var hasher = SHA256.Create();
            byte[] output = hasher.ComputeHash(input);
            var hash = new StringBuilder();
            foreach (byte b in output)
            {
                hash.Append(b.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}