using Google.Apis.Drive.v3.Data;
using log4net;
using Microsoft.AspNetCore.Http;
using ms_api_drive.Dto;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ms_api_drive.Util
{
    public class Files
    {

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public async System.Threading.Tasks.Task<ListFileDto> ToByteArrayAsync(IFormFileCollection formFiles, string parentId)
        {
            ListFileDto listFileDto = new ListFileDto();
            try
            {
                if (!string.IsNullOrEmpty(parentId))
                {
                    foreach (var file in formFiles)
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            var binary = memoryStream.ToArray();
                            File body = new File();
                            body.Name = file.FileName;
                            body.CreatedTime = new DateTime();
                            body.Parents = new List<string> { parentId };
                            listFileDto.FileListDto.Add(body);
                            listFileDto.ListByteDto.Add(binary);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }

            return listFileDto;
        }

        public string TransformDocNameIntoStringTypeFile(string fileName)
        {
            string name = null;
            try
            {
                if (null != fileName && fileName.Contains(Constant.SplitPoint))
                {
                    var data = fileName.Split(Constant.SplitPoint);
                    name = TypeOfFile(data[Constant.One]);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }

            return name;
        }

        public string TypeOfFile(string fileExtension)
        {
            string extension = null;
            try
            {
                if (null != fileExtension && fileExtension.ToLower() == Constant.ExtensionDocx)
                {
                    extension = Constant.TypeFileDocxUrl;
                }
                else if (null != fileExtension && (fileExtension.ToLower() == Constant.ExtensionXlsx || fileExtension.ToLower() == Constant.ExtensionCsv || fileExtension.ToLower() == Constant.ExtensionXls))
                {
                    extension = Constant.TypeFileXlsxUrl;
                }
                else
                {
                    extension = Constant.TypeFileDocxUrl;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
            }

            return extension;
        }
    }
}
