using Google.Apis.Auth.OAuth2;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Service
{
    public interface IGoogleDriveCredentialService
    {
      ServiceAccountCredential GetCredentials();

    }
}
