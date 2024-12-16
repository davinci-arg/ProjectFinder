using Newtonsoft.Json;

namespace Service.GitHubFinderAPI.Models.Dto;

public class OwnerDto
{
    [JsonProperty("login")]
    public string Author { get; set; }
}
