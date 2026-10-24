# Certificate Signing Server

While thinking through the design, the end-user certificate located on the smartcard is automatically copied to the certificate (MY / Personal) store during login.  
Which means the cert will not be available on the web server. This is not a good way to manage all the company's employee certificates just to sign PDF documents.  
Feel the best way to handle, is to create a generic certificate to be use for the actual signing of the PDF document. The PDFSigner class offers a way to customize
the signature panel with logo and/or signer's name.

One thought would to standup a IIS server to act as a middle exchange server (PDF Signing Service). This server would require a smartcard (or certificate-based)
authentication to prove user's identity. Once authenticated and the page as loaded, the PDF Signing Service would process the query string data. This could performed
many operations such as;

- fetching document from some repository
- then obtain the generic certificate from the MY store
- customize the signature with signer's name
- finally stamp the PDF and upload back to repository

One issues with using a smart-card for signing PDF via IIS web server, PDFSinger required **BOTH** the smart-card certificate AND the certificate in the store.
This might require to use a generic smart-card certificate for signing, but customize the signature panel on the PDF document.

## Notes

When reading a certificate directly from a `.pfx` file, the same certificate **must** be imported into the MY store.
This is require to valid the signature in the PDF document.

Smart-card signing seems to maintain its validation even when emailed to another user.

## Test Cases

- TEST 1 - Sign PDF with primary smart card (Jeremy R. Travis) and emailed to Rick F. to open and validate.
  - Successful! the signature were validated.
  - Since I logged onto his computer once, thought I would try LDA.
- TEST 2 - Sign PDF with LDA smart card (FBPLDA51) and email to Rick F. to open and validate.
  - Successful! the signature were validated.
- TEST 3 - Sign PDF with a general certificate with exportable key (KDJ) and email to Rick F. to open and validate.
  - Failed! the signature was not validated. A copy of the certificate must be visible in the MY store.

## Certificate (PowerShell)

```powershell
#Get-PfxCertificate -FilePath "C:\someFile.pfx"

#----
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store([System.Security.Cryptography.X509Certificates.StoreName]::My,"CurrentUser") #LocalMachine
$store.Open("MaxAllowed")
$store.Certificates
#$cert = $store.Certificates | ?{$_.subject -match "^CN=JEREMY R. TRAVIS"}
#$cert.PrivateKey
#$cert.PrivateKey.ToXmlString($false)
$store.Close()

#----

$date_now = Get-Date
$extended_date = $date_now.AddYears(2)
New-SelfSignedCertificate -Subject "CN=General User" -NotAfter $extended_date -KeyAlgorithm RSA -KeyLength 2048 -CertStoreLocation "cert:CurrentUser\My" -KeyUsage DigitalSignature

$thumbprint = "96B2E81871F4F4D5441E510912835DFBBA150A70"
$pwd = ConvertTo-SecureString -String "password1234" -Force -AsPlainText
$path = ("cert:\CurrentUser\My\" + $thumbprint)
Export-PfxCertificate -cert $path -FilePath c:\deploy\GeneralUser.pfx -Password $pwd
```
