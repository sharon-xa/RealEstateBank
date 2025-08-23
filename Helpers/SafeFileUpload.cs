namespace RealEstateBank.Helpers;

public static class SafeFileUpload {
    public static string? CheckFile(IFormFile file, HashSet<string> allowedFileTypes, int maxSizeInBytes) {
        if (file == null || file.Length == 0)
            return "no file uploaded";

        var fileType = Path.GetExtension(file.FileName);
        if (fileType == null)
            return "File type isn't allowed";

        fileType = fileType.Remove(0, 1);

        if (!allowedFileTypes.Contains(fileType))
            return "File type isn't allowed";

        if (file.Length > maxSizeInBytes)
            return "File size is too big";

        return null;
    }
}
