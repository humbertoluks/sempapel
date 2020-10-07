using System.Reflection;
using AutoMapper;
using Backend.Dtos;
using Domain;

namespace Backend.Helpers
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
        (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof (TSource);
            var destinationProperties = typeof (TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }

    
    public class AutoMapperProfile : Profile
    {
       
        public AutoMapperProfile()
        {

            CreateMap<Guia, GuiaDto>();
            CreateMap<GuiaStatus, GuiaStatusDto>();
            CreateMap<GuiaTipo, GuiaTipoDto>();
            CreateMap<StatusCheckIn, StatusCheckInDto>();
            CreateMap<PutGuiaDto, Guia>();
            //  .ForMember(dest => dest.GuiaStatusId, opts => opts.MapFrom(src => src.GuiaStatus.Id))
            //  .ForMember(dest => dest.StatusCheckInId, opts => opts.MapFrom(src => src.StatusCheckIn.Id))
            //  .ForMember(dest => dest.GuiaCancelada, opts => opts.MapFrom(src => src.GuiaCancelada))
            //  .ForMember(dest => dest.NumeroLote, opts => opts.MapFrom(src => src.NumeroLote));
        }
    }
}
