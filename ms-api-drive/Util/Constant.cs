using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ms_api_drive.Util
{
    public class Constant
    {
        /**
        * static variable
        * */
        public const string Policy = "MyPolicy";
        public const string PathBase = "/api";
        public const string UrlWordDrive = "https://drive.google.com/file/d/";

        /**
         * 
         * Message Error
         * */
        public const string CreateFileError = "MyPolicy";


        /**
         * Controller
         * */
        public const string ApiHealth = "/api/health";
        public const string DriveFolder = "drive/folder";
        public const string DriveFolderDelete = "drive/folder/{fileId}";
        public const string DriveFile = "drive/file";
        public const string DriveFileCopy = "drive/file/{fileId}/folder/{folderId}";
        public const string DriveFilesPermission = "drive/permission";
        /**
         * Connection p12 and account service
         * 
         * */
        public const string ServiceAccount = "SERVICE_ACCOUNT";
        public const string KeyP12 = "KEY_PATH";
        public const string Secret = "SECRET";
        public const string Port = "PORT_MS_API_DRIVE";
        public const string Host = "HOST_MS_API_DRIVE";

        public const int Zero = 0;
        public const int One = 1;
        public const int StatusCodeOk = 200;
        public const int StatusCodeNotFound = 404;

        /***
         * Extension
         * */
        public const string TypeFileDocxUrl = "https://docs.google.com/document/d/";
        public const string TypeFileXlsxUrl = "https://docs.google.com/spreadsheets/d/";

        public const string ExtensionDocx = "docx";
        public const string ExtensionXlsx = "xlsx";
        public const string ExtensionXls = "xls";
        public const string ExtensionCsv = "csv";
        public const string ExtensionJpg = "jpg";

        /***
         * Split
         * */

        public const string SplitPoint = ".";
    }
}
