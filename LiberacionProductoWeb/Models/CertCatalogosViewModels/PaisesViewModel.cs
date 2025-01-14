using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class PaisesViewModel : SechToolDistributionBatchVM
    {
        public Paises Paises { get; set; }
        public List<Paises> PaisesList { get; set; }
        public List<String> SelectedPaisesFilter { get; set; }
        public List<SelectListItem> PaisesFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class Paises
    {
        public string iD_PAIS { get; set; }        
        public string nombre { get; set; }        
        public int iD_STATUS { get; set; }
        public string identificador { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoPaises
    {
        public string iD_PAIS { get; set; }
        public string nombre { get; set; }
        public string iD_STATUS { get; set; }
    }
}
