
using static ProjectFinder.Web.Utility.SD;

namespace ProjectFinder.Web.Models;

public class RequestDto
{
    public KeyValuePair<string, string> Header { get; set; }
    public ApiType ApiType { get; set; } = ApiType.GET;
    public ServiceType ServiceType { get; set; } = ServiceType.INTERNAL;
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
}
