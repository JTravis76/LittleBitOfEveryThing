using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Signatures;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.PDF;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;

//using NativeLibraryManager;
//using System.Runtime.InteropServices;
//using System.Reflection;

namespace WebApp.Certificate1
{
    class Program
    {
        //https://viewbag.wordpress.com/2019/12/24/pdf-digital-signatures-itext7-bouncy-castle-net-core/
        //https://sysadmins.lv/retired-msft-blogs/alejacma/how-to-sign-and-verify-the-signature-with-net-and-a-certificate-csharp.aspx
        //https://www.pkisolutions.com/accessing-and-using-certificate-private-keys-in-net-framework-net-core/
        //https://paulstovell.com/x509certificate2/
        //https://docs.microsoft.com/en-us/answers/questions/18421/cmssignercomputesignature-throws-windowscryptograp.html
        //https://www.codeproject.com/Articles/240655/Using-a-Smart-Card-Certificate-with-NET-Security-i
        //https://www.adobe.com/devnet-docs/acrobatetk/tools/DigSigDC/appearances.html

        public static string _homeDir = Environment.CurrentDirectory.Replace(@"\bin\Debug\netcoreapp3.1", @"\Resources");

        //[DllImport("acpkcs211")]
        //private static extern void ActivId();

        private const string _providerName = "Microsoft Base Smart Card Crypto Provider";

        /// <summary>
        /// https://stackoverflow.com/questions/22236116/find-certificate-on-smartcard-currently-on-reader
        /// </summary>
        private static class NativeMethods
        {
            public const uint PROV_RSA_FULL = 0x00000001;
            public const uint CRYPT_VERIFYCONTEXT = 0xF0000000;
            public const uint CRYPT_FIRST = 0x00000001;
            public const uint CRYPT_NEXT = 0x00000002;
            public const uint ERROR_NO_MORE_ITEMS = 0x00000103;
            public const uint PP_ENUMCONTAINERS = 0x00000002;

            [DllImport("advapi32.dll", BestFitMapping = false, ThrowOnUnmappableChar = true, SetLastError = true)]
            public static extern bool CryptAcquireContext(
            ref IntPtr phProv,
            [MarshalAs(UnmanagedType.LPStr)] string pszContainer,
            [MarshalAs(UnmanagedType.LPStr)] string pszProvider,
            uint dwProvType,
            uint dwFlags);

            [DllImport("advapi32.dll", BestFitMapping = false, ThrowOnUnmappableChar = true, SetLastError = true)]
            public static extern bool CryptGetProvParam(
            IntPtr hProv,
            uint dwParam,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder pbData,
            ref uint pdwDataLen,
            uint dwFlags);

            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern bool CryptReleaseContext(
            IntPtr hProv,
            uint dwFlags);
        }

        /// <summary>
        /// https://stackoverflow.com/questions/22236116/find-certificate-on-smartcard-currently-on-reader
        /// </summary>
        private static List<string> GetKeyContainers()
        {
            List<string> containers = new List<string>();

            IntPtr hProv = IntPtr.Zero;

            try
            {
                if (!NativeMethods.CryptAcquireContext(ref hProv, null, _providerName, NativeMethods.PROV_RSA_FULL, NativeMethods.CRYPT_VERIFYCONTEXT))
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                uint pcbData = 0;
                uint dwFlags = NativeMethods.CRYPT_FIRST;
                if (!NativeMethods.CryptGetProvParam(hProv, NativeMethods.PP_ENUMCONTAINERS, null, ref pcbData, dwFlags))
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                StringBuilder sb = new StringBuilder((int)pcbData + 1);
                while (NativeMethods.CryptGetProvParam(hProv, NativeMethods.PP_ENUMCONTAINERS, sb, ref pcbData, dwFlags))
                {
                    containers.Add(sb.ToString());
                    dwFlags = NativeMethods.CRYPT_NEXT;
                }

                int err = Marshal.GetLastWin32Error();
                if (err != NativeMethods.ERROR_NO_MORE_ITEMS)
                    throw new Win32Exception(err);

                if (hProv != IntPtr.Zero)
                {
                    if (!NativeMethods.CryptReleaseContext(hProv, 0))
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    hProv = IntPtr.Zero;
                }
            }
            catch
            {
                if (hProv != IntPtr.Zero)
                {
                    if (!NativeMethods.CryptReleaseContext(hProv, 0))
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    hProv = IntPtr.Zero;
                }

                throw;
            }

            return containers;
        }

