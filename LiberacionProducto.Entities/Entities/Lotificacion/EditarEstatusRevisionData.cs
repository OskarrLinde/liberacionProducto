using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacionProducto.Entities.Entities.Lotificacion
{
    public class EditarEstatusRevisionData
    {
        public int IdAnalisis { get; set; }
        public string IdLote { get; set; } // Id del lote
        public int? Estatus_Revision { get; set; }
        public string? Comentarios { get; set; }
    }
}
