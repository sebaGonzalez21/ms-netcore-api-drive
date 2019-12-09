using Google.Apis.Drive.v3;
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
    public interface IGoogleDriveService
    {
        DriveService GetDriveService();

        Permission AddPermission(DriveService service, string fileId, string email, string type, string role);

        List<FileDto> InsertFile(DriveService service, string parentId, IFormFileCollection formFiles);

        List<FileDto> ListFiles(DriveService service);

        FolderDto CreateFolder(DriveService service, string folderName, string description);

        FolderDto CreateFolderInFolder(DriveService service, string folderName, string folderId);

        FileDto CopyFileOrigin(DriveService service, string fileId, string folderId);

        bool DeleteFolder(DriveService service, string folderId);

    }
}
