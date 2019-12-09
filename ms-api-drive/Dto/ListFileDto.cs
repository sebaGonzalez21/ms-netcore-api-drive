using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Dto
{
    [Serializable]
    public class ListFileDto
    {
         public List<File> FileListDto { get; set; }
        public List<byte[]> ListByteDto { get; set; }


        public ListFileDto() {
            FileListDto = new List<File>();
            ListByteDto = new List<byte[]>();
        }
    }
}
