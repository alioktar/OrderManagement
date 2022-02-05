using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.HttpApi.Base
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
