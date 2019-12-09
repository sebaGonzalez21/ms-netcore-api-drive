using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using log4net;
using Microsoft.Extensions.Configuration;
using ms_api_drive.Service;
using ms_api_drive.Util;
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Imp
{
    public class ImpGoogleDriveCredential : IGoogleDriveCredentialService
    {

        static string[] Scopes = { DriveService.Scope.Drive };
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ImpGoogleDriveCredential(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        public ServiceAccountCredential GetCredentials()
        {
            ServiceAccountCredential serviceAccountCredential = null;
            try
            {
                var keyFilePath = Configuration.GetConnectionString(Constant.KeyP12);
                var serviceAccountEmail = Configuration.GetConnectionString(Constant.ServiceAccount);
                var secret = Configuration.GetConnectionString(Constant.Secret);
                var certificate = new X509Certificate2(keyFilePath, secret, X509KeyStorageFlags.Exportable);
                serviceAccountCredential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = Scopes,

                }.FromCertificate(certificate));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }

            return serviceAccountCredential;

        }
    }
}
