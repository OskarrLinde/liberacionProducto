using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class GradoViewModel : SechToolDistributionBatchVM
    {
        public Grados Grados { get; set; }
        public List<Grados> GradosList { get; set; }
        public List<String> SelectedGradosFilter { get; set; }
        public List<SelectListItem> GradosFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }
    public class Grados
    {
        public int? iD_GRADO { get; set; }
        public string descripcion { get; set; }
        public int gradO_ESPECIAL { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }
    public class DtoGrados
    {
        public string iD_GRADO { get; set; }
        public string descripcion { get; set; }
        public string gradO_ESPECIAL { get; set; }
        public string iD_STATUS { get; set; }
    }
}
