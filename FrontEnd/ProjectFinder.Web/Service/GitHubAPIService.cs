using System.Net.Http.Headers;
using ProjectFinder.Web.Models;
using ProjectFinder.Web.Service.IService;
using ProjectFinder.Web.Utility;
using static ProjectFinder.Web.Utility.SD;

namespace ProjectFinder.Web.Service;

public class GitHubAPIService : IGitHubAPIService
{
    private readonly IBaseService _baseService;

    public GitHubAPIService(IEnumerable<IBaseService> services)
    {
        _baseService = services.First(service => service is GitHubService);
    }

    public async Task<ResponseDto> GetRepositoriesAsync(string search)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.GitHubSearchRepositoryAPI + search
        });
    }
}
