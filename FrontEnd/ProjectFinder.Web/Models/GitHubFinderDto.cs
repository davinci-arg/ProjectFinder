using System.ComponentModel.DataAnnotations;

namespace ProjectFinder.Web.Models;

public class GitHubFinderDto
{
    public string ProjectName { get; set; }
    public string Repositories { get; set; }
}
