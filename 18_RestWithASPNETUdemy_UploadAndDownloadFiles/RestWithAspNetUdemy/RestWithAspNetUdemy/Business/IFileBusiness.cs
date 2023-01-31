using Microsoft.AspNetCore.Http;
using RestWithAspNetUdemy.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNetUdemy.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);

        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);

        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