        static void Main(string[] args)
        {
            //var accessor = new ResourceAccessor(Assembly.GetExecutingAssembly());
            //var libManager = new LibraryManager(new LibraryItem(Platform.Windows, Bitness.x64, new LibraryFile("acpkcs211.dll", accessor.Binary("acpkcs211.dll"))));

            //libManager.LoadNativeLibrary();
            //ActivId();
            //====================================

            //var certs = GetKeyContainers();


            //====================================

            //FetchCertFromSmartCard();
            //FetchCertificateFromStore();
            //SignPdfByPhysicalCertificate();
            //SignData();

            //====================================
            //Hash the data
            //SHA1Managed sha1 = new SHA1Managed();

            //UnicodeEncoding encoding = new UnicodeEncoding();
            //byte[] data = encoding.GetBytes("P@ssw0rd");
            //byte[] hash = sha1.ComputeHash(data);

            // Sign the hash
            //byte[] z = rsa.SignHash(hash, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
            //byte[] c =  csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));

            //====================================
            Console.WriteLine("Completed...");
            //Console.ReadKey();
        }

        public static void FetchCertificateFromStore()
        {
            const string _RSA = "1.2.840.113549.1.1.1";
            const string _DSA = "1.2.840.10040.4.1";
            const string _ECC = "1.2.840.10045.2.1";

            // Access Personal (MY) certificate store of current user
            // Smart card cert is automatically added to the Windows Cert Store upon use.
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);

            // Find the certificate we'll use to sign
            RSACryptoServiceProvider csp = null;
            System.Security.Cryptography.RSA rsa = null;

            foreach (X509Certificate2 cert in my.Certificates.Find(X509FindType.FindByApplicationPolicy, "Smart Card Logon", true))
            {
                //Console.WriteLine(cert.Subject);
                //if (cert.Subject.Contains("fbplda51(jt)"))
                //if (cert.Subject.Contains("CN=KDJ"))
                if (cert.Subject.Contains("CN=JEREMY R. TRAVIS"))
                {
                    switch (cert.PublicKey.Oid.Value)
                    {
                        case _RSA:
                            rsa = cert.GetRSAPrivateKey();
                            //ICipherParameters pk = TransformRSAPrivateKey(rsa);
                            //ICipherParameters pk = Org.BouncyCastle.Security.DotNetUtilities.GetRsaKeyPair(rsa).Private;
                            //System.Diagnostics.Debug.WriteLine("");
                            break;
                        case _DSA:
                            //var dsa = cert.GetDSAPrivateKey();
                            break;
                        case _ECC:
                            //var ecc = cert.GetECDsaPrivateKey();
                            break;
                        default:
                            csp = (RSACryptoServiceProvider)cert.PrivateKey;
                            break;
                    }

                    //File.WriteAllBytes($"{_homeDir}\\Jeremy-Travis.pfx", cert.Export(X509ContentType.Pkcs12, "PWD"));

                    SignPdf(cert);

                    System.Diagnostics.Debug.WriteLine("");
                    break;
                }
            }
        }

