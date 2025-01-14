using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class ProductoParametroViewModel : SechToolDistributionBatchVM
    {
        public ProductoParametro ProductoParametro { get; set; }
        public List<ProductoParametro> ProductoParametroList { get; set; }
        public List<String> SelectedProductoParametroFilter { get; set; }
        public List<SelectListItem> ProductoParametroFilter { get; set; }

        // Producto Lista
        public List<Productos> ProductosList { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        // Parametro Lista
        public List<Parametro> ParametroList { get; set; }
        public List<SelectListItem> ParametroFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class ProductoParametro
    {
        public int? iD_PRODUCTO_PARAMETRO { get; set; }
        public int? iD_PARAMETRO { get; set; }
        public int? iD_PRODUCTO { get; set; }
        public string clavE_PALS { get; set; }
        public string nombre { get; set; }
        public string nombrE_ESP { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoProductoParametro
    {
        public string iD_PRODUCTO_PARAMETRO { get; set; }
        public string iD_PARAMETRO { get; set; }
        public string iD_PRODUCTO { get; set; }
        public string clavE_PALS { get; set; }
        public string nombre { get; set; }
        public string nombrE_ESP { get; set; }
        public string iD_STATUS { get; set; }
    }
}
