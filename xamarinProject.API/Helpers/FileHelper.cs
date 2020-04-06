namespace xamarinProject.API.Helpers
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;

    public class FileHelper
    {
        public static bool UploadPhoto(MemoryStream ms, string folder, string name)
        {
            try
            {
                ms.Position = 0;
                var partialPath = Directory.GetCurrentDirectory();
                var path = Path.Combine(partialPath, folder, name);
                File.WriteAllBytes(path, ms.ToArray());
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
