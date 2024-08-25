using Microsoft.AspNetCore.Http;

namespace PrintMaster.Application.Handle.HandleFile
{
    public class HandleUploadFile
    {
        public static async Task<string> Upfile(IFormFile file)
        {
            string fileName = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split(".").Length - 1];
                fileName = "Nguyensonfs_" + DateTime.Now.Ticks.ToString() + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files");
                if (!File.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var exactPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fileName;
        }


    }
}
