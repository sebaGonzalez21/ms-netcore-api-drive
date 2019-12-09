using System;
using System.Runtime.Serialization;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive.Dto
{
    [Serializable]
    public class CreateFolderDto : ISerializable
    {
        public string NameDto { get; set; }
        public string DescriptionDto { get; set; }
        public string IdFolderDto { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}
