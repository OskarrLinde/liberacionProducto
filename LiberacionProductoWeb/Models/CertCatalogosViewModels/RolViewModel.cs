using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class RolViewModel : SechToolDistributionBatchVM
    {
        public Rol Rol { get; set; }
        public List<Rol> RolList { get; set; }
        public List<String> SelectedRolFilter { get; set; }
        public List<SelectListItem> RolFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class Rol
    {
        public int? iD_ROL { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoRol
    {
        public string iD_ROL { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
    }
}
