using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class ProductoGradoViewModel : SechToolDistributionBatchVM
    {
        public ProductoGrado ProductoGrado { get; set; }
        public List<ProductoGrado> ProductoGradoList { get; set; }
        public List<String> SelectedProductoGradoFilter { get; set; }
        public List<SelectListItem> ProductoGradoFilter { get; set; }
        public List<ProductoGrado> ProductoGradoListGroup {  get; set; }
        public List<SelectListItem> ProductoGradoFilterGroup { get; set; }

        // Lista Productos
        public List<Productos> ProductosList { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        // Lista Grados
        public List<Grados> GradosList { get; set; }
        public List<SelectListItem> GradosFilter { get; set; }

        // List Excel Export
        public List<ProductoGrado> ProductoGradoExportExcelList { get; set; }
        public List<SelectListItem> ProductoGradoExportExcelFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
        public bool showAll { get; internal set; }
    }

    public class ProductoGrado
    {
        public int? iD_PRODUCTO_GRADO { get; set; }
        public int? iD_GRADO { get; set; }
        public int? iD_PRODUCTO { get; set; }
        public string clavE_PALS { get; set; }
        public string nombre { get; set; }
        public string nombrE_ESP { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA {  get; set; }
    }

    public class DtoProductoGrado
    {
        public string iD_PRODUCTO_GRADO { get; set; }
        public string iD_GRADO { get; set; }
        public string iD_PRODUCTO { get; set; }
        public string clavE_PALS { get; set; }
        public string nombre { get; set; }
        public string nombrE_ESP { get; set; }
        public string iD_STATUS { get; set; }
    }
}
