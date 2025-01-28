using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class ParametroViewModel : SechToolDistributionBatchVM
    {
        public Parametro Parametro { get; set; }
        public List<Parametro> ParametroList { get; set; }
        public List<String> SelectedParametroFilter { get; set; }
        public List<SelectListItem> ParametroFilter { get; set; }

        // Lista Unidad Medidas
        public List<UnidadMedidas> UnidadMedidasList { get; set; }
        public List<SelectListItem> UnidadMedidasFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
        public bool showAll { get; internal set; }
    }

    public class Parametro
    {
        public int? iD_PARAMETRO { get; set; }
        public int? iD_UNIDAD_MEDIDA { get; set; }
        public int? decimaleS_CERTIFICADO { get; set; }
        public string clavE_PALS { get; set; }
        public string leyenda { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoParametro
    {
        public string iD_PARAMETRO { get; set; }
        public string iD_UNIDAD_MEDIDA { get; set; }
        public string decimaleS_CERTIFICADO { get; set; }
        public string clavE_PALS { get; set; }
        public string leyenda { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
    }
}
