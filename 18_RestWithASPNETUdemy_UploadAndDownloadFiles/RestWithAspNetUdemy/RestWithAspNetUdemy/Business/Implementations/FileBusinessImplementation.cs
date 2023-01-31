using Microsoft.AspNetCore.Http;
using RestWithAspNetUdemy.Data.VO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestWithAspNetUdemy.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        private string _caminhoAPI = "/api/v1/file/";
        private string _diretorioUpload = "\\UploadDir\\";

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + _diretorioUpload;
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;

            return File.ReadAllBytes(filePath);
        }
        
        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO();

            if (file != null && file.Length > 0)
            {
                var fileType = Path.GetExtension(file.FileName);
                var baseUrl = _context.HttpContext.Request.Host;

                if (fileType.ToLower().Equals(".pdf") ||
                    fileType.ToLower().Equals(".jpg") ||
                    fileType.ToLower().Equals(".png") ||
                    fileType.ToLower().Equals(".jpeg"))
                {
                    var docName = Path.GetFileName(file.FileName);
                    var destination = Path.Combine(_basePath, string.Empty, docName);

                    fileDetail.DocName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + _caminhoAPI + fileDetail.DocName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }

            return fileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>();

            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }

            return list;
        }
    }
}
