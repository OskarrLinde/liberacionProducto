using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class PlantaAprobadaViewModel : SechToolDistributionBatchVM
    {
        public PlantaAprobada PlantaAprobada { get; set; }
        public List<PlantaAprobada> PlantaAprobadaList { get; set; }
        public List<String> SelectedPlantaAprobadaFilter { get; set; }
        public List<SelectListItem> PlantaAprobadaFilter { get; set; }
    }

    public class PlantaAprobada
    {
        public int? ID_PLANTA_APROBADA { get; set; }
        public string descripcion { get; set; }
        public int iD_STATUS { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoPlantaAprobada
    {
        public string ID_PLANTA_APROBADA { get; set; }
        public string descripcion { get; set; }
        public string iD_STATUS { get; set; }
        public string usR_ALTA { get; set; }
        public string usR_MODIFICA { get; set; }
    }
}
