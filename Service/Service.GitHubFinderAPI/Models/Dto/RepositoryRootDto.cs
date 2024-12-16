using Newtonsoft.Json;

namespace Service.GitHubFinderAPI.Models.Dto;

public class RepositoryRootDto
{
    [JsonProperty("items")]
    public List<RepositoryDto> Repositories { get; set; }
}
