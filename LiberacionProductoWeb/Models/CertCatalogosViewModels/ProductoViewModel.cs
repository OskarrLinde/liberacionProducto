using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class ProductoViewModel : SechToolDistributionBatchVM
    {
        public Productos Productos { get; set; }
        public List<Productos> ProductosList { get; set; }
        public List<String> SelectedProductosFilter { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }
    public class Productos
    {
        public int? iD_PRODUCTO { get; set; }
        public string clavE_PALS { get; set; }
        public string nombre { get; set; }
        public string nombrE_ESP { get; set; }
        public string identificador { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }
    public class DtoProductos
    {
        public string iD_PRODUCTO { get; set; }
        public string clavE_PALS { get; set; }
        public string nombre { get; set; }
        public string nombrE_ESP { get; set; }
        public string iD_STATUS { get; set; }
    }
}
