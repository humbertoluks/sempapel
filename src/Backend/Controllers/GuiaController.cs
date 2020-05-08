using System;
using System.Threading.Tasks;
using Domain;
using Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections;
using Backend.Dtos;
using System.Collections.Generic;

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

                var result = _mapper.Map<GuiaDto>(guia);

                return Created($"/v1/guias/{result.Id}", result);                
            }
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var guias = await _guiaRepository.GetAsync();
                //var results = _mapper.Map<IEnumerable<GuiaDto>>(guias);

                return base.Ok(guias); 
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