using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Application.Main.Commons
{
    /// <summary>
    /// Configura las reglas de mapeo utilizadas por AutoMapper
    /// para convertir objetos entre las entidades del Domain
    /// y los DTOs de la capa Application.
    /// </summary>
    public class MappingsProfile : Profile
    {
        /// <summary>
        /// Constructor donde se registran las configuraciones
        /// de mapeo entre las diferentes clases.
        /// </summary>
        public MappingsProfile()
        {
            /*
             * Configura el mapeo entre Customer y CustomerDto.
             *
             * Customer → CustomerDto
             * CustomerDto → Customer
             *
             * ReverseMap() permite realizar el mapeo en ambos sentidos.
             *
             * Como ambas clases tienen propiedades con los mismos nombres
             * y tipos compatibles, AutoMapper puede realizar el mapeo
             * automáticamente.
             */
            CreateMap<Customer, CustomerDto>().ReverseMap();

            /*
             * EJEMPLO DE MAPEO MANUAL:
             *
             * La siguiente configuración realiza el mismo mapeo,
             * pero indicando explícitamente qué propiedad de origen
             * corresponde a cada propiedad de destino.
             *
             * Esto normalmente NO es necesario cuando las propiedades
             * tienen el mismo nombre y tipo, pero resulta útil cuando
             * los nombres son diferentes o se necesita una transformación.
             *
             * CreateMap<Customer, CustomerDto>().ReverseMap()
             *     .ForMember(
             *         destination => destination.CustomerId,
             *         source => source.MapFrom(src => src.CustomerId))
             *     .ForMember(
             *         destination => destination.CompanyName,
             *         source => source.MapFrom(src => src.CompanyName))
             *     .ForMember(
             *         destination => destination.ContactName,
             *         source => source.MapFrom(src => src.ContactName))
             *     .ForMember(
             *         destination => destination.ContactTitle,
             *         source => source.MapFrom(src => src.ContactTitle))
             *     .ForMember(
             *         destination => destination.Address,
             *         source => source.MapFrom(src => src.Address))
             *     .ForMember(
             *         destination => destination.City,
             *         source => source.MapFrom(src => src.City))
             *     .ForMember(
             *         destination => destination.Region,
             *         source => source.MapFrom(src => src.Region))
             *     .ForMember(
             *         destination => destination.PostalCode,
             *         source => source.MapFrom(src => src.PostalCode))
             *     .ForMember(
             *         destination => destination.Country,
             *         source => source.MapFrom(src => src.Country))
             *     .ForMember(
             *         destination => destination.Phone,
             *         source => source.MapFrom(src => src.Phone))
             *     .ForMember(
             *         destination => destination.Fax,
             *         source => source.MapFrom(src => src.Fax))
             *     .ReverseMap();
             *
             * Ejemplo:
             *
             * destination => destination.CustomerId
             *     → propiedad que queremos llenar.
             *
             * src => src.CustomerId
             *     → propiedad de donde obtenemos el valor.
             *
             * En este caso:
             * Customer.CustomerId → CustomerDto.CustomerId
             *
             * Como los nombres coinciden, AutoMapper puede hacerlo
             * automáticamente y por eso solamente necesitamos:
             *
             * CreateMap<Customer, CustomerDto>().ReverseMap();
             */
        }
    }
}