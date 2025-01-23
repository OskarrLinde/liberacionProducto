using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class PlantaViewModel : SechToolDistributionBatchVM
    {
        public Plantas Plantas { get; set; }
        public List<Plantas> PlantaList { get; set; }
        public List<String> SelectedPlantasFilter { get; set; }
        public List<SelectListItem> PlantasFilter { get; set; }

        // Pais Lista
        public List<Paises> PaisesList { get; set; }
        public List<SelectListItem> PaisesFilter { get; set; }

        // Tipo Suministros Lista
        public List<TipoSuministros> TipoSuministrosList { get; set; }
        public List<SelectListItem> TipoSuministrosFilter { get; set; }

        // Fuente Suministro Lista
        public List<FuenteSuministro> FuenteSuministroList { get; set; }
        public List<SelectListItem> FuenteSuministroFilter { get; set; }

        // Planta Aprobada Lista
        public List<PlantaAprobada> PlantaAprobadaList { get; set; }
        public List<SelectListItem> PlantaAprobadFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class Plantas
    {
        public int? iD_PLANTA { get; set; }
        public string iD_PAIS { get; set; }
        public int? iD_TIPO_SUMINISTRO { get; set; }
        public string descripcion { get; set; }
        public string clavE_CERTIFICADO { get; set; }
        public string identificador { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
        public int? ID_FUENTE_SUMINISTRO { get; set; }
        public int? ID_PLANTA_APROBADA { get; set; }
    }

    public class DtoPlantas
    {
        public string iD_PLANTA { get; set; }
        public string iD_PAIS { get; set; }
        public string iD_TIPO_SUMINISTRO { get; set; }
        public string descripcion { get; set; }
		public string identificador { get; set; }
		public string clavE_CERTIFICADO { get; set; }
        public string iD_STATUS { get; set; }
        public string ID_FUENTE_SUMINISTRO { get; set; }
        public string ID_PLANTA_APROBADA { get; set; }
    }
}
