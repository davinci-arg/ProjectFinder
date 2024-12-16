using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectFinder.Web.Models;
using ProjectFinder.Web.Service.IService;
using ProjectFinder.Web.Utility;

namespace ProjectFinder.Web.Controllers;


public class GitHubFinderController : Controller
{
    private readonly IGitHubFinderService _gitHubFinderService;
    private readonly IGitHubAPIService _gitHubAPIService;

    public GitHubFinderController(IGitHubFinderService gitHubFinderService, IGitHubAPIService gitHubAPIService)
    {
        _gitHubFinderService = gitHubFinderService;
        _gitHubAPIService = gitHubAPIService;
    }

    [HttpGet]
    public async Task<IActionResult> Repositories(GitHubFinderDto gitHubFinderDto)
    {
        if (string.IsNullOrWhiteSpace(gitHubFinderDto.ProjectName))
        {
            return View();
        }

        List<RepositoryDto> repositories = new();
        ResponseDto response = await _gitHubFinderService.FindAsync(gitHubFinderDto.ProjectName);

        if (response.Result == null)
        {
            response = await _gitHubAPIService.GetRepositoriesAsync(gitHubFinderDto.ProjectName);
            gitHubFinderDto.Repositories = response.Result.ToString();
            response = await _gitHubFinderService.SaveAsync(gitHubFinderDto);
        }

        repositories = JsonConvert.DeserializeObject<List<RepositoryDto>>(Convert.ToString(response.Result));
        ViewData["Search"] = gitHubFinderDto.ProjectName;

        return View(repositories);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string projectName)
    {
        if (string.IsNullOrEmpty(projectName))
        {
            return BadRequest();
        }

        return Ok(projectName);
    }

}
