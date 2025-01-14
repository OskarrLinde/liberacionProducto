using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class UnidadMedidaViewModel : SechToolDistributionBatchVM
    {
        public UnidadMedidas UnidadMedidas { get; set; }
        public List<UnidadMedidas> UnidadMedidasList { get; set; }
        public List<String> SelectedUnidadMedidasFilter { get; set; }
        public List<SelectListItem> UnidadMedidasFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }
    public class UnidadMedidas
    {
        public int? iD_UNIDAD_MEDIDA { get; set; }
        public string descripcion { get; set; }
        public string clavE_PALS { get; set; }
        public bool binario { get; set; }
        public int tipo { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }
    public class DtoUnidadMedidas
    {
        public string iD_UNIDAD_MEDIDA { get; set; }
        public string descripcion { get; set; }
        public string clavE_PALS { get; set; }
        public string binario { get; set; }
        public string tipo { get; set; }
        public string iD_STATUS { get; set; }
        public string usR_ALTA { get; set; }
        public string usR_MODIFICA { get; set; }
    }
}
