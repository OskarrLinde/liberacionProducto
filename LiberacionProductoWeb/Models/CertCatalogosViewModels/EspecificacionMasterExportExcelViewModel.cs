using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class EspecificacionMasterExportExcelViewModel : SechToolDistributionBatchVM
    {
        public EspecificacionMasterExportExcel EspecificacionMasterExportExcel { get; set; }
        public List<EspecificacionMasterExportExcel> EspecificacionMasterExportExcelList { get; set; }
        public List<String> SelectedEspecificacionMasterExcelFilter { get; set; }
        public List<SelectListItem> EspecificacionMasterExportExcelFilter { get; set; }
    }

    public class EspecificacionMasterExportExcel()
    {
        public int? iD_PLANTA { get; set; }
        public int? iD_PRODUCTO { get; set; }
        public string observaciones { get; set; }
        public int iD_STATUS { get; set; }
        public int? iD_PARAMETRO { get; set; }
        public int? iD_UNIDAD_MEDIDA { get; set; }
        public decimal? valoR_LIMITE_SUP { get; set; }
        public decimal? valoR_LIMITE_INF { get; set; }
        public string observaciones2 { get; set; }
        public int orden { get; set; }
        
        public int? iD_CLIENTE { get; set; }
        public int tanquE_PX { get; set; }        
        public int? iD_GRADO { get; set; }
    }
}
