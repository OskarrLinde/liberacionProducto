using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class TransporteViewModel : SechToolDistributionBatchVM
    {
        public Transporte Transporte { get; set; }
        public List<Transporte> TransporteList { get; set; }
        public List<String> SelectedTransporteFilter { get; set; }
        public List<SelectListItem> TransporteFilter { get; set; }

        // Lista Tipo Transporte
        public List<TipoTransporte> TipoTransporteList { get; set; }
        public List<SelectListItem> TipoTransporteFilter { get; set; }

        // Lista Pais
        public List<Paises> PaisesList { get; set; }
        public List<SelectListItem> PaisesFilter { get; set; }

        // Lista Producto
        public List<Productos> ProductosList { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        // Lista Status Pipa
        public List<StatusPipas> StatusPipasList { get; set; }
        public List<SelectListItem> StatusPipasFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class Transporte
    {
        public int? iD_TRANSPORTE { get; set; }
        public int? iD_TIPO_TRANSPORTE { get; set; }
        public string iD_PAIS { get; set; }
        public int? iD_PRODUCTO { get; set; }
        public int? iD_STATUS_PIPA { get; set; }
        public string descripcion { get; set; }
        public string clavE_PALS { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoTransporte
    {
        public string iD_TRANSPORTE { get; set; }
        public string iD_TIPO_TRANSPORTE { get; set; }
        public string iD_PAIS { get; set; }
        public string iD_PRODUCTO { get; set; }
        public string iD_STATUS_PIPA { get; set; }
        public string descripcion { get; set; }
        public string clavE_PALS { get; set; }
        public string iD_STATUS { get; set; }
    }
}
