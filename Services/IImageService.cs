namespace Forms.Services;

public interface IImageService
{
    Task InitializeAsync();
    Task UploadFileAsync(Stream fileStream, string fileName);
    string GetPublicUrl(string fileName);
}
