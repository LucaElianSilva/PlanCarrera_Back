using Microsoft.AspNetCore.Mvc;
using PlanCarrera.Business.Services;

namespace PlanCarrera.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PersonaController : Controller
    {
        private readonly IPersonaService _service;

        public PersonaController(IPersonaService personaService)
        {
            _service = personaService;
        }

        [HttpGet]
        public IActionResult GetPersonas()
        {
            try
            {
                return Ok(_service.List());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}