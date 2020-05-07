using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Models;
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
        public Guia Post(
            [FromServices] IGuiaRepository guiaRepository,
            [FromServices] IUnitOfWork uow,
            Guia guia){
            
            try
            {
                guiaRepository.Save(guia);
                uow.Commit();

                return guia;
            }
            catch (System.Exception ex)
            {
                uow.Rollback();
                throw new Exception (ex.Message.ToString());
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