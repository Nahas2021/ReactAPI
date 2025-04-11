namespace ReactAPI
{
    public class UploadService
    {
        public string Upload(IFormFile file)
        {
            // Allowed extensions
            List<string> validExtensions = new List<string> { ".jpg", ".png", ".gif" };

            // Get file extension
            string extension = Path.GetExtension(file.FileName).ToLower();

            // Validate extension
            if (!validExtensions.Contains(extension))
            {
                return $"Extension is not valid ({string.Join(',', validExtensions)})";
            }

            // Validate file size (max 5MB)
            long size = file.Length;
            if (size > (5 * 1024 * 1024))
                return "Maximum size can be 5MB";

            // Generate unique file name
            string fileName = Guid.NewGuid().ToString() + extension;

            // Define upload directory
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            // Ensure directory exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Save file
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Flush(); // Ensure all data is written
            }

            return fileName;
        }
    }
}
