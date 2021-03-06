using api_curso_react.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_curso_react.Models
{
    public class Cobrador : IEntidadAuditable
    {
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }        
        public bool Activo { get; set; }

        #region Datos de Auditoría
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        #endregion

        #region Relaciones
        public TipoIdentificacion TipoIdentificacionOperador { get; set; }
        public string IdentificacionOperador { get; set; }
        public Operador Operador { get; set; }
        #endregion
    }
}
