using System.Security.Authentication;
using Salesforce.Common;
using Salesforce.Force;

namespace Forms.Services;

public class SalesforceService(
    string clientId,
    string clientSecret,
    string username,
    string password,
    string securityToken,
    ILogger<SalesforceService> logger
)
{
    private string ClientId { get; } = clientId;
    private string ClientSecret { get; } = clientSecret;
    private string Username { get; } = username;
    private string Password { get; } = password;
    private string SecurityToken { get; } = securityToken;
    private ILogger<SalesforceService> Logger { get; } = logger;

    public async Task<ForceClient> GetForceClientAsync()
    {
        try
        {
            var auth = new AuthenticationClient();
            await auth.UsernamePasswordAsync(
                ClientId,
                ClientSecret,
                Username,
                Password + SecurityToken
            );
            Logger.LogInformation("Authentication successful.");
            return new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
        }
        catch (AuthenticationException ex)
        {
            Logger.LogError(ex, "Authentication failed for user {Username}.", Username);
            throw new Exception("Authentication failed. Please check your credentials.");
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, "HTTP request error occurred during authentication.");
            throw new SalesforceException("Network error while connecting to Salesforce.");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Unexpected error during authentication process.");
            throw new Exception("An unexpected error occurred during authentication.");
        }
    }

    public async Task CreateAccountWithContact(
        string accountName,
        string contactFirstName,
        string contactLastName,
        string contactEmail
    )
    {
        try
        {
            var client = await GetForceClientAsync();

            var account = new { Name = accountName };
            var accountResult = await client.CreateAsync("Account", account);

            try
            {
                var contact = new
                {
                    FirstName = contactFirstName,
                    LastName = contactLastName,
                    Email = contactEmail,
                    AccountId = accountResult.Id,
                };
                await client.CreateAsync("Contact", contact);
            }
            catch
            {
                await client.DeleteAsync("Account", accountResult.Id);
                throw;
            }
            Logger.LogInformation(
                "Account and Contact successfully created for {ContactFirstName} {ContactLastName}.",
                contactFirstName,
                contactLastName
            );
        }
        catch (ForceException ex)
        {
            Logger.LogError(ex, "Salesforce API error occurred while creating account or contact.");
            throw new SalesforceException(
                "Failed to create records in Salesforce. Please try again."
            );
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, "HTTP request error occurred while communicating with Salesforce.");
            throw new SalesforceException("Network error while communicating with Salesforce.");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Unexpected error occurred while creating records.");
            throw new Exception("An unexpected error occurred while creating records.");
        }
    }
}

public class SalesforceException(string message) : Exception(message) { }
