using Supabase;

namespace Forms.Services;

public class SupabaseImageService(string supabaseUrl, string supabaseKey, string bucketName)
    : IImageService
{
    private Client Client { get; } = new Client(supabaseUrl, supabaseKey);
    private string BucketName { get; } = bucketName;

    public async Task InitializeAsync()
    {
        await Client.InitializeAsync();
    }

    // TODO: handle errors
    public async Task UploadFileAsync(Stream fileStream, string fileName)
    {
        using var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);
        byte[] fileBytes = memoryStream.ToArray();
        var storage = Client.Storage;
        var response = await storage.From(BucketName).Upload(fileBytes, fileName);
        Console.WriteLine(response);
    }

    public string GetPublicUrl(string fileName)
    {
        return Client.Storage.From(BucketName).GetPublicUrl(fileName);
    }
}
