
using LiberacionProducto.Entities.Entities.Lotificacion;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.Principal;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Product = LiberacionProductoWeb.Models.CatalogsViewModels.Product;

namespace LiberacionProductoWeb.Models.Lotificacion
{
    public class LotificacionViewModel : SechToolDistributionBatchVM
    {
        public DateTime FechaActual { get; set; }
        public List<CatPlanta> CatPlanta { get; set; }
        public List<CatProducto> CatProducto { get; set; }
        public Plant Plant { get; set; }
        public Product Product { get; set; }
        public List<CAB_EspecificacionProducto> lstEspecificProduct { get; set; }
        public List<CAB_TC_Analizadores> lstAnalizadores { get; set; }
        public int ExternalId { get; set; }
        public string? NombreUsuario { get; set; }

        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }

    }

}
