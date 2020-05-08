using AutoMapper;
using Backend.Dtos;
using Domain;

namespace Backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Guia, GuiaDto>();
            CreateMap<GuiaStatus, GuiaStatusDto>();
            CreateMap<GuiaTipo, GuiaTipoDto>();
            CreateMap<StatusCheckIn, StatusCheckInDto>();
        }
        
    }
}