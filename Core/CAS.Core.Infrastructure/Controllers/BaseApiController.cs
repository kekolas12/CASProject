using Microsoft.AspNetCore.Mvc;

namespace CAS.Core.Infrastructure.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:ApiVersion}")]
    public abstract class BaseApiController : Controller
    {
        //public IActionResult CreateActionResultInstance<T>(DataSourceResult<T> response)
        //{
        //	return new ObjectResult(response)
        //	{
        //		StatusCode = response.StatusCode,
        //	};
        //}
        //public IActionResult CreateActionResultJson<T>(DataSourceResult<T> response)
        //{
        //	return new JsonResult(response)
        //	{
        //		StatusCode = response.StatusCode,
        //	};
        //}
    }
}
