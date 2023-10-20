using API.Dtos;
using API.Dtos.DtosPost;
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


          CreateMap<Laboratorio, LabxMedicaDto>().ReverseMap();
          CreateMap<Medicamento, MedicaNombreDto>().ReverseMap();
          CreateMap<Mascota, MascotaNombreDto>().ReverseMap();
          CreateMap<Raza, RazaXEspecieDto>().ReverseMap();
          CreateMap<Especie, EspecieXRazaDto>().ReverseMap();
          CreateMap<Propietario, PropConMascotaDto>().ReverseMap();
          CreateMap<Cita, CitaMascoDto>().ReverseMap();
          CreateMap<Veterinario, VeterConMascoDto>().ReverseMap();
          CreateMap<Usuario, UsuarioDto>().ReverseMap();
          CreateMap<Mascota, MascotaConPropietarioDto>().ReverseMap();
          CreateMap<Raza, MascotaXRazaDto>().ReverseMap();
          CreateMap<TotalMascotasXRaza, TotalMascotasXRazaDto>().ReverseMap();
          CreateMap<MovimientosConTotal, MovimientosConTotalDto>().ReverseMap();
          CreateMap<Medicamento, MedicamentoConProvDto>().ReverseMap();
          CreateMap<MedicamentoProveedor, MedicProvDto>().ReverseMap();
          
          CreateMap<Cita, CitaPDto>().ReverseMap();
          CreateMap<Especie, EspeciePDto>().ReverseMap();
          CreateMap<FormulaMedica, FormulaMedicaPDto>().ReverseMap();
          CreateMap<Laboratorio, LaboratorioPDto>().ReverseMap();
          CreateMap<Mascota, MascotaPDto>().ReverseMap();
          CreateMap<Medicamento, MedicamentoPDto>().ReverseMap();
          CreateMap<Movimiento, MovimientoPDto>().ReverseMap();
          CreateMap<Propietario, PropietarioPDto>().ReverseMap();
          CreateMap<Proveedor, ProveedorPDto>().ReverseMap();
          CreateMap<Raza, RazaPDto>().ReverseMap();
          CreateMap<TipoMovimiento, TipMovPDto>().ReverseMap();
          CreateMap<Veterinario, VeterinarioPDto>().ReverseMap();
           CreateMap<Usuario, UsuarioPDto>().ReverseMap();
           CreateMap<DetalleMovimiento, DetalleMovPDto>().ReverseMap();

     }
}
