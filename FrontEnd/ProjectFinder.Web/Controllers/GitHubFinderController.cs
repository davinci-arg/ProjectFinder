using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectFinder.Web.Models;
using ProjectFinder.Web.Service.IService;

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

    public IActionResult Finder()
    {
        return View();
    }

    [HttpGet]
    // [Authorize(Roles = $"{SD.ADMINISTRATOR},{SD.COSTUMER}")]
    public async Task<IActionResult> Repositories(GitHubFinderDto gitHubFinderDto)
    {
        if (string.IsNullOrWhiteSpace(gitHubFinderDto.ProjectName))
        {
            TempData["error"] = "please enter a project name";
            return RedirectToAction(nameof(Finder));
        }

        ViewData["FinderModel"] = gitHubFinderDto;
        List<RepositoryDto> repositories = new();
        ResponseDto response = await _gitHubFinderService.FindAsync(gitHubFinderDto.ProjectName);

        if (response.Result == null)
        {
            response = await _gitHubAPIService.GetRepositoriesAsync(gitHubFinderDto.ProjectName);
            gitHubFinderDto.Repositories = response.Result.ToString();
            response = await _gitHubFinderService.SaveAsync(gitHubFinderDto);
        }

        if (response.Result != null && response.IsSuccess)
        {
            repositories = JsonConvert.DeserializeObject<List<RepositoryDto>>(Convert.ToString(response.Result));
        }
        else
        {
            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Finder));
        }

        return View(repositories);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string projectName)
    {
        if (string.IsNullOrEmpty(projectName))
        {
            return BadRequest();
        }

        await _gitHubFinderService.DeleteAsync(projectName);
        return Ok(projectName);
    }
}
