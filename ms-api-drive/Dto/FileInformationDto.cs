using System;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Dto
{
    [Serializable]
    public class FileInformationDto
    {
        public string IdDto { get; set; }
        public string NameDto { get; set; }
        public string MimeTypeDto { get; set; }
        public string UrlDto { get; set; }
    }
}
