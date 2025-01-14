using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacionProducto.Entities.Entities.Lotificacion
{
    public class LotificacionData
    {
        public MasterData MasterData { get; set; }
        public List<DetailData> DetailData { get; set; }
    }

    public class MasterData
    {
        public int IdAnalisis { get; set; } // Id del análisis
        public string IdLote { get; set; } // Id del lote
        public int IdPlanta { get; set; } // Id de la planta
        public string IdTanque { get; set; } // Id del tanque
        public string IdTanqueDesc { get; set; } // Id del tanque
        public string IdProducto { get; set; } // Id del producto
        public string NivelIni { get; set; } // Nivel inicial
        public string IdUMedidaIni { get; set; } // Id de la unidad de medida inicial
        public string NivelFin { get; set; } // Nivel final
        public string IdUMedidaFin { get; set; } // Id de la unidad de medida final
        public string Comentarios { get; set; } // Comentarios adicionales
        public string LoteOrigen { get; set; } // Lote de origen
        public int UsrAlta { get; set; }
        public int Estatus_Analisis { get; set; }
                                               
    }

    public class DetailData
    {
        public int IdAnalisis { get; set; }
        public string IdParametro { get; set; } // Id del parámetro
        public string ValorLimiteInf { get; set; } // Valor límite inferior
        public string ValorLimiteSup { get; set; } // Valor límite superior
        public string ValorAnalisis { get; set; } // Valor del análisis
        public string IdUnidadMedida { get; set; }
        public string IdAnalizador { get; set; } // Id del analizador
        public int IdMetodo { get; set; }
        public int UsrAlta { get; set; }
    }

    public class CancelarLoteData
    {
        public int IdAnalisis { get; set; }
        public int IdEstatus { get; set; }
        public string MotivoCancelacion { get; set; }
        public int UserCancel { get; set; }

    }
}
