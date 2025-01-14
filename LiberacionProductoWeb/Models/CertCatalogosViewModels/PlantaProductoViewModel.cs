using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class PlantaProductoViewModel : SechToolDistributionBatchVM
    {
        public PlantaProducto PlantaProducto { get; set; }
        public List<PlantaProducto> PlantaProductoList { get; set; }
        public List<String> SelectedPlantaProductoFilter { get; set; }
        public List<SelectListItem> PlantaProductoFilter { get; set; }

        // Planta Lista
        public List<Plantas> PlantasList { get; set; }
        public List<SelectListItem> PlantasFilter { get; set; }
        public List<String> SelectedPlantasFilter { get; set; }

        // Pais Lista
        public List<Paises> PaisesList { get; set; }
        public List<SelectListItem> PaisesFilter { get; set; }

        // Tipo Suministros Lista
        public List<TipoSuministros> TipoSuministrosList { get; set; }
        public List<SelectListItem> TipoSuministrosFilter { get; set; }

        // Producto Lista
        public List<Productos> ProductosList { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        // List Excel Export
        public List<PlantaProductoExportExcel> PlantaProductoExportExcelList { get; set; }
        public List<SelectListItem> PlantaProductoExportExcelFilter { get; set; }

        //messages vieews
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class PlantaProducto
    {
        public int? iD_PLANTA_PRODUCTO { get; set; }
        public int? iD_PLANTA { get; set; }
        public int? iD_PRODUCTO { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoPlantaProducto
    {
        public string iD_PLANTA_PRODUCTO { get; set; }
        public string iD_PLANTA { get; set; }
        public string iD_PRODUCTO { get; set; }
        public string iD_STATUS { get; set; }
    }
}
