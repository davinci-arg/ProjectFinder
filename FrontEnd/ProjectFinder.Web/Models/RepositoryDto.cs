namespace ProjectFinder.Web.Models;

public class RepositoryDto
{
    public string Name { get; set; }
    public string RepositoryUrl { get; set; }
    public int Stargazers { get; set; }
    public int Watchers { get; set; }
    public OwnerDto Owner { get; set; }
}
