using Newtonsoft.Json;

namespace Service.GitHubFinderAPI.Models.Dto;

public class RepositoryDto
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("html_url")]
    public string RepositoryUrl { get; set; }
    [JsonProperty("stargazers_count")]
    public int Stargazers { get; set; }
    [JsonProperty("watchers_count")]
    public int Watchers { get; set; }
    [JsonProperty("owner")]
    public OwnerDto Owner { get; set; }
}
