using Microsoft.AspNetCore.Http;
using System;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Dto
{
    [Serializable]
    public class UploadFileDto
    {
        public IFormFile FileDto { get; set; }
        public string FolderIdDto { get; set; }
    }
}
