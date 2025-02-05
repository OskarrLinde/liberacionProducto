using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class AnalizadorViewModel : SechToolDistributionBatchVM
    {
        public Analizador Analizador { get; set; }
        public List<Analizador> AnalizadorList { get; set; }
        public List<String> SelectedAnalizadorFilter { get; set; }
        public List<SelectListItem> AnalizadorFilter { get; set; }

        // Productos Lista
        public List<Productos> ProductosList { get; set; }        
        public List<SelectListItem> ProductosFilter { get; set; }

        // messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
        public bool showAll { get; set; }
    }

    public class Analizador
    {
        public int? iD_ANALIZADOR {  get; set; }
        public int? iD_PRODUCTO { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }        
        public string clavE_PALS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoAnalizador
    {
        public string iD_ANALIZADOR { get; set; }
        public string iD_PRODUCTO { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }        
        public string clavE_PALS { get; set; }        
    }
}
