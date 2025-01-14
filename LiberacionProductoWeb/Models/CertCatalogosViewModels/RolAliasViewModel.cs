using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class RolAliasViewModel : SechToolDistributionBatchVM
    {
        public RolAlias RolAlias { get; set; }
        public List<RolAlias> RolAliasList { get; set; }
        public List<String> SelectedRolAliasFilter { get; set; }
        public List<SelectListItem> RolAliasFilter { get; set; }

        // Lista Rol
        public List<Rol> RolList { get; set; }
        public List<SelectListItem> RolFilter { get; set; }

        // Lista Paises
        public List<Paises> PaisesList { get; set; }
        public List<SelectListItem> PaisesFilter { get; set; }

        // List Excel Export
        public List<RolAlias> RolAliasExportExcelList { get; set; }
        public List<SelectListItem> RolAliasExportExcelFilter { get; set; }

        //messages vieews
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class RolAlias
    {
        public int? iD_ROL_ALIAS { get; set; }
        public int? iD_ROL { get; set; }
        public string iD_PAIS { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoRolAlias
    {
        public string iD_ROL_ALIAS { get; set; }
        public string iD_ROL { get; set; }
        public string iD_PAIS { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
    }
}