        public static void FetchCertFromSmartCard()
        {
            //== https://www.pkcs11interop.net/
            string _ckaLabel = null; // equal to Friendly Name or Subject ??
            byte[] _ckaId = null; // equal to ???

            string path = @"C:\Program Files (x86)\HID Global\ActivClient\acpkcs211.dll";
            var appType = Net.Pkcs11Interop.Common.AppType.SingleThreaded;
            var pkcs11Interop = new Pkcs11InteropFactories();
            using (IPkcs11Library pkcs11Library = pkcs11Interop.Pkcs11LibraryFactory.LoadPkcs11Library(new Pkcs11InteropFactories(), path, appType))
            {
                // Do something interesting
                // Get list of available slots
                List<ISlot> slots = pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent);
                foreach (ISlot slot in slots)
                {
                    using (Net.Pkcs11Interop.HighLevelAPI.ISession session = slot.OpenSession(SessionType.ReadWrite))
                    {
                        List<IObjectAttribute> searchTemplate = new List<IObjectAttribute>();
                        searchTemplate.Add(new Net.Pkcs11Interop.HighLevelAPI41.ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE));
                        searchTemplate.Add(new Net.Pkcs11Interop.HighLevelAPI41.ObjectAttribute(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509));
                        if (!string.IsNullOrEmpty(_ckaLabel))
                            searchTemplate.Add(new Net.Pkcs11Interop.HighLevelAPI41.ObjectAttribute(CKA.CKA_LABEL, _ckaLabel));
                        if (_ckaId != null)
                            searchTemplate.Add(new Net.Pkcs11Interop.HighLevelAPI41.ObjectAttribute(CKA.CKA_ID, _ckaId));

                        List<IObjectHandle> foundObjects = session.FindAllObjects(searchTemplate);
                        if (foundObjects.Count < 1)
                            throw new Exception(string.Format("Certificate with label \"{0}\" and id \"{1}\" was not found", _ckaLabel, (_ckaId == null) ? null : ConvertUtils.BytesToHexString(_ckaId)));
                        else if (foundObjects.Count > 1)
                            throw new Exception(string.Format("More than one certificate with label \"{0}\" and id \"{1}\" was found", _ckaLabel, (_ckaId == null) ? null : ConvertUtils.BytesToHexString(_ckaId)));

                        //== This code works
                        if (foundObjects.Count == 1)
                        {
                            List<CKA> attributes = new List<CKA>();
                            attributes.Add(CKA.CKA_ID);
                            attributes.Add(CKA.CKA_LABEL);
                            attributes.Add(CKA.CKA_VALUE);

                            List<IObjectAttribute> certificateAttributes = session.GetAttributeValue(foundObjects[0], attributes);

                            //byte[] signingCertificate = certificateAttributes[0].GetValueAsByteArray();
                            //SignPdf(signingCertificate, "TODO");

                            string ckaId = ConvertUtils.BytesToHexString(certificateAttributes[0].GetValueAsByteArray());
                            string ckaLabel = certificateAttributes[1].GetValueAsString();
                            byte[] ckaValue = certificateAttributes[2].GetValueAsByteArray();

                            Certificate certificate = new Certificate(ckaId, ckaLabel, ckaValue);
                            X509Certificate2 x509Cert = CertUtils.ToDotNetObject(certificate.Data);

                            SignPdf(x509Cert);

                            System.Diagnostics.Debug.WriteLine("");
                        }
                    }
                }

                //var info = pkcs11Library.GetInfo();
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        public static AsymmetricKeyParameter TransformRSAPrivateKey(AsymmetricAlgorithm privateKey)
        {
            /*= When using smart card cert from store
             * 'Key not valid for use in specified state.'
             */
            RSA rsa = (RSA)privateKey;
            (privateKey as System.Security.Cryptography.RSACng).Key.SetProperty(
                new CngProperty(
                    "Export Policy",
                    BitConverter.GetBytes((int)CngExportPolicies.AllowPlaintextExport),
                    CngPropertyOptions.Persist));

            RSAParameters parameters = rsa.ExportParameters(true);

            //RSACryptoServiceProvider prov = (RSACryptoServiceProvider)privateKey;
            //RSAParameters parameters = prov.ExportParameters(true);

            return new RsaPrivateCrtKeyParameters(
                new BigInteger(1, parameters.Modulus),
                new BigInteger(1, parameters.Exponent),
                new BigInteger(1, parameters.D),
                new BigInteger(1, parameters.P),
                new BigInteger(1, parameters.Q),
                new BigInteger(1, parameters.DP),
                new BigInteger(1, parameters.DQ),
                new BigInteger(1, parameters.InverseQ));
        }

        public static void SignPdf(X509Certificate2 cert)
        {
            using (PdfReader reader = new PdfReader($"{_homeDir}\\Sample-Signatures.pdf"))
            {
                iText.IO.Image.ImageData img = iText.IO.Image.ImageDataFactory.CreatePng(new Uri($"{_homeDir}\\logo.png"));

                using (FileStream fs = new FileStream("C:\\Deploy\\signedFromStore.pdf", FileMode.Create))
                {
                    PdfSigner signer = new PdfSigner(reader, fs, new StampingProperties());

                    PdfSignatureAppearance sap = signer.GetSignatureAppearance();
                    sap.SetReason("general approval").SetLocation("Piketon, OH");

                    //== Watermark logo (behind the signature)
                    //sap.SetImage(img);
                    //sap.SetImageScale(0.3f);//<- equivalent to percentage 0.3f = 3%
                    //sap.SetImageScale(0.8f);

                    // NOTE: when using graphic/name + description, the signature box is split in middle where logo is on left and text is on right.
                    sap.SetRenderingMode(PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION);
                    //NOTE: do not use with SetImage()
                    // Left section of signature
                    sap.SetSignatureGraphic(img);

                    string name = cert?.SubjectName.Name.Replace("CN=", "").Split(",")[0] ?? "";
                    sap.SetLayer2Text($"Digitally signed by {name}\nDate: {signer.GetSignDate()}");
                    //sap.SetLayer2FontSize(12f); //zero is auto-size

                    signer.SetFieldName("Signature23");


                    //Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters rsaPrivate = new Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters(null);

                    /*==ERR: Invalid type specified.
                     * Thinking the library is attempting to export the private key, which is non-exportable
                     * WORKED!! when using a general certificate, which has a exportable private key
                     */
                    //ICipherParameters pk = Org.BouncyCastle.Security.DotNetUtilities.GetRsaKeyPair(rsa).Private;
                    //ICipherParameters pk = Org.BouncyCastle.Security.DotNetUtilities.GetKeyPair(cert.PrivateKey).Private;

                    // Get Certificate chain
                    var cp = new Org.BouncyCastle.X509.X509CertificateParser();
                    var chain = new Org.BouncyCastle.X509.X509Certificate[] {
                        cp.ReadCertificate(cert.RawData)
                    };


                    //IExternalSignature pks = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);
                    //== Use the SmartCard with pin to sign
                    IExternalSignature pks = new SmartCardSignature();

                    signer.SignDetached(pks, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                }
            }
        }

        public static void SignPdf(byte[] certData, string name = "")
        {
            using (PdfReader reader = new PdfReader($"{_homeDir}\\Sample-Signatures.pdf"))
            {
                iText.IO.Image.ImageData img = iText.IO.Image.ImageDataFactory.CreatePng(new Uri($"{_homeDir}\\CompanyLogo.png"));

                using (FileStream fs = new FileStream("C:\\Deploy\\signedFromSmartCard.pdf", FileMode.Create))
                {
                    PdfSigner signer = new PdfSigner(reader, fs, new StampingProperties());

                    PdfSignatureAppearance sap = signer.GetSignatureAppearance();
                    sap.SetReason("general approval").SetLocation("Piketon, OH");

                    // NOTE: when using graphic/name + description, the signature box is split in middle where logo is on left and text is on right.
                    sap.SetRenderingMode(PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION);
                    //NOTE: do not use with SetImage()
                    // Left section of signature
                    sap.SetSignatureGraphic(img);

                    sap.SetLayer2Text($"Digitally signed by {name}\nDate: {signer.GetSignDate()}");
                    //sap.SetLayer2FontSize(12f); //zero is auto-size

                    signer.SetFieldName("Signature23");
                    //signer.SetFieldName("Signature25");

                    // Get Certificate chain
                    var cp = new Org.BouncyCastle.X509.X509CertificateParser();
                    var chain = new Org.BouncyCastle.X509.X509Certificate[] {
                        cp.ReadCertificate(certData)
                    };

                    //== Use the SmartCard with pin to sign
                    IExternalSignature pks = new SmartCardSignature();

                    signer.SignDetached(pks, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                }
            }
        }

        public static void SignPdfByPhysicalCertificate()
        {
            //This is a cert with a private key and known pwd
            string KEYSTORE = "C:\\deploy\\GeneralUser.pfx";
            char[] PASSWORD = "password1234".ToCharArray();

            using (PdfReader reader = new PdfReader($"{_homeDir}\\Sample-Signatures.pdf"))
            {
                Pkcs12Store pk12 = new Pkcs12Store(new FileStream(KEYSTORE, FileMode.Open, FileAccess.Read), PASSWORD);
                string alias = null;
                foreach (object a in pk12.Aliases)
                {
                    alias = ((string)a);
                    if (pk12.IsKeyEntry(alias))
                    {
                        break;
                    }
                }
                ICipherParameters pk = pk12.GetKey(alias).Key;

                X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[ce.Length];
                for (int k = 0; k < ce.Length; ++k)
                {
                    chain[k] = ce[k].Certificate;
                }
                string issuerDN = (string)chain[0].IssuerDN.GetValueList()[0];

                using (FileStream fs = new FileStream("C:\\Deploy\\signedFromPfx.pdf", FileMode.Create))
                {
                    PdfSigner signer = new PdfSigner(reader, fs, new StampingProperties());

                    PdfSignatureAppearance sap = signer.GetSignatureAppearance();
                    sap.SetReason("general approval").SetLocation("Piketon, OH");//.SetPageRect(new Rectangle(36, 648, 200, 100))
                    sap.SetRenderingMode(PdfSignatureAppearance.RenderingMode.NAME_AND_DESCRIPTION);
                    sap.SetLayer2Text($"Digitally signed by {issuerDN}\nDate: {signer.GetSignDate()}");

                    signer.SetFieldName("Signature23");

                    IExternalSignature pks = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);
                    signer.SignDetached(pks, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                }
            }
        }

        public static void SignData()
        {
            const string xmlPubKey =
                @"<RSAKeyValue><Modulus>5Z/yjVC/EbAINKooDyZ8/I3vmw1v+0/z2eZEmKK11AChJ2vLxpRtNOtTxXDa+HtyWo3Ywtz78u4fqjgvrkL4+YUqvJKX7/2TIc9OHUUllTn4o8rBhPWz7GIFn2YbW+CJzf2OrreyMTfDWAYozpt7ghNbpoOrPPl6J8DxZfph3fzuJfTSDBe6q/z7sKL1ldYfHWrOG9w2Z4SDWX20fbTV4o8nrIZlvHiLQ7i/ZXKa7wiD86UKxnfiYjHDd39hUpzsrSWICr2vSuku8pbYt+wi3NGofrlYjE7wtKiSoFAfMWWlj0BlKCOhboY/uAUq+sYEkgKXEvxw3HVD7sw/MQQsxw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            try
            {
                //must enable "Allow unsafe code" option within properties
                unsafe
                {
                    // Define the string value to assign to a new secure string.
                    // Also, is the smart card pin
                    //char[] pin = { '0', '6', '2', '6', '1', '9', '7', '0' };

                    //System.Security.SecureString pwd;
                    //// Instantiate a new secure string.
                    //fixed (char* pChars = pin)
                    //{
                    //    pwd = new SecureString(pChars, pin.Length);
                    //}

                    CspParameters csp = new CspParameters(1, "Microsoft Base Smart Card Crypto Provider") {
                        Flags = CspProviderFlags.UseDefaultKeyContainer,
                        //KeyPassword = pwd
                    };

                    //byte[] msg =  SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("FooBar"));

                    byte[] msg = new byte[32];
                    Random rnd = new Random((int)DateTime.Now.Ticks);
                    rnd.NextBytes(msg);

                    Console.WriteLine("Data to sign : " + BitConverter.ToString(msg));


                    /*== When setting algorithm, the byte array must match the container size , anything different will throw a "Bad Key" error.
                     * 'SHA1' = [20]
                     * 'SHA256' = [32]
                     */
                    string algorithm = "SHA256";

                    RSACryptoServiceProvider rsaCsp = new RSACryptoServiceProvider(csp);
                    RSAPKCS1SignatureFormatter rsaSign = new RSAPKCS1SignatureFormatter(rsaCsp);
                    rsaSign.SetHashAlgorithm(algorithm);
                    byte[] signature = rsaSign.CreateSignature(msg);

                    Console.WriteLine();
                    Console.WriteLine("Signature: " + BitConverter.ToString(signature));

                    //fetch public key to use to verify
                    //xmlPubKey = rsaCsp.ToXmlString(false);

                    //==---------------------------------------------

                    RSACryptoServiceProvider rsaCsp2 = new RSACryptoServiceProvider();
                    rsaCsp2.FromXmlString(xmlPubKey);

                    RSAPKCS1SignatureDeformatter rsaVerify = new RSAPKCS1SignatureDeformatter(rsaCsp2);
                    rsaVerify.SetHashAlgorithm(algorithm);
                    bool verified = rsaVerify.VerifySignature(msg, signature);

                    Console.WriteLine();
                    Console.WriteLine("Signature verified [{0}]", verified);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Crypto error: " + ex.Message);
            }
        }

    }
}
