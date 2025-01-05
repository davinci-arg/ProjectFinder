using System.Net.Http.Headers;
using ProjectFinder.Web.Models;
using ProjectFinder.Web.Service.IService;
using ProjectFinder.Web.Utility;

namespace ProjectFinder.Web.Service;

public class GitHubFinderService : IGitHubFinderService
{
    private readonly IBaseService _baseService;

    public GitHubFinderService(IEnumerable<IBaseService> services)
    {
        _baseService = services.FirstOrDefault(service => service is BaseService);
    }

    public async Task<ResponseDto?> DeleteAsync(string projectName)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.DELETE,
            Url = SD.GitHubFinderAPIService + "/api/find/" + projectName,
            Header = new KeyValuePair<string, string>("Accept", "application/json")
        });
    }

    public async Task<ResponseDto?> FindAsync(string projectName)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.GitHubFinderAPIService + "/api/find/" + projectName,
            Header = new KeyValuePair<string, string>("Accept", "application/json")
        });
    }

    public async Task<ResponseDto?> SaveAsync(GitHubFinderDto gitHubFinderDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = gitHubFinderDto,
            Url = SD.GitHubFinderAPIService + "/api/find/",
            Header = new KeyValuePair<string, string>("Accept", "application/json")
        });
    }
}
