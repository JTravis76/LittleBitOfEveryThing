using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationGallery
{
    public enum Strength: int
    {
        Good = 128,
        Better = 192,
        Best = 256
    }


    public class AES
    {
        private string _salt = "Qcxi8430#20d9n!!BXz";


        //public void EncryptDecryptMe(string text)
        //{
        //    try
        //    {
        //        //Test/gets decryption on password
        //        text = Decrypt(text.Trim(), (int)Strength.Good);
        //    }
        //    catch
        //    {
        //        //encrypted basic password then write to file
        //        text = Encrypt(text.Trim(), (int)Strength.Good);

        //        //use basic password
        //        //text = text;
        //    }
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Password"></param>
        /// <param name="Bits"></param>
        /// <returns></returns>
        public string Encrypt(string Data, Strength strength = Strength.Good)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(Data);
            byte[] byteArray = StrToByteArray("0x00,0x01,0x02,0x1C,0x1D,0x1E,0x03,0x04,0x05,0x0F,0x20,0x21,0xAD,0xAF,0xA4");
            System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(_salt, byteArray);


            byte[] encrytedData;
            switch (strength)
            {
                case Strength.Good:
                    encrytedData = Encrypt(clearBytes, pdb.GetBytes(16), pdb.GetBytes(16));
                    break;
                    
                case Strength.Better:
                    encrytedData = Encrypt(clearBytes, pdb.GetBytes(24), pdb.GetBytes(16));
                    break;

                case Strength.Best:
                    encrytedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                    break;

                default:
                    return String.Concat(128);
            }
            return Convert.ToBase64String(encrytedData);
        }



        /// <summary>
        /// Encrypt a byte array into a byte array using a key and an IV
        /// </summary>
        /// <param name="clearData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            using(System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.Security.Cryptography.Rijndael alg = System.Security.Cryptography.Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;

                using(System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, alg.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                {
                    cs.Write(clearData, 0, clearData.Length);
                    cs.Close();

                    byte[] encryptedData = ms.ToArray();
                    return encryptedData;
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Password"></param>
        /// <param name="Bits"></param>
        /// <returns></returns>
        public string Decrypt(string Data, Strength strength = Strength.Good)
        {
            byte[] clearBytes = Convert.FromBase64String(Data);
            byte[] byteArray = StrToByteArray("0x00,0x01,0x02,0x1C,0x1D,0x1E,0x03,0x04,0x05,0x0F,0x20,0x21,0xAD,0xAF,0xA4");
            System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(_salt, byteArray);


            byte[] decrytedData;
            switch (strength)
            {
                case Strength.Good:
                    decrytedData = Decrypt(clearBytes, pdb.GetBytes(16), pdb.GetBytes(16));
                    break;
                case Strength.Better:
                    decrytedData = Decrypt(clearBytes, pdb.GetBytes(24), pdb.GetBytes(16));
                    break;
                case Strength.Best:
                    decrytedData = Decrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                    break;
                default:
                    return String.Concat(128);
            }
            return System.Text.Encoding.Unicode.GetString(decrytedData);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.Security.Cryptography.Rijndael alg = System.Security.Cryptography.Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;

                using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, alg.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                {
                    cs.Write(cipherData, 0, cipherData.Length);
                    cs.Close();

                    byte[] decryptedData = ms.ToArray();
                    return decryptedData;
                }
            }
        }



        /// <summary>
        /// convert a string to a byte array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }


    }
}