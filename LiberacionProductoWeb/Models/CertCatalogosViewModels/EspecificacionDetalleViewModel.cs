using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class EspecificacionDetalleViewModel : SechToolDistributionBatchVM
    {
        public EspecificacionDetalle EspecificacionDetalle { get; set; }
        public List<EspecificacionDetalle> EspecificacionDetalleList { get; set; }
        public List<String> SelectedEspecificacionDetalleFilter { get; set; }
        public List<SelectListItem> EspecificacionDetalleFilter { get; set; }

        // Lista Parametros 
        public List<Parametro> ParametrosList { get; set; }
        public List<SelectListItem> ParametrosFilter { get; set; }

        // Lista Unidad Medida 
        public List<UnidadMedidas> UnidadMedidaList { get; set; }
        public List<SelectListItem> UnidadMedidaFilter { get; set; }

        //messages vieews
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class EspecificacionDetalle
    {
        public int id { get; set; }
        public int? iD_ESPECIFICACION { get; set; }
        public int? iD_PARAMETRO { get; set; }
        public int? iD_UNIDAD_MEDIDA { get; set; }
        public decimal? valoR_LIMITE_SUP { get; set; }
        public decimal? valoR_LIMITE_INF { get; set; }
        public string observaciones { get; set; }
        public int orden { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoEspecificacionDetalle
    {
        public string id { get; set; }
        public string iD_ESPECIFICACION { get; set; }
        public string iD_PARAMETRO { get; set; }
        public string iD_UNIDAD_MEDIDA { get; set; }
        public string valoR_LIMITE_SUP { get; set; }
        public string valoR_LIMITE_INF { get; set; }
        public string observaciones { get; set; }
        public string orden { get; set; }
        public string iD_STATUS { get; set; }        
    }
}
