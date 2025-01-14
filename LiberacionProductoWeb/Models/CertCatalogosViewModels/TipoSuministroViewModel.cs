using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class TipoSuministroViewModel : SechToolDistributionBatchVM
    {
        public TipoSuministros TipoSuministros { get; set; }
        public List<TipoSuministros> TipoSuministrosList { get; set; }
        public List<String> SelectedTipoSuministrosFilter { get; set; }
        public List<SelectListItem> TipoSuministrosFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }
    public class TipoSuministros
    {
        public int? iD_TIPO_SUMINISTRO { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public string identificador { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }
    public class DtoTipoSuministros
    {
        public string iD_TIPO_SUMINISTRO { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
    }
}
