using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackApi.Services;

namespace BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReflectionController : ControllerBase
    {
        private readonly IReflectionService _reflectionService;

        public ReflectionController(IReflectionService reflectionService)
        {
            _reflectionService = reflectionService;
        }

        [HttpGet("importers")]
        public ActionResult<IEnumerable<string>> GetImporters()
        {
            var dllNames = _reflectionService.GetImporterDllNames();
            return Ok(dllNames);
        }
    }
}