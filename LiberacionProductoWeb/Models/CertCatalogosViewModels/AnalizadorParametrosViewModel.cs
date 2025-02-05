using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class AnalizadorParametrosViewModel : SechToolDistributionBatchVM
    {
        public AnalizadorParametros AnalizadorParametros { get; set; }
        public List<AnalizadorParametros> AnalizadorParametrosList { get; set; }
        public List<String> SelectedAnalizadorParametrosFilter { get; set; }
        public List<SelectListItem> AnalizadorParametrosFilter { get; set; }

        // Lista Analizador
        public List<Analizador> AnalizadorList { get; set; }
        public List<SelectListItem> AnalizadorFilter { get; set; }

        // Productos Lista
        public List<Productos> ProductosList { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        // Lista Plantas
        public List<Plantas> PlantasList { get; set; }
        public List<SelectListItem> PlantasFilter { get; set; }

        // Lista Parametros 
        public List<Parametro> ParametrosList { get; set; }
        public List<SelectListItem> ParametrosFilter { get; set; }

        // Lista Metodo
        public List<Metodo> MetodoList { get; set; }
        public List<SelectListItem> MetodoFilter { get; set; }

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

		// List Excel Export
		public List<AnalizadorParametros> AnalizadorParametrosExportExcelList { get; set; }
        public List<SelectListItem> AnalizadorParametrosExportExcelFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
        public bool showAll { get; set; }
    }

    public class AnalizadorParametros
    {
        public int? iD_ANALIZADOR_PARAM { get; set; }
        public int? iD_ANALIZADOR { get; set; }
        public int? iD_PLANTA { get; set; }
        public int? iD_PARAMETRO { get; set; }
        public int? iD_METODO { get; set; }
        public string descripcion { get; set; }
        public string limitE_INFERIOR { get; set; }
        public string leyendA_REPORTE { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoAnalizadorParametros
    {
        public string iD_ANALIZADOR_PARAM { get; set; }
        public string iD_ANALIZADOR { get; set; }
        public string iD_PLANTA { get; set; }
        public string iD_PARAMETRO { get; set; }
        public string iD_METODO { get; set; }
        public string descripcion { get; set; }
        public string limitE_INFERIOR { get; set; }
        public string leyendA_REPORTE { get; set; }
        public string clave_Pals { get; set; }
        public string iD_STATUS { get; set; }
    }
}
