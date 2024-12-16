namespace ProjectFinder.Web.Utility;

public class SD
{
    public static string GitHubSearchRepositoryAPI { get; set; }
    public static string GitHubFinderAPIService { get; set; }
    public static string AuthAPIService { get; set; }
    public enum ApiType
    {
        GET, POST, PUT, DELETE
    }
}
