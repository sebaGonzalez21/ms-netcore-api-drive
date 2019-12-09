using System;
using System.Reflection;
using Google.Apis.Drive.v3.Data;
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
    public class PermissionController : ControllerBase
    {

        private readonly IDriveService _iDriveService;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public PermissionController(IDriveService iDriveService)
        {
            _iDriveService = iDriveService;
        }

        /// <summary>
        /// Give Permission into Files and Folders
        /// </summary>
        [HttpPost]
        [Route(Constant.DriveFilesPermission)]
        [Produces("application/json")]
        [ProducesResponseType(Constant.StatusCodeOk, Type = typeof(Permission))]
        [ProducesResponseType(Constant.StatusCodeNotFound)]
        public ActionResult<Permission> Permisson(PermissionUserDto permissionUserDto)
        {
            Permission response = null;
            try
            {

                response = _iDriveService.AddPermission(permissionUserDto.IdDto, permissionUserDto.EmailDto, permissionUserDto.TypeDto, permissionUserDto.RoleDto);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);

            }

            return Ok(response);


        }
    }
}
