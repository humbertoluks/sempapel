using System;
using System.Threading.Tasks;
using Domain;
using Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    [ApiController]
    [Route("v1/guias")]
    public class GuiaController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        [Authorize]
        public IActionResult Post(
            [FromServices] IGuiaRepository guiaRepository,
            [FromServices] IUnitOfWork uow,
            Guia guia){
            
            try
            {
                guiaRepository.Save(guia);
                uow.Commit();

                return this.StatusCode(StatusCodes.Status201Created);
            }
            catch (System.Exception ex)
            {
                uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados, detalhes: "+ ex);
            }
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Get([FromServices] IGuiaRepository guiaRepository)
        {
            try
            {
                var results = await guiaRepository.GetAsync();
                return base.Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados");
            }
        }

        [HttpGet("{id}")]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Get([FromServices] IGuiaRepository guiaRepository, int id)
        {
            try
            {
                var results = await guiaRepository.GetAsync(id);
                return base.Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados");
            }
        }
    }
}