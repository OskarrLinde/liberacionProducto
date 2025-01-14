using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class TipoCertificadoViewModel : SechToolDistributionBatchVM
    {
        public TipoCertificado TipoCertificado { get; set; }
        public List<TipoCertificado> TipoCertificadoList { get; set; }
        public List<String> SelectedTipoCertificadoFilter { get; set; }
        public List<SelectListItem> TipoCertificadoFilter { get; set; }
    }

    public class TipoCertificado
    {
        public int? iD_TIPO { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoTipoCertificado
    {
        public string iD_TIPO { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
        public string usR_ALTA { get; set; }
        public string usR_MODIFICA { get; set; }
    }
}
