using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Threading.Tasks;

using Backend.Dtos;
using Domain;

namespace Backend.Controllers
{
    [ApiController]
    [Route("v1/guias")]
    public class GuiaController : ControllerBase
    {
        private readonly IGuiaRepository _guiaRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GuiaController([FromServices] IGuiaRepository guiaRepository, 
            [FromServices] IUnitOfWork uow,
            IMapper mapper)
        {
            this._guiaRepository = guiaRepository;
            this._uow = uow;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Post(Guia guia){
            
            try
            {
                _guiaRepository.Save(guia);
                
                await _uow.CommitAsync();
                return Created($"/v1/guias/{guia.Id}", _mapper.Map<GuiaDto>(guia));                
            }
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var guias = await _guiaRepository.GetAsync();
                var results = _mapper.Map<GuiaDto[]>(guias);

                //return base.Ok(); 
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var guia = await _guiaRepository.GetAsync(id);

                var result = _mapper.Map<GuiaDto>(guia);

                return base.Ok(result); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados");
            }
        }
    }
}