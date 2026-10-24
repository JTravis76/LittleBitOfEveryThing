using iText.Kernel.Pdf;
using iText.Signatures;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace WebApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CertificateController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            X509Store my = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            my.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection collection = my.Certificates;//.Find(X509FindType.FindByApplicationPolicy, "Smart Card Logon", true);//KeyEncipherment

            List<string> certs = new List<string>();
            if (collection.Count > 0)
            {
                foreach (X509Certificate2 cert in collection)
                {
                    //Use friendly name first with fallback to CN from subject
                    if (!string.IsNullOrEmpty(cert.FriendlyName))
                    {
                        certs.Add(cert.FriendlyName);
                    }
                    else
                    {
                        string name = cert.SubjectName.Name.Replace("CN=", "").Split(",")[0];
                        certs.Add(name);
                    }
                }
            }

            return Ok(certs);
        }

        [Route("sign")]
        [HttpPost]
        public IActionResult Sign()
        {
            //X509Store my = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //my.Open(OpenFlags.ReadOnly);

            //foreach (X509Certificate2 cert in my.Certificates)
            //{
            //    if (cert.Subject.Contains("CN=General User"))
            //    {
            //        SignPdf(cert);

            //        break;
            //    }
            //}

            //-------------

            SignPdfByPhysicalCertificate();


            return Ok();
        }



        public static void SignPdf(X509Certificate2 cert)
        {
            using (PdfReader reader = new PdfReader("E:\\inetpub\\PDFSigning\\App_Data\\Sample-Signatures.pdf"))
            {
                iText.IO.Image.ImageData img = iText.IO.Image.ImageDataFactory.CreatePng(new Uri("E:\\inetpub\\PDFSigning\\App_Data\\logo.png"));

                using (FileStream fs = new FileStream("E:\\inetpub\\PDFSigning\\App_Data\\signedFromStore.pdf", FileMode.Create))
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
                    ICipherParameters pk = Org.BouncyCastle.Security.DotNetUtilities.GetKeyPair(cert.PrivateKey).Private;

                    // Get Certificate chain
                    var cp = new Org.BouncyCastle.X509.X509CertificateParser();
                    var chain = new Org.BouncyCastle.X509.X509Certificate[] {
                        cp.ReadCertificate(cert.RawData)
                    };


                    IExternalSignature pks = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);
                    //== Use the SmartCard with pin to sign
                    //IExternalSignature pks = new SmartCardSignature();

                    signer.SignDetached(pks, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                }
            }
        }

        public static void SignPdfByPhysicalCertificate()
        {
            //This is a cert with a private key and known pwd
            string KEYSTORE = "E:\\inetpub\\PDFSigning\\App_Data\\GeneralUser.pfx";
            char[] PASSWORD = "password1234".ToCharArray();

            using (PdfReader reader = new PdfReader("E:\\inetpub\\PDFSigning\\App_Data\\Sample-Signatures.pdf"))
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

                using (FileStream fs = new FileStream("E:\\inetpub\\PDFSigning\\App_Data\\signedFromPfx.pdf", FileMode.Create))
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
    }
}
