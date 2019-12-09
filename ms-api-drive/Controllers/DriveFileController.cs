using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ms_api_drive.Dto;
using ms_api_drive.Service;
using ms_api_drive.Util;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Controllers
{
    [Route(Constant.PathBase)]
    [ApiController]
    [EnableCors(Constant.Policy)]
    public class DriveFileController : ControllerBase
    {
        private readonly IDriveService _iDriveService;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DriveFileController(IDriveService iDriveService)
        {
            _iDriveService = iDriveService;
        }

        /// <summary>
        /// Insert File in Drive
        /// </summary>
        [HttpPost]
        [Route(Constant.DriveFile)]
        [Produces("application/json")]
        [ProducesResponseType(Constant.StatusCodeOk, Type = typeof(List<FileDto>))]
        [ProducesResponseType(Constant.StatusCodeNotFound)]
        public ActionResult<List<FileDto>> InsertFile([FromForm] UploadFileDto uploadFileDto)
        {
            List<FileDto> response = null;
            try
            {
                if (Request.Form.Files.Count > Constant.Zero && !string.IsNullOrEmpty(uploadFileDto.FolderIdDto))
                {
                    response = _iDriveService.InsertFile(uploadFileDto.FolderIdDto, Request.Form.Files);
                }
                else
                {
                    return NotFound(Constant.CreateFileError);
                }

            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);

            }

            return Ok(response);


        }

        /// <summary>
        /// Copy a file
        /// </summary>
        [HttpPost]
        [Route(Constant.DriveFileCopy)]
        [Produces("application/json")]
        [ProducesResponseType(Constant.StatusCodeOk, Type = typeof(FileDto))]
        [ProducesResponseType(Constant.StatusCodeNotFound)]
        public ActionResult<FileDto> CopyFileOrigin(string fileId, string folderId)
        {
            FileDto response = null;
            try
            {
                if (!string.IsNullOrEmpty(fileId) && !string.IsNullOrEmpty(folderId))
                {
                    response = _iDriveService.CopyFileOrigin(fileId, folderId);
                }

            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);

            }
            return Ok(response);

        }
    }

}