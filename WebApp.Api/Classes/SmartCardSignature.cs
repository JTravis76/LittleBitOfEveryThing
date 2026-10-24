using iText.Signatures;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Api.Classes
{
    public class SmartCardSignature : IExternalSignature
    {
        /// <summary>The hash algorithm.</summary>
        private readonly string hashAlgorithm;
        /// <summary>The encryption algorithm (obtained from the private key)</summary>
        private readonly string encryptionAlgorithm;

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

            var csp = new CspParameters(1, "Microsoft Base Smart Card Crypto Provider")
            {
                Flags = CspProviderFlags.UseDefaultKeyContainer
            };

            var provider = new RSACryptoServiceProvider(csp);
            var formatter = new RSAPKCS1SignatureFormatter(provider);
            formatter.SetHashAlgorithm(this.hashAlgorithm);//"SHA1"
            byte[] signature = formatter.CreateSignature(hash);
            return signature;
        }
    }
}
