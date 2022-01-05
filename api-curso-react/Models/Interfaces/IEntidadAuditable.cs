using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_curso_react.Models
{
    public interface IEntidadAuditable
    {
        public String CreadoPor { get; set; }
        public String ModificadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }
}
