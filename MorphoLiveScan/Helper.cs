namespace MorphoLiveScan
{
    public static class Helper
    {
        public static string ConvertToBase64String(IFormFile file)
        {
            using var memoryStream = new System.IO.MemoryStream();
            file.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();
            var base64String = Convert.ToBase64String(bytes);
            return base64String;
        }
    }
}
