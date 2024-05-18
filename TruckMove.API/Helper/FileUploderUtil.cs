namespace TruckMove.API.Helper
{
    public static class FileUploderUtil
    {

        public async static Task<string> UploadImage(string rootLocation, FileUpload fileUpload, string _folder,string scheme , HostString Host)
        {

                string folder = _folder;
                
                var uploadsFolder = Path.Combine(rootLocation);
                var uploadsFolder2 = Path.Combine(uploadsFolder, folder);

                if (!Directory.Exists(uploadsFolder2))
                {
                    Directory.CreateDirectory(uploadsFolder2);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileUpload.file.FileName;
                var filePath = Path.Combine(uploadsFolder2, uniqueFileName);



                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileUpload.file.CopyToAsync(stream);
                }

                var fileUrl = $"{scheme}://{Host}/uploads/{folder}/{uniqueFileName}";
                return fileUrl;
            
           
        }
    }
}
