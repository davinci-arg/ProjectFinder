namespace ProjectFinder.Web.Utility;

public class SD
{
    public static string GitHubSearchRepositoryAPI { get; set; }
    public static string GitHubFinderAPIService { get; set; }
    public static string AuthAPIService { get; set; }
    public const string ADMINISTRATOR = "ADMINISTRATOR";
    public const string COSTUMER = "COSTUMER";
    public const string TOKEN_COOKIE = "JWTToken";
    public enum ApiType
    {
        GET, POST, PUT, DELETE
    }
}
