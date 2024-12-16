using Microsoft.EntityFrameworkCore;
using Service.GitHubFinderAPI.Models;

namespace Service.GitHubFinderAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<GitHubFinder> GitHubFinders { get; set; }
}
