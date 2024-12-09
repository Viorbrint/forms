public class ApiClient(HttpClient httpClient)
{
    public async Task<HttpResponseMessage> Signin(SigninModel model)
    {
        const string url = "auth/signin";
        return await httpClient.PostAsJsonAsync(url, model);
    }
}
