using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using log4net;
using Microsoft.AspNetCore.Http;
using ms_api_drive.Dto;
using ms_api_drive.Service;
using ms_api_drive.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Imp
{
    public class ImpGoogleDrive : IGoogleDriveService
    {
        private readonly IGoogleDriveCredentialService _googleDriveCredentialService;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Files _objFiles = new Files();

        public ImpGoogleDrive(IGoogleDriveCredentialService googleDriveCredentialService)
        {
            _googleDriveCredentialService = googleDriveCredentialService;
        }

        public DriveService GetDriveService()
        {

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _googleDriveCredentialService.GetCredentials(),
                ApplicationName = "Drive API Sample"
            });

            return service;
        }

        /// <summary>
        /// Insert a new permission.
        /// </summary>
        /// <param name="service">Drive API service instance.</param>
        /// <param name="fileId">ID of the file to insert permission for.</param>
        /// <param name="email">
        /// User or group e-mail address, domain name or null for "default" type.
        /// </param>
        /// <param name="type">The value "user", "group", "domain" or "default".</param>
        /// <param name="role">The value "owner", "writer" or "reader".</param>
        /// <returns>The inserted permission, null is returned if an API error occurred</returns>
        public Permission AddPermission(DriveService service, string fileId, string email, string type, string role)
        {
            Permission newPermission = null;

            try
            {
                newPermission = new Permission()
                {
                    EmailAddress = email,
                    Type = type,
                    //Domain = "user",
                    Role = role
                };

                newPermission = service.Permissions.Create(newPermission, fileId).Execute();

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return newPermission;
        }

        /// <param name="service">Drive API service instance.</param>
        /// <param name="title">Title of the file to insert, including the extension.</param>
        /// <param name="description">Description of the file to insert.</param>
        /// <param name="parentId">Parent folder's ID.</param>
        /// <param name="mimeType">MIME type of the file to insert.</param>
        public List<FileDto> InsertFile(DriveService service, string parentId, IFormFileCollection formFiles)
        {
            List<FileDto> listFileDtoResponse = new List<FileDto>();

            try
            {
                if (formFiles.Count > Constant.Zero)
                {
                    var listFileDto = _objFiles.ToByteArrayAsync(formFiles, parentId).Result;

                    for (int i = Constant.Zero; i < listFileDto.FileListDto.Count; i++)
                    {
                        var objFile = listFileDto.FileListDto[i];
                        var byteFile = listFileDto.ListByteDto[i];
                        System.IO.MemoryStream stream = new System.IO.MemoryStream(byteFile);
                        FilesResource.CreateMediaUpload request = service.Files.Create(objFile, stream, objFile.MimeType);
                        request.Upload();

                        var fileExtension = _objFiles.TransformDocNameIntoStringTypeFile(request.ResponseBody.Name);
                        if (null != fileExtension)
                        {
                            var fileDto = new FileDto()
                            {
                                IdDto = request.ResponseBody.Id,
                                NameDto = request.ResponseBody.Name,
                                MimeTypeDto = request.ResponseBody.MimeType,
                                UrlDto = fileExtension + request.ResponseBody.Id
                            };
                            listFileDtoResponse.Add(fileDto);
                        }

                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return listFileDtoResponse;
        }

        public List<FileDto> ListFiles(DriveService service)
        {
            List<FileDto> respon = new List<FileDto>();

            try
            {
                FilesResource.ListRequest request = service.Files.List();
                FileList files = request.Execute();
                if (files.Files.Count > Constant.Zero)
                {
                    foreach (var obj in files.Files)
                    {
                        var fileExtension = _objFiles.TransformDocNameIntoStringTypeFile(obj.Name);
                        var fileDto = new FileDto()
                        {
                            IdDto = obj.Id,
                            MimeTypeDto = obj.MimeType,
                            NameDto = obj.Name,
                            UrlDto = fileExtension + obj.Id
                        };
                        respon.Add(fileDto);

                    }
                }


            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return respon;


        }

        public FolderDto CreateFolder(DriveService service, string folderName, string description)
        {
            FolderDto result = null;
            try
            {
                var fileMetadata = new File();

                fileMetadata.Name = folderName;
                fileMetadata.Description = description;
                fileMetadata.MimeType = "application/vnd.google-apps.folder";
                fileMetadata.CreatedTime = new DateTime();
                var file = service.Files.Create(fileMetadata).Execute();
                if (null != file)
                {
                    result = new FolderDto()
                    {
                        IdDto = file.Id,
                        NameDto = file.Name,
                        MimeTypeDto = file.MimeType,
                        UrlDto = null
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }

            return result;
        }


        public FolderDto CreateFolderInFolder(DriveService service, string folderName, string folderId)
        {
            FolderDto result = null;
            try
            {

                var fileMetaData = new File()
                {
                    Name = System.IO.Path.GetFileName(folderName),
                    MimeType = "application/vnd.google-apps.folder",
                    CreatedTime = new DateTime(),
                    Parents = new List<string> { folderId }
                };

                FilesResource.CreateRequest request;
                request = service.Files.Create(fileMetaData);
                request.Fields = "id";
                var file = request.Execute();

                if (null != file)
                {
                    result = new FolderDto()
                    {
                        IdDto = file.Id,
                        NameDto = file.Name,
                        MimeTypeDto = file.MimeType,
                        UrlDto = null
                    };
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }

            return result;
        }


        public bool DeleteFolder(DriveService service, string folderId)
        {
            bool result = false;
            try
            {
                service.Files.Delete(folderId).Execute();
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }

            return result;
        }

        public FileDto CopyFileOrigin(DriveService service, string fileId, string folderId)
        {
            FileDto fileDto = null;
            try
            {
                File copiedFile = service.Files.Get(fileId).Execute();

                var body = new File
                {
                    Name = copiedFile.Name,
                    MimeType = copiedFile.MimeType,
                    Parents = new List<string> { folderId }
                };

                var request = service.Files.Copy(body, fileId);
                var copy = request.Execute();

                if (null != copy)
                {
                    fileDto = new FileDto()
                    {
                        IdDto = copy.Id,
                        NameDto = copy.Name,
                        MimeTypeDto = copy.MimeType,
                        UrlDto = Constant.UrlWordDrive + copy.Id
                    };
                    
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }
            return fileDto;
        }
    }
}
