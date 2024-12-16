using System.ComponentModel.DataAnnotations;

namespace Service.GitHubFinderAPI.Models;

public class GitHubFinder
{
    [Key]
    public string? ProjectName { get; set; }
    [Required]
    public string? Repositories { get; set; }
}
