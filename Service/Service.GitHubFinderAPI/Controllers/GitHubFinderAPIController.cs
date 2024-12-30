using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.GitHubFinderAPI.Data;
using Service.GitHubFinderAPI.Models;
using Service.GitHubFinderAPI.Models.Dto;

namespace Service.GitHubFinderAPI.Controllers;

[ApiController]
[Route("api/find")]
public class GitHubFinderAPIController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ResponseDto _response;
    private readonly IMapper _mapper;

    public GitHubFinderAPIController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _response = new ResponseDto();
    }

    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            List<RepositoryDto> repositories = new();
            var gitHubFinders = _db.GitHubFinders.Select(p => p.Repositories);

            foreach (var repository in gitHubFinders)
            {
                repositories.AddRange(JsonConvert.DeserializeObject<RepositoryRootDto>(repository).Repositories);
            }

            _response.Result = repositories;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            _response.Success = false;
        }

        return _response;
    }

    [HttpGet("{nameFinder}")]
    public ResponseDto Get(string nameFinder)
    {
        try
        {
            GitHubFinder finder = _db.GitHubFinders.First(f => f.ProjectName.ToLower() == nameFinder.ToLower());
            var root = JsonConvert.DeserializeObject<RepositoryRootDto>(finder.Repositories);
            _response.Result = root.Repositories;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            _response.Success = false;
        }

        return _response;
    }

    [HttpPost]
    public ResponseDto Post([FromBody] GitHubFinderDto gitHubFinderDto)
    {
        try
        {
            var root = JsonConvert.DeserializeObject<RepositoryRootDto>(gitHubFinderDto.Repositories);
            _db.GitHubFinders.Add(_mapper.Map<GitHubFinder>(gitHubFinderDto));
            _db.SaveChanges();
            _response.Result = root.Repositories;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            _response.Success = false;
        }

        return _response;
    }

    [HttpDelete("{id}")]
    public ResponseDto Delete(string id)
    {
        try
        {
            _db.GitHubFinders.Remove(_db.GitHubFinders.First(f => f.ProjectName.ToLower() == id.ToLower()));
            _db.SaveChanges();
            //_response.Result = nameFinder;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            _response.Success = false;
        }
        return _response;
    }

}
