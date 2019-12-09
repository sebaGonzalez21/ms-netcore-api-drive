using System;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Dto
{
    [Serializable]
    public class PermissionUserDto
    {
        public string IdDto { get; set; }
        public string EmailDto { get; set; }
        public string TypeDto { get; set; }
        public string RoleDto { get; set; }
    }
}
