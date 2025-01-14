using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class TipoEspecificacionViewModel : SechToolDistributionBatchVM
    {
        public TipoEspecificacion TipoEspecificacion { get; set; }
        public List<TipoEspecificacion> TipoEspecificacionList { get; set; }
        public List<String> SelectedTipoEspecificacionFilter { get; set; }
        public List<SelectListItem> TipoEspecificacionFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class TipoEspecificacion
    {
        public int? iD_TIPO_ESPECIFICACION { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoTipoEspecificacion
    {
        public string iD_TIPO_ESPECIFICACION { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
    }
}
