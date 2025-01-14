using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class ClientesViewModel : SechToolDistributionBatchVM
    {
        public Clientes Clientes { get; set; }
        public List<Clientes> ClientesList { get; set; }
        public List<String> SelectedClientesFilter { get; set; }
        public List<SelectListItem> ClientesFilter { get; set; }

        // Lista Tanque
        public List<Tanque> TanqueList { get; set; }
        public List<SelectListItem> TanqueFilter { get; set; }

        // Lista Productos
        public List<Productos> ProductosList { get; set; }
        public List<SelectListItem> ProductosFilter { get; set; }

        // Lista Grados
        public List<Grados> GradosList { get; set; }
        public List<SelectListItem> GradosFilter { get; set; }

        // Lista Tipo de Certificado
        public List<TipoCertificado> TipoCertificadoList { get; set; }
        public List<SelectListItem> TipoCertificadoFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class Clientes
    {
        public int? iD_CLIENTE { get; set; }
        public int? iD_CLIENTE_JDE { get; set; }
        public int tanquE_PX { get; set; }
        public int? iD_PRODUCTO { get; set; }
        public int? iD_GRADO { get; set; }
        public int? iD_CERTIFICADO { get; set; }
        public string razoN_SOCIAL { get; set; }
        public string nombre { get; set; }
        public int nO_EMP_VENDEDOR { get; set; }
        public string nombrE_VENDEDOR { get; set; }
        public string negocio { get; set; }
        public string segmento { get; set; }
        public string industria { get; set; }
        public string zona { get; set; }
        public string region { get; set; }
        public string estado { get; set; }
        public string notas { get; set; }
        public string sellos { get; set; }
        public int status { get; set; }
        public int usR_ALTA { get; set; }
        public int usR_MODIFICA { get; set; }
    }

    public class DtoClientes
    {
        public string iD_CLIENTE { get; set; }
        public string iD_CLIENTE_JDE { get; set; }
        public string tanquE_PX { get; set; }
        public string iD_PRODUCTO { get; set; }
        public string iD_GRADO { get; set; }
        public string iD_CERTIFICADO { get; set; }
        public string razoN_SOCIAL { get; set; }
        public string nombre { get; set; }
        public string nO_EMP_VENDEDOR { get; set; }
        public string nombrE_VENDEDOR { get; set; }
        public string negocio { get; set; }
        public string segmento { get; set; }
        public string industria { get; set; }
        public string zona { get; set; }
        public string region { get; set; }
        public string estado { get; set; }
        public string notas { get; set; }
        public string sellos { get; set; }
        public string status { get; set; }
    }
}
