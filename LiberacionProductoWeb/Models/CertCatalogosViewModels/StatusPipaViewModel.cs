using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class StatusPipaViewModel : SechToolDistributionBatchVM
    {
        public StatusPipas StatusPipas { get; set; }
        public List<StatusPipas> StatusPipasList { get; set; }
        public List<String> SelectedStatusPipasFilter { get; set; }
        public List<SelectListItem> StatusPipasFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
        public bool showAll { get; internal set; }
    }
    public class StatusPipas
    {
        public int? iD_STATUS_PIPA { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }
    public class DtoStatusPipas
    {
        public string iD_STATUS_PIPA { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
        public string usR_ALTA { get; set; }
        public string usR_MODIFICA { get; set; }
    }
}
