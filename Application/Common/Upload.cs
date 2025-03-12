using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Common;

public  class Upload(IWebHostEnvironment environment)
{
    public string UploadFile(IFormFile? file, string directory)
    {
        if (file == null) return string.Empty;
    
        var fullDirectoryPath = Path.Combine(environment.WebRootPath, "Images", directory); //wwwroow/images/product
    
        if (!Directory.Exists(fullDirectoryPath))
        {
            Directory.CreateDirectory(fullDirectoryPath);
        }

        var filePath = Path.Combine(fullDirectoryPath, file.FileName);//wwwroow/images/product/logo.png
    
        // بررسی اینکه آیا فایلی با نام مشابه وجود دارد
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath); // حذف فایل در صورت وجود
        }

        using var f = System.IO.File.Create(filePath);//wwwroow/images/product/logo.png
        file.CopyTo(f);

        return file.FileName;
    }
}