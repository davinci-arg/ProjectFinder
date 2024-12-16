using ProjectFinder.Web.Models;

namespace ProjectFinder.Web.Service.IService;

public interface IGitHubAPIService
{
    Task<ResponseDto> GetRepositoriesAsync(string search);

}
