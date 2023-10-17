using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CitaMascoDto
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public MascotaNombreDto Mascota { get; set; }
    }
}