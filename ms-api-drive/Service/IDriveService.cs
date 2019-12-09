using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using ms_api_drive.Dto;
using System.Collections.Generic;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Service
{
    public interface IDriveService
    {
        FolderDto CreateFolder(string folderName, string description);
        FolderDto CreateFolderInFolder(string folderId, string folderName);

        Permission AddPermission(string fileId, string email, string type, string role);

        bool DeleteFolder(string folderId);

        List<FileDto> ListFolder();

        List<FileDto> InsertFile(string parentId, IFormFileCollection formFiles);

        FileDto CopyFileOrigin(string fileId, string folderId);
    }
}
