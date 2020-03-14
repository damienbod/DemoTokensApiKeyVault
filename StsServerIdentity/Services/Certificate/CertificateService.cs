using System.Security.Cryptography.X509Certificates;

namespace StsServerIdentity.Services.Certificate
{
    public static class CertificateService
    {
        public static (X509Certificate2, X509Certificate2) GetCertificate(CertificateConfiguration certificateConfiguration, bool isProduction)
        {
            (X509Certificate2, X509Certificate2) certs = (null, null);

            if (isProduction)
            {
                if (certificateConfiguration.UseLocalCertStore)
                {
                    using X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                    store.Open(OpenFlags.ReadOnly);
                    var storeCerts = store.Certificates.Find(X509FindType.FindByThumbprint, certificateConfiguration.CertificateThumbprint, false);
                    certs.Item1 = storeCerts[0];
                    store.Close();
                }
                else
                {
                    var keyVaultEndpoint = certificateConfiguration.KeyVaultEndpoint;
                    KeyVaultCertificateService keyVaultCertificateService
                        = new KeyVaultCertificateService(keyVaultEndpoint, certificateConfiguration.CertificateNameKeyVault);

                    certs = keyVaultCertificateService.GetCertificateFromKeyVault();
                }
            }
            else
            {
                certs.Item1 = new X509Certificate2(
                    certificateConfiguration.DevelopmentCertificatePfx,
                    certificateConfiguration.DevelopmentCertificatePassword);
            }

            return certs;
        }

    }
}
