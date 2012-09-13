using System;
using System.Security.Cryptography;
using System.Text;

namespace Aqueduct.Utils
{
    /// <summary>
    /// Hash a plain string with given salt and compare the hashed strings with plain text.
    /// </summary>
    public class HashService
    {
        private readonly HashAlgorithm hashAlgorithm;
        /// <summary>
        /// Default constructor that creates defaul hash algorithm to use
        /// </summary>
        public HashService()
        {
            hashAlgorithm = new SHA512Managed();
        }

        /// <summary>
        /// Constractor to pass a hash algorithm to using in hashing process
        /// </summary>
        /// <param name="hashAlgorithm"></param>
        public HashService(HashAlgorithm hashAlgorithm)
        {
            this.hashAlgorithm = hashAlgorithm;
        }
       
        /// <summary>
        /// hashes the given string with given salt to base 64 string
        /// </summary>
        /// <param name="plainText">plain text to be hashed</param>
        /// <param name="salt">Salt that will be used in hashing</param>
        /// <returns>hashed base 64 string</returns>
        public string ComputeHash(string plainText,string salt)
        {
            byte[] hashBytes = GetHashBytes(plainText, salt);
            string hashValue = Convert.ToBase64String(hashBytes);
            return hashValue;
        }

        /// <summary>
        /// hashes the given string with given salt to base 64 string
        /// </summary>
        /// <param name="plainText">plain text to be hashed</param>
        /// <param name="salt">Salt that will be used in hashing</param>
        /// <returns>hashed base 64 string</returns>
        public string ComputeBase64Hash(string plainText, string salt)
        {
            return ComputeHash(plainText, salt);
        }

        /// <summary>
        /// hashes the given string with given salt to hexadecimal string
        /// </summary>
        /// <param name="plainText">plain text to be hashed</param>
        /// <param name="salt">Salt that will be used in hashing</param>
        /// <returns>hashed hexadecimal string</returns>
        public string ComputeHexadecimalStringHash(string plainText, string salt)
        {
            byte[] hashBytes = GetHashBytes(plainText, salt);
            string hashValue = BitConverter.ToString(hashBytes);
            return hashValue;
        }

        private byte[] GetHashBytes(string plainText, string salt)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            return hashAlgorithm.ComputeHash(plainTextWithSaltBytes);
        }

        /// <summary>
        /// Verifies if the hashValue is hashed form of plainText with given salt 
        /// </summary>
        /// <param name="plainText">Plain text to be verified</param>
        /// <param name="hashValue">Hash value to be verified</param>
        /// <param name="salt">Salt that will be used in hashing</param>
        /// <returns></returns>
        public bool VerifyHash(string plainText, string hashValue, string salt)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);
            int hashSizeInBits = hashAlgorithm.HashSize;
            int hashSizeInBytes = hashSizeInBits/8;
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;
            string expectedHashString = ComputeHash(plainText, salt);    
            return hashValue == expectedHashString;
        }
    }
}
