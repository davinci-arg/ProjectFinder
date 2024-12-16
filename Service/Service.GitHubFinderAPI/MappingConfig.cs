using AutoMapper;
using Service.GitHubFinderAPI.Models;
using Service.GitHubFinderAPI.Models.Dto;

namespace Service.GitHubFinderAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<GitHubFinder, GitHubFinderDto>();
            config.CreateMap<GitHubFinderDto, GitHubFinder>();
        });

        return mappingConfig;
    }
}
