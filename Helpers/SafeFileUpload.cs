namespace RealEstateBank.Helpers;

public static class SafeFileUpload {
    public static string? CheckFile(IFormFile file, HashSet<string> allowedFileTypes, int maxSizeInBytes) {
        var fileType = Path.GetExtension(file.FileName);

        if (!allowedFileTypes.Contains(fileType))
            return "File type isn't allowed";

        if (file.Length > maxSizeInBytes)
            return "File size is too big";

        return null;
    }
}
