namespace xamarinProject.Backend.Helpers
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class FilesHelper
    {
        private IHostingEnvironment _env;

        public FilesHelper(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadPhotoAsync(IFormFile file, string folder)
        {

            string path = string.Empty;
            string pic = string.Empty;

            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(_env.ContentRootPath, folder, pic);
                using (var stream = System.IO.File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return pic;
        }
    }
}
