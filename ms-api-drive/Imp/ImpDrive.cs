using Google.Apis.Drive.v3.Data;
using log4net;
using Microsoft.AspNetCore.Http;
using ms_api_drive.Dto;
using ms_api_drive.Service;
using System;
using System.Collections.Generic;
using System.Reflection;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Imp
{
    public class ImpDrive : IDriveService
    {
        private readonly IGoogleDriveService _googleDriveOperation;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ImpDrive(IGoogleDriveService googleDriveOperation)
        {
            _googleDriveOperation = googleDriveOperation;
        }

        public FolderDto CreateFolder(string folderName, string description)
        {
            FolderDto response = null;
            try
            {
                var service = _googleDriveOperation.GetDriveService();
                response = _googleDriveOperation.CreateFolder(service, folderName, description);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return response;
        }

        public FolderDto CreateFolderInFolder(string folderName, string folderId)
        {
            FolderDto response = null;
            try
            {
                var service = _googleDriveOperation.GetDriveService();
                response = _googleDriveOperation.CreateFolderInFolder(service, folderName, folderId);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return response;
        }

        public List<FileDto> ListFolder()
        {
            List<FileDto> response = null;
            try
            {
                var service = _googleDriveOperation.GetDriveService();
                response = _googleDriveOperation.ListFiles(service);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return response;
        }

        public Permission AddPermission(string fileId, string email, string type, string role)
        {
            Permission objPermmision = null;
            try
            {
                var service = _googleDriveOperation.GetDriveService();
                objPermmision = _googleDriveOperation.AddPermission(service, fileId, email, type, role);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return objPermmision;
        }


        public bool DeleteFolder(string folderId)
        {
            bool delete = false;

            try
            {
                var service = _googleDriveOperation.GetDriveService();
                delete = _googleDriveOperation.DeleteFolder(service, folderId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return delete;
        }

        public List<FileDto> InsertFile(string parentId, IFormFileCollection formFiles)
        {
            List<FileDto> response = null;
            try
            {
                var service = _googleDriveOperation.GetDriveService();
                response = _googleDriveOperation.InsertFile(service, parentId, formFiles);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return response;
        }

        public FileDto CopyFileOrigin(string fileId, string folderId)
        {
            FileDto response = null;
            try
            {
                var service = _googleDriveOperation.GetDriveService();
                response = _googleDriveOperation.CopyFileOrigin(service, fileId, folderId);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return response;
        }
    }
}
