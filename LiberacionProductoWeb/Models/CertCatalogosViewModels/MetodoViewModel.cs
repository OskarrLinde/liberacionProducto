using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class MetodoViewModel : SechToolDistributionBatchVM
    {
        public Metodo Metodo { get; set; }
        public List<Metodo> MetodoList { get; set; }
        public List<String> SelectedMetodoFilter { get; set; }
        public List<SelectListItem> MetodoFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
        public bool showAll { get; internal set; }
    }

    public class Metodo
    {
        public int? iD_METODO { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoMetodo
    {
        public string iD_METODO { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }        
    }
}
