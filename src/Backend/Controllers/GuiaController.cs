using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Backend.Dtos;
using Domain;
using Repository.Interfaces;

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

        [HttpPost]
        [Route("Delete/{IdGuiaExterno}")]
        [Authorize]
        public async Task<IActionResult> Delete(int IdGuiaExterno){
            try
            {
                _guiaRepository.Delete(IdGuiaExterno);
                await _uow.CommitAsync();

                return Ok();
            }
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("")]
        //[Authorize (AuthenticationSchemes = "Bearer")]
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
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpGet("GetByIdExterno/{IdGuiaExterno}")]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetByIdExterno(int IdGuiaExterno)
        {
            try
            {
                var guia = await _guiaRepository.GetByIdExternoAsync(IdGuiaExterno);

                var result = _mapper.Map<GuiaDto>(guia);

                return base.Ok(result); 
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Put(PutGuiaDto putGuia)
        {
            try
            {
                var guia = await _guiaRepository.GetByIdExternoAsync(putGuia.IdGuiaExterno);

                _mapper.Map<PutGuiaDto, Guia>(putGuia, guia);

                _guiaRepository.Update(guia);

                await _uow.CommitAsync();

                return base.Ok(guia); 
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }
    }
}
