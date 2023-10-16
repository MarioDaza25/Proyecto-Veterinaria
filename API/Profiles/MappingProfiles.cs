using API.Dtos;
using AutoMapper;
using Dominio.Entidades;

namespace API.Profiles;

public class MappingProfiles : Profile
{
     public MappingProfiles()
     {
        CreateMap<Cita, CitaDto>().ReverseMap();
        CreateMap<DetalleMovimiento, DetalleMovDto>().ReverseMap();
        CreateMap<Especie, EspecieDto>().ReverseMap();
        CreateMap<FormulaMedica, FormulaDto>().ReverseMap();
        CreateMap<Laboratorio, LaboratorioDto>().ReverseMap();
        CreateMap<Mascota, MascotaDto>().ReverseMap();
        CreateMap<Medicamento, MedicamentoDto>().ReverseMap();
        CreateMap<Movimiento, MovimientoDto>().ReverseMap();
        CreateMap<Propietario, PropietarioDto>().ReverseMap();
        CreateMap<Proveedor, ProveedorDto>().ReverseMap();
        CreateMap<Raza, RazaDto>().ReverseMap();
        CreateMap<TipoMovimiento, TipoMovDto>().ReverseMap();
        CreateMap<Veterinario, VeterinarioDto>().ReverseMap();

        CreateMap<Rol, RolDto>().ReverseMap();
     }
}
