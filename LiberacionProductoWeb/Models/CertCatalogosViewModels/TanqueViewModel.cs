using LiberacionProductoWeb.Models.RAPModels;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class TanqueViewModel : SechToolDistributionBatchVM
    {
        public Tanque Tanque { get; set; }
        public List<Tanque> TanqueList { get; set; }
        public List<String> SelectedTanqueFilter { get; set; }
        public List<SelectListItem> TanqueFilter { get; set; }

        // Lista Planta
        public List<Plantas> PlantasList { get; set; }
        public List<SelectListItem> PlantasFilter { get; set; }

        // Lista Producto
        public List<Productos> ProductosList { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        // Lista Grado
        public List<Grados> GradosList { get; set; }
        public List<SelectListItem> GradosFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
        public bool showAll { get; internal set; }
    }

    public class Tanque
    {
        public int? iD_TANQUE { get; set; }
        public string descripcion { get; set; }
        public string clavE_PALS { get; set; }
        public int? iD_PLANTA { get; set; }
        public int? iD_PRODUCTO { get; set; }
        public int iD_STATUS { get; set; }        
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoTanque
    {
        public string iD_TANQUE { get; set; }
        public string descripcion { get; set; }
        public string iD_PLANTA { get; set; }
        public string iD_PRODUCTO { get; set; }
        public string iD_STATUS { get; set; }
        public string clavE_PALS { get; set; }
    }
}
