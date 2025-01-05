using ProjectFinder.Web.Models;

namespace ProjectFinder.Web.Service.IService;

public interface IBaseService
{
    Task<ResponseDto>? SendAsync(RequestDto requestDto, bool withBearer = true);
}
