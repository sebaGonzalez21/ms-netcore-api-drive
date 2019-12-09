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
    public class DriveFolderController : ControllerBase
    {
        private readonly IDriveService _iDriveService;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DriveFolderController(IDriveService iDriveService)
        {
            _iDriveService = iDriveService;
        }
        /// <summary>
        /// Create Sub-Folder in Folder in Drive
        /// </summary>
        [HttpPost]
        [Route(Constant.DriveFolder)]
        [Produces("application/json")]
        [ProducesResponseType(Constant.StatusCodeOk, Type = typeof(FolderDto))]
        [ProducesResponseType(Constant.StatusCodeNotFound)]
        public ActionResult<FolderDto> CreateFolderInFolder([FromBody] CreateFolderDto createFolderDto)
        {
            FolderDto response = null;
            try
            {
                if (!string.IsNullOrEmpty(createFolderDto.IdFolderDto))
                {

                    response = _iDriveService.CreateFolderInFolder(createFolderDto.NameDto, createFolderDto.IdFolderDto);
                }
                else
                {
                    response = _iDriveService.CreateFolder(createFolderDto.NameDto, createFolderDto.DescriptionDto);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);

            }
            return Ok(response);

        }

        /// <summary>
        /// Delete Sub-Folder in Folder in Drive
        /// </summary>
        [HttpDelete]
        [Route(Constant.DriveFolderDelete)]
        [Produces("application/json")]
        [ProducesResponseType(Constant.StatusCodeOk, Type = typeof(bool))]
        [ProducesResponseType(Constant.StatusCodeNotFound)]
        public ActionResult<bool> DeleteFolder(string fileId)
        {
            bool response = false;
            try
            {
                if (!string.IsNullOrEmpty(fileId))
                {
                    response = _iDriveService.DeleteFolder(fileId);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);

            }

            return Ok(response);

        }

        /// <summary>
        /// Get List of Folders
        /// </summary>
        [HttpGet]
        [Route(Constant.DriveFolder)]
        [Produces("application/json")]
        [ProducesResponseType(Constant.StatusCodeOk, Type = typeof(List<FileDto>))]
        [ProducesResponseType(Constant.StatusCodeNotFound)]
        public ActionResult<List<FileDto>> ListFolder()
        {
            List<FileDto> response = null;
            try
            {
                response = _iDriveService.ListFolder();

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);

            }

            return Ok(response);


        }
    }
}