using DevExpress.Office.Utils;
using LiberacionProductoWeb.Data.Specifications.Base;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class EspecificacionMasterViewModel : SechToolDistributionBatchVM
    {
        public EspecificacionMaster EspecificacionMaster { get; set; }
        public List<EspecificacionMaster> EspecificacionMasterList { get; set; }
        public List<String> SelectedEspecificacionMasterFilter { get; set; }
        public List<SelectListItem> EspecificacionMasterFilter { get; set; }

        // Lista Planta 
        public List<Plantas> PlantasList { get; set; }
        public List<SelectListItem> PlantasFilter { get; set; }

        // Lista Tanque
        public List<Tanque> TanqueList { get; set; }
        public List<SelectListItem> TanqueFilter { get; set; }

        // Lista Cliente
        public List<Clientes> ClientesList { get; set; }
        public List<SelectListItem> ClientesFilter { get; set; }

        // Lista Productos
        public List<Productos> ProductosList { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        // Lista Grados
        public List<Grados> GradosList { get; set; }
        public List<SelectListItem> GradosFilter { get; set; }

        // Lista Tipo Especificacion
        public List<TipoEspecificacion> TipoEspecificacionList { get; set; }
        public List<SelectListItem> TipoEspecificacionFilter { get; set; }

        // Lista Parametros 
        public List<Parametro> ParametroList { get; set; }
        public List<SelectListItem> ParametroFilter { get; set; }

        // Lista Unidad Medidas
        public List<UnidadMedidas> UnidadMedidasList { get; set; }
        public List<SelectListItem> UnidadMedidasFilter { get; set; }

        // List Excel Export
        public List<EspecificacionMasterExportExcel> EspecificacionMasterExportExcelList { get; set; }
        public List<SelectListItem> EspecificacionMasterExportExcelFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class EspecificacionMaster
    {
        public int? iD_ESPECIFICACION { get; set; }
        public int? iD_PLANTA { get; set; }
        public int? iD_CLIENTE { get; set; }
        public int tanquE_PX { get; set; }
        public int? iD_PRODUCTO { get; set; }
        public int? iD_GRADO { get; set; }
        public int? iD_TIPO_ESPECIFICACION { get; set; }
        public int iD_STATUS { get; set; }
        public string observaciones { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoEspecificacionMaster
    {
        public string iD_ESPECIFICACION { get; set; }
        public string iD_PLANTA { get; set; }
        public string iD_CLIENTE { get; set; }
        public string tanquE_PX { get; set; }
        public string iD_PRODUCTO { get; set; }
        public string iD_GRADO { get; set; }
        public string iD_TIPO_ESPECIFICACION { get; set; }
        public string iD_STATUS { get; set; }
        public string observaciones { get; set; }
    }
}
