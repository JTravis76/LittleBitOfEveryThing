using iText.Signatures;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Certificate1
{
    public class SmartCardSignature : IExternalSignature
    {
        /// <summary>The hash algorithm.</summary>
        private string hashAlgorithm;
        /// <summary>The encryption algorithm (obtained from the private key)</summary>
        private string encryptionAlgorithm;

        public SmartCardSignature()
        {
            this.hashAlgorithm = "SHA256";
            this.encryptionAlgorithm = "RSA";
        }

        public string GetEncryptionAlgorithm()
        {
            return this.encryptionAlgorithm;
        }

        public string GetHashAlgorithm()
        {
            return this.hashAlgorithm;
        }

        public byte[] Sign(byte[] message)
        {
            byte[] hash;
            using (SHA256 sha256 = SHA256.Create())
            {
                hash = sha256.ComputeHash(message);
            }

            CspParameters csp = new CspParameters(1, "Microsoft Base Smart Card Crypto Provider")
            {
                Flags = CspProviderFlags.UseDefaultKeyContainer
            };

            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(csp);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(provider);
            formatter.SetHashAlgorithm(this.hashAlgorithm);
            return formatter.CreateSignature(hash);
        }
    }
}
