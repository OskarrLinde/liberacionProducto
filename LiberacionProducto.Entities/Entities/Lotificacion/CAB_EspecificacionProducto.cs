using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacionProducto.Entities.Entities.Lotificacion
{
    public class CAB_EspecificacionProducto
    {
        public int? IdProducto { get; set; }
        public int? IdParametro { get; set; }
        public string DescripcionParametro { get; set; }
        public decimal ValorLimiteInf { get; set; }
        public decimal ValorLimiteSup { get; set; }
        public int IdUnidadMedida { get; set; }
        public string DescripcionUnidadMedida { get; set; }
        public string Observaciones { get; set; }
    }
}
