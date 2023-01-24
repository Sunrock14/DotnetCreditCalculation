using Credit.Core.Utilities.Results.Abstract;
using Credit.Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Credit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CustomResponse<T>(IDataResult<T> responce) 
        {
            switch (responce.StatusCode)
            {
                case 200: 
                    return Ok(responce.Data);
                case 204:
                    return NotFound();
                case 404:
                    return BadRequest();
                default:
                    return  NoContent();
            }
        }
    }
}
