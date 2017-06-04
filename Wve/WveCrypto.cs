using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Wve
{
   /// <summary>
   /// object with encryption tools
   /// </summary>
    public class WveCrypto
    {
        /// <summary>
        /// a static implementation of the standard SHA-1 hash algorythm
        /// Returns 160 bit hash (20 bytes) 
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static byte[] HashSHA1(byte[] plaintext)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(plaintext);
        }

        /// <summary>
        /// static implementation of standard SHA-1 that returns a 
        /// 40 character Hex representation of the 20 byte hash of
        /// the UnicodeEncoding(=UTF-16) encoding of given string
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string HashSHA1(string plaintext)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hash = HashSHA1(UE.GetBytes(plaintext));
            return GetHexStringFromByteArray(hash);
        }

        //initialization vector, typed at random 10/13/2006
        private byte[] iv = new byte[]{230,34,8,34,127,098,3,45,
										  54,198,217,100,100,74,208,9};
        //encryption key (symmetrical)
        private byte[] key = new byte[]{67,123,218,126,243,255,0,43,
										   49,220,63,64,92,45,74,29,
										   134,7,239,194,56,67,38,65,
										   76,67,233,134,227,52,98,98};
        //encryption method
        private RijndaelManaged rm;

        /// <summary>
        /// no parameters, leaves default key and i.v.
        /// </summary>
        public WveCrypto()
        {
            //leaves key and iv default values
            initialize();
        }

        /// <summary>
        /// with initialization vector and key
        /// </summary>
        /// <param name="initializationVector">16 bytes</param>
        /// <param name="newKey">32 bytes</param>
        public WveCrypto(byte[] initializationVector,
            byte[] newKey)
        {
            iv = initializationVector;
            key = newKey;
            initialize();
        }

        private void initialize()
        {
            rm = new RijndaelManaged();
            rm.IV = iv;
            rm.Key = key;
        }

        //  better to use the HashSHA1() methods becaue Sha1Managed isn't certified


        /// <summary>
        /// converts byte array to hexadecimal string
        /// </summary>
        public static string GetHexStringFromByteArray(byte[] bytes)
        {
            int len = bytes.Length;
            StringBuilder sb = new StringBuilder(len * 2);

            for (int x = 0; x < len; ++x)
                sb.Append(bytes[x].ToString("X2"));

            return sb.ToString();
        }


        /// <summary>
        /// returns the byte array from two character hex numbers
        /// </summary>
        public static byte[] GetByteArrayFromHexString(string hexString)
        {
            int len = hexString.Length / 2;
            byte[] result = new byte[len];
            for (int i = 0; i < len; i++)
            {
                try
                {
                    result[i] = byte.Parse(hexString.Substring(i * 2, 2),
                       System.Globalization.NumberStyles.HexNumber);
                }
                catch (Exception er)
                {
                    throw new Exception("Error reading hexadecimal string at position " +
                       i.ToString() + "\r\nString = " + hexString +
                       "\r\n\r\nDetails: " + er.ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// encrypt bytes, using current key and iv
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] toEncrypt)
        {
            ICryptoTransform encryptor;
            MemoryStream msEncrypt;
            CryptoStream csEncrypt;

            encryptor = rm.CreateEncryptor();
            msEncrypt = new MemoryStream();
            csEncrypt = new CryptoStream(msEncrypt, encryptor,
               CryptoStreamMode.Write);

            //write data to stream and flush
            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            //get encrypted array
            return msEncrypt.ToArray();
        }

        /// <summary>
        /// encrypt a file to another file
        /// </summary>
        /// <param name="outFilePath">plaintext file to encrypt</param>
        /// <param name="plainFilePath">encrypted file location to write to</param>
        /// <param name="bufferSize">size of buffer for chunks read and written</param>
        /// <returns></returns>
        public bool Encrypt(string plainFilePath, string outFilePath, int bufferSize)
        {
            bool result = false;
            using (FileStream fsIn = new FileStream(plainFilePath, FileMode.Open))
            {
                using (FileStream fsOut = new FileStream(outFilePath, FileMode.OpenOrCreate))
                {
                    ICryptoTransform encryptor;
                    encryptor = rm.CreateEncryptor();
                    using (CryptoStream cs = new CryptoStream(fsOut, encryptor,
                       CryptoStreamMode.Write))
                    {
                        using (BinaryWriter bWriter = new BinaryWriter(cs))
                        {
                            using (BinaryReader bReader = new BinaryReader(fsIn))
                            {
                                byte[] buffer;
                                while ((buffer = bReader.ReadBytes(bufferSize)).Length > 0)
                                {
                                    bWriter.Write(buffer, 0, buffer.Length);
                                    bWriter.Flush();
                                }
                            }
                        }
                    }
                }//from using fsOut
            }//from using fsIn
            result = true; //if goth here
            return result;
        }

        /// <summary>
        /// encrypt string to encrypted byte array, 
        /// using current key and iv
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public byte[] Encrypt(string toEncrypt)
        {
            System.Text.ASCIIEncoding textConverter =
                   new System.Text.ASCIIEncoding();
            return Encrypt(textConverter.GetBytes(toEncrypt));
        }

        /// <summary>
        /// decrypt a file to another file
        /// </summary>
        /// <param name="cryptoFilePath">encrypted file to decrypt</param>
        /// <param name="outFilePath">plaintext file to write to</param>
        /// <param name="bufferSize">size of buffer for chunks read and written</param>
        /// <returns></returns>
        public bool Decrypt(string cryptoFilePath, string outFilePath, int bufferSize)
        {
            bool result = false;
            using (FileStream fsIn = new FileStream(cryptoFilePath, FileMode.Open))
            {
                using (FileStream fsOut = new FileStream(outFilePath, FileMode.OpenOrCreate))
                {
                    ICryptoTransform decryptor;
                    decryptor = rm.CreateDecryptor();
                    using (CryptoStream cs = new CryptoStream(fsIn,
                        decryptor,
                       CryptoStreamMode.Read))
                    {
                        using (BinaryWriter bWriter = new BinaryWriter(fsOut))
                        {
                            using (BinaryReader bReader = new BinaryReader(cs))
                            {

                                byte[] buffer;
                                while ((buffer = bReader.ReadBytes(bufferSize)).Length > 0)
                                {
                                    bWriter.Write(buffer, 0, buffer.Length);
                                    bWriter.Flush();
                                }
                            }
                        }
                    }
                }//from using fsOut
            }//from using fsIn
            result = true; //if goth here
            return result;
        }

        /// <summary>
        /// decrypt, using current key and iv
        /// Does not handle error if password is wrong, but passes 
        /// unhandled error to calling routine!
        /// Truncates zeroed bytes at the end of block if length = 0
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="length">Length of byte[] array to return. 
        /// May set length to 0 for strings for automatic resizing,
        /// but not for binary data.</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] toDecrypt, int length)
        {
            ICryptoTransform decryptor;
            MemoryStream msDecrypt;
            CryptoStream csDecrypt;
            byte[] temp; //for resizing array

            decryptor = rm.CreateDecryptor();

            msDecrypt = new MemoryStream(toDecrypt);
            msDecrypt.Position = 0; //start at first
            csDecrypt = new CryptoStream(msDecrypt, decryptor,
               CryptoStreamMode.Read);

            //read the data
            byte[] decrypted = new byte[toDecrypt.Length];
            //if generates error, must be handled by calling routine
            int bytesRead = csDecrypt.Read(decrypted, 0, toDecrypt.Length);

            //now resize the deciphered block of bytes
            if (length == 0)
            {
                //remove the \0 chars appended when buffer flushed
                for (int i = 0; i < decrypted.Length; i++)
                {
                    if (decrypted[i] == 0)
                    {
                        temp = new byte[i];
                        for (int j = 0; j < i; j++)
                        {
                            temp[j] = decrypted[j];
                        }
                        decrypted = temp;
                        break;
                    }
                }
            }
            else
            {
                //remove whatever bytes trail the real data
                temp = new byte[length];
                for (int j = 0; j < length; j++)
                {
                    temp[j] = decrypted[j];
                }
                decrypted = temp;
            }

            return decrypted;
        }

        /// <summary>
        /// returns encrypted bytes in Base64 format
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string EncryptToBase64(byte[] toEncrypt)
        {
            return System.Convert.ToBase64String(Encrypt(toEncrypt));
        }

        /// <summary>
        /// returns encrypted in Base64 format
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string EncryptToBase64(string toEncrypt)
        {
            System.Text.ASCIIEncoding textConverter =
                  new System.Text.ASCIIEncoding();
            return EncryptToBase64(textConverter.GetBytes(toEncrypt));
        }

        //got here
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encodedCiphertext"></param>
        /// <param name="length">length of bytes to return, or may
        /// use 0 for autotrim if is string data</param>
        /// <returns></returns>
        public byte[] DecryptFromBase64(string encodedCiphertext, int length)
        {
            return Decrypt(System.Convert.FromBase64String(encodedCiphertext), length);
            //turn Decrypt(GetByteArrayFromHexString(encryptedHex), length);
        }

        /// <summary>
        /// decrypt from Base64 encoded ciphertext to string
        /// </summary>
        /// <param name="encodedCiphertext"></param>
        /// <returns></returns>
        public string DecryptFromBase64ToString(string encodedCiphertext)
        {
            System.Text.ASCIIEncoding textConverter =
                 new System.Text.ASCIIEncoding();
            //decrypt with length parameter 0 to strip zeroed bytes at end of block
            return textConverter.GetString(DecryptFromBase64(encodedCiphertext, 0));
        }

        /// <summary>
        /// returns encrypted bytes in a hexadecimal string format
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string EncryptToHex(byte[] toEncrypt)
        {
            return GetHexStringFromByteArray(Encrypt(toEncrypt));
        }

        /// <summary>
        /// returns encrypted bytes in a hexadecimal string format
        /// (accepts string  plaintext parameter)
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string EncryptToHex(string toEncrypt)
        {
            System.Text.ASCIIEncoding textConverter =
                   new System.Text.ASCIIEncoding();
            return EncryptToHex(textConverter.GetBytes(toEncrypt));
        }

        /// <summary>
        /// decrypt hexadecmial string of bytes to original plaintext in bytes
        /// Set length to 0 if string to truncate zeroed bytes at end of block
        /// </summary>
        /// <param name="encryptedHex"></param>
        /// <param name="length">lenght of data to return, may use 0 if string</param>
        /// <returns></returns>
        public byte[] DecryptFromHex(string encryptedHex, int length)
        {
            return Decrypt(GetByteArrayFromHexString(encryptedHex), length);
        }

        /// <summary>
        /// decrypt hexadecimal string of bytes to original plaintext as string
        /// </summary>
        /// <param name="encryptedHex"></param>
        /// <returns></returns>
        public string DecryptFromHexToString(string encryptedHex)
        {
            System.Text.ASCIIEncoding textConverter =
                   new System.Text.ASCIIEncoding();
            //decrypt with length parameter 0 to strip zeroed bytes at end of block
            return (textConverter.GetString(
               DecryptFromHex(encryptedHex, 0)));
        }

        /// <summary>
        /// Concatenate username:password and encode it with Base64 encoding
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncodeAuthorizationHeader(string username, string password)
        {
            //concatenate the string
            string namePass = username + ":" + password;
            //encode it in utf-8
            System.Text.UTF8Encoding utf8 = new UTF8Encoding();
            // and encode it in Base64
            return System.Convert.ToBase64String(utf8.GetBytes(namePass));
        }

        /// <summary>
        /// decodes a Base64 string into username and password and returns true
        /// only if it is in the format username:password.
        /// If not, the decoded Base64 string is returned in username, null in password,
        /// and the method returns false
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="headerEncoded"></param>
        public static bool DecodeAuthorizationHeader(string headerEncoded,
            out string username,
            out string password)
        {
            bool result = false; //unless it is in correct format
            username = null;
            password = null;
            //get concatenated string
            System.Text.UTF8Encoding utf8 = new UTF8Encoding();
            string raw = utf8.GetString(
                System.Convert.FromBase64String(headerEncoded));
            //check for username:password format
            if (raw.Contains(":"))
            {
                username = raw.Substring(0, raw.IndexOf(":"));
                if (raw.Length > raw.IndexOf(":") + 1)
                {
                    password = raw.Substring(raw.IndexOf(":") + 1,
                        raw.Length - raw.IndexOf(":") - 1);
                }
                else
                {
                    password = string.Empty;
                }
                result = true;
            }
            else
            {
                username = raw;
                //leave result false
            }
            return result;
        }

        /// <summary>
        /// compare files and return true if same size and bytewise compareson matches.
        /// </summary>
        /// <param name="file1Path"></param>
        /// <param name="file2Path"></param>
        /// <returns></returns>
        public static bool CompareFiles(string file1Path, string file2Path)
        {
            bool matches = false;
            using (FileStream fs1 = new FileStream(file1Path, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fs2 = new FileStream(file2Path, FileMode.Open, FileAccess.Read))
                {
                    if ((fs1.Length == 0) && (fs2.Length == 0))
                    {
                        return true;
                    }
                    else if (fs1.Length != fs2.Length)
                    {
                        return false;
                    }
                    else //same length
                    {
                        int fs1Byte;
                        //while not eof
                        while ((fs1Byte = fs1.ReadByte()) != -1)
                        {
                            if (fs2.ReadByte() != fs1Byte)
                            {
                                return false;
                            }
                        }//from while not end of file
                        //if got here they must match
                        matches = true;
                    }//from if length matches
                }
            }
            return matches;
        }

        //////////////////////////////////////////////////////////
        // Helper methods:
        // CreateRandomSalt: Generates a random salt value of the
        //                   specified length.
        //
        // ClearBytes: Clear the bytes in a buffer so they can't
        //             later be read from memory.
        //////////////////////////////////////////////////////////


        /// <summary>
        /// CreateRandomSalt: Generates a random salt value of the
        ///                   specified length.
        ///                   (from Microsoft Help)
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] CreateRandomSalt(int length)
        {
            // Create a buffer
            byte[] randBytes;

            if (length >= 1)
            {
                randBytes = new byte[length];
            }
            else
            {
                randBytes = new byte[1];
            }

            // Create a new RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes(randBytes);

            // return the bytes.
            return randBytes;
        }

        /// <summary>
        /// make a random string of digits
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandomDigits(int length)
        {
            StringBuilder sbResult = new StringBuilder();
            byte[] bytes = CreateRandomSalt(length);
            for (int i = 0; i < length; i++)
            {
                sbResult.Append((((int)bytes[i]) % 10).ToString());
            }
            return sbResult.ToString();
        }


        /// <summary>
        /// ClearBytes: Clear the bytes in a buffer so they can't
        ///           later be read from memory.
        ///           (from Microsoft help)
        /// </summary>
        /// <param name="buffer"></param>
        public static void ClearBytes(byte[] buffer)
        {
            // Check arguments.
            if (buffer == null)
            {
                throw new ArgumentException("buffer");
            }

            // Set each byte in the buffer to 0.
            for (int x = 0; x < buffer.Length; x++)
            {
                buffer[x] = 0;
            }
        }
    }//wvecrypto
}//namespace
