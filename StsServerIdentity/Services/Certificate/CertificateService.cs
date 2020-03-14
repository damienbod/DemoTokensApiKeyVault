using System.Security.Cryptography.X509Certificates;

namespace StsServerIdentity.Services.Certificate
{
    public static class CertificateService
    {
        public static X509Certificate2 GetCertificate(CertificateConfiguration certificateConfiguration, bool isProduction)
        {
            X509Certificate2 cert;

            if (isProduction)
            {
                if (certificateConfiguration.UseLocalCertStore)
                {
                    using X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                    store.Open(OpenFlags.ReadOnly);
                    var certs = store.Certificates.Find(X509FindType.FindByThumbprint, certificateConfiguration.CertificateThumbprint, false);
                    cert = certs[0];
                    store.Close();
                }
                else
                {
                    var keyVaultEndpoint = certificateConfiguration.KeyVaultEndpoint;
                    KeyVaultCertificateService keyVaultCertificateService
                        = new KeyVaultCertificateService(keyVaultEndpoint, certificateConfiguration.CertificateNameKeyVault);

                    cert = keyVaultCertificateService.GetCertificateFromKeyVault();
                }
            }
            else
            {
                cert = new X509Certificate2(
                    certificateConfiguration.DevelopmentCertificatePfx,
                    certificateConfiguration.DevelopmentCertificatePassword);
            }

            return cert;
        }

    }
}
