using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class FuenteSuministroViewModel : SechToolDistributionBatchVM
    {
        public FuenteSuministro FuenteSuministro { get; set; }
        public List<FuenteSuministro> FuenteSuministroList { get; set; }
        public List<String> SelectedFuenteSuministroFilter { get; set; }
        public List<SelectListItem> FuenteSuministroFilter { get; set; }
    }

    public class FuenteSuministro
    {
        public int? ID_FUENTE_SUMINISTRO { get; set; }        
        public string descripcion { get; set; }        
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }        
    }

    public class DtoFuenteSuministro
    {
        public string ID_FUENTE_SUMINISTRO { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
        public string usR_ALTA { get; set; }
        public string usR_MODIFICA { get; set; }
    }
}
