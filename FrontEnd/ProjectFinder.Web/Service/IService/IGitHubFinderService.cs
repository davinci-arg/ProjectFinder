using ProjectFinder.Web.Models;

namespace ProjectFinder.Web.Service.IService;

public interface IGitHubFinderService
{
    Task<ResponseDto> FindAsync(string projectName);
    Task<ResponseDto> SaveAsync(GitHubFinderDto gitHubFinderDto);
    Task<ResponseDto> DeleteAsync(string projectName);
}
