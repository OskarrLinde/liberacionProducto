using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class TipoTransporteViewModel : SechToolDistributionBatchVM
    {
        public TipoTransporte TipoTransporte { get; set; }
        public List<TipoTransporte> TipoTransporteList { get; set; }
        public List<String> SelectedTipoTransporteFilter { get; set; }
        public List<SelectListItem> TipoTransporteFilter { get; set; }

        //messages vieews
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class TipoTransporte
    {
        public int? iD_TIPO_TRANSPORTE { get; set; }
        public string descripcion { get; set; }
        public string analizar { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoTipoTransporte
    {
        public string iD_TIPO_TRANSPORTE { get; set; }
        public string descripcion { get; set; }
        public string analizar { get; set; }
        public string iD_STATUS { get; set; }
    }
}
