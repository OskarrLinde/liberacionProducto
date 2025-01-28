using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacionProducto.Entities.Entities.Lotificacion
{
    public class ListadoLotificacion
    {
        public class AnalisisTanque
        {
            public int IdAnalisis { get; set; }
            public string? IdLote { get; set; }
            //public int IdTanque { get; set; }
            public string? IdTanque { get; set; }
            //public decimal NivelIni { get; set; }
            public string NivelIni { get; set; }
            public decimal? NivelIniVal { get; set; }
            public int? UMInicial { get; set; }
            public string? UMInicialDesc { get; set; }
            public string NivelFin { get; set; }
            public decimal? NivelFinVal { get; set; }
            public int? UMFinal { get; set; }
            public string? UMFinalDesc { get; set; }

            public string? Comentarios { get; set; }
            public string? LoteOrigen { get; set; }
            public string? Estatus { get; set; }
            public DateTime? FechaAlta { get; set; }
            public string? UsrAlta { get; set; }
            public int GetUseralta { get; set; }
            public bool PermisoUser { get; set; }
            public bool PermisoUserAdmin { get; set; }
            public List<DetalleAnalisisTanque> Detalles { get; set; }
        }

        public class DetalleAnalisisTanque
        {
            public int? IdAnalisisDetail { get; set; }
            public int? IdPlanta { get; set; }
            public int? IdParametro { get; set; }
            public string? descParametro { get; set; }
            public decimal ValorLimiteInf { get; set; }
            public decimal ValorLimiteSup { get; set; }
            public decimal ValorAnalisis { get; set; }
            public string? IdAnalizador { get; set; }
            public string? IdMetodo { get; set; }
            public int? IdMetodoVal { get; set; }
            public string? DescUnidadMedida { get; set; }
            public string? MotivoCancelBitacora { get; set; }
            public DateTime? FechaCancelBitacora { get; set; }
            public int? UserCancelBitacora { get; set; }
            public string? UserNameCancelBitacora { get; set; }
            public int? IdAnalizadorVal { get; set; }

        }

        public class ListadoLotificacionData
        {
            public int IdPlanta { get; set; } // Id de la planta
            public string IdProducto { get; set; } // Id del producto
            public DateTime FechaInicial { get; set; }
            public DateTime FechaFinal { get; set; }
        }

    }
}
