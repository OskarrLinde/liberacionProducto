using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class PlantaProductoExportExcelViewModel : SechToolDistributionBatchVM
    {
        public PlantaProductoExportExcel PlantaProductoExportExcel { get; set; }
        public List<PlantaProductoExportExcel> PlantaProductoExportExcelList { get; set; }
        public List<String> SelectedPlantaProductoExportExcelFilter { get; set; }
        public List<SelectListItem> PlantaProductoExportExcelFilter { get; set; }
    }

    public class PlantaProductoExportExcel()
    {
        public int? iD_PLANTA { get; set; }
        public string iD_PAIS { get; set; }
        public int? iD_TIPO_SUMINISTRO { get; set; }
        public string descripcion { get; set; }
        public string clavE_CERTIFICADO { get; set; }
        public string identificador { get; set; }
        public int iD_STATUS { get; set; }                
        public int? iD_PRODUCTO { get; set; }        
    }
}
