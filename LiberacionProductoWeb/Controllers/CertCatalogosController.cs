using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.CertCatalogosViewModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Controllers
{
    public class CertCatalogosController : Controller
    {
        private readonly IAnalyticsCertsService _analyticsCerts;
        private readonly IGeneralCatalogRepository _generalRepository;
        private readonly IFormulaCatalogRepository _formulaRepository;
        private readonly IProductCatalogRepository _productRepository;
        private readonly IStabilityCatalogRepository _stabilityCatalogRepository;
        private readonly IContainerCatalogRepository _containerCatalogRepository;
        private readonly IDispositionCatalogRepository _dispositionCatalogRepository;
        private readonly IReportAuditTrailService _reportAuditTrailService;
        private readonly ILogger<AccessController> _logger;
        IStringLocalizer<Localize.Resource> _resource;
        private AppDbContext _context;
        private Transporte iD_GRADO;
        private readonly int[] plantsByUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrincipalService _principalService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CatalogCertificate _catalogCertificate;

        public CertCatalogosController(ILogger<AccessController> logger, UserManager<ApplicationUser> userManager, IStringLocalizer<Localize.Resource> resource, IConfiguration config, CatalogCertificate catalogCertificate)
        {
            _logger = logger;
            _userManager = userManager;
            _resource = resource;
            _config = config;
            _catalogCertificate = catalogCertificate;
        }

        public IConfiguration _config { get; }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Catalogs()
        {
            Models.ConfigViewModels.ConfiguracionUsuarioVM configuracionUsuario = new Models.ConfigViewModels.ConfiguracionUsuarioVM();

            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorPrincipal"));
                _logger.LogError(ex, "Error.");
            }

            return View(configuracionUsuario);
        }

        #region GetAPI

        public List<Analizador> getAnalizador()
        {
            var _model = new List<Analizador>();
            var _url = _catalogCertificate.urlCatalogs + "Analizador";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Analizador>>(JSONObj["data"]);

            return _model;
        }

        public List<AnalizadorParametros> getAnalizadorParametros()
        {
            var _model = new List<AnalizadorParametros>();
            var _url = _catalogCertificate.urlCatalogs + "AnalizadorParam";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<AnalizadorParametros>>(JSONObj["data"]);

            return _model;
        }

        public List<Clientes> getClientes()
        {
            var _model = new List<Clientes>();
            var _url = _catalogCertificate.urlCatalogs + "Cliente";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Clientes>>(JSONObj["data"]);

            return _model;
        }

        public List<EspecificacionDetalle> getEspecificacionDetalle()
        {
            var _model = new List<EspecificacionDetalle>();
            var _url = _catalogCertificate.urlCatalogs + "EspecificacionDetalle";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<EspecificacionDetalle>>(JSONObj["data"]);

            return _model;
        }

        public List<EspecificacionMaster> getEspecificacionMaster()
        {
            var _model = new List<EspecificacionMaster>();
            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<EspecificacionMaster>>(JSONObj["data"]);

            return _model;
        }

        public List<Grados> getGrado()
        {
            var _model = new List<Grados>();
            var _url = _catalogCertificate.urlCatalogs + "Grado";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Grados>>(JSONObj["data"]);

            return _model;
        }

        public List<Metodo> getMetodo()
        {
            var _model = new List<Metodo>();
            var _url = _catalogCertificate.urlCatalogs + "Metodo";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Metodo>>(JSONObj["data"]);

            return _model;
        }

        public List<Paises> getPaises()
        {
            var _model = new List<Paises>();
            var _url = _catalogCertificate.urlCatalogs + "Paises";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Paises>>(JSONObj["data"]);

            return _model;
        }

        public List<Parametro> getParametro()
        {
            var _model = new List<Parametro>();
            var _url = _catalogCertificate.urlCatalogs + "Parametro";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Parametro>>(JSONObj["data"]);

            return _model;
        }

        public List<Plantas> getPlantas()
        {
            var _model = new List<Plantas>();
            var _url = _catalogCertificate.urlCatalogs + "Planta";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Plantas>>(JSONObj["data"]);

            return _model;
        }

        public List<PlantaProducto> getPlantaProducto()
        {
            var _model = new List<PlantaProducto>();
            var _url = _catalogCertificate.urlCatalogs + "PlantaProducto";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<PlantaProducto>>(JSONObj["data"]);

            return _model;
        }

        public List<Productos> getProductos()
        {
            var _model = new List<Productos>();
            var _url = _catalogCertificate.urlCatalogs + "Producto";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Productos>>(JSONObj["data"]);


            return _model;
        }

        public List<ProductoGrado> getProductoGrado()
        {
            var _model = new List<ProductoGrado>();
            var _url = _catalogCertificate.urlCatalogs + "ProductoGrado";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<ProductoGrado>>(JSONObj["data"]);

            return _model;
        }

        public List<ProductoParametro> getProductoParametros()
        {
            var _model = new List<ProductoParametro>();
            var _url = _catalogCertificate.urlCatalogs + "ProductoParametro";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<ProductoParametro>>(JSONObj["data"]);

            return _model;
        }

        public List<Rol> getRol()
        {
            var _model = new List<Rol>();
            var _url = _catalogCertificate.urlCatalogs + "Rol";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Rol>>(JSONObj["data"]);

            return _model;
        }

        public List<RolAlias> getRolAlias()
        {
            var _model = new List<RolAlias>();
            var _url = _catalogCertificate.urlCatalogs + "RolAlias";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<RolAlias>>(JSONObj["data"]);

            return _model;
        }

        public List<StatusPipas> getStatusPipa()
        {
            var _model = new List<StatusPipas>();
            var _url = _catalogCertificate.urlCatalogs + "StatusPipa";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<StatusPipas>>(JSONObj["data"]);

            return _model;
        }

        public List<Models.CertCatalogosViewModels.Tanque> getTanque()
        {
            var _model = new List<Models.CertCatalogosViewModels.Tanque>();
            var _url = _catalogCertificate.urlCatalogs + "Tanque";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Models.CertCatalogosViewModels.Tanque>>(JSONObj["data"]);

            return _model;
        }

        public List<TipoEspecificacion> getTipoEspecificacion()
        {
            var _model = new List<TipoEspecificacion>();
            var _url = _catalogCertificate.urlCatalogs + "TipoEspecificacion";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<TipoEspecificacion>>(JSONObj["data"]);

            return _model;
        }

        public List<TipoSuministros> getTipoSuministro()
        {
            var _model = new List<TipoSuministros>();
            var _url = _catalogCertificate.urlCatalogs + "TipoSuministro";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<TipoSuministros>>(JSONObj["data"]);


            return _model;
        }

        public List<TipoTransporte> getTipoTransporte()
        {
            var _model = new List<TipoTransporte>();
            var _url = _catalogCertificate.urlCatalogs + "TipoTransporte";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<TipoTransporte>>(JSONObj["data"]);

            return _model;
        }

        public List<Transporte> getTransporte()
        {
            var _model = new List<Transporte>();
            var _url = _catalogCertificate.urlCatalogs + "Transporte";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<Transporte>>(JSONObj["data"]);

            return _model;
        }

        public List<UnidadMedidas> getUnidadMedidas()
        {
            var _model = new List<UnidadMedidas>();
            var _url = _catalogCertificate.urlCatalogs + "UnidadMedida";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<UnidadMedidas>>(JSONObj["data"]);


            return _model;
        }

        public List<FuenteSuministro> getFuenteSuministro()
        {
            var _model = new List<FuenteSuministro>();
            var _url = _catalogCertificate.urlCatalogs + "FuenteSuministro";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<FuenteSuministro>>(JSONObj["data"]);


            return _model;
        }

        public List<PlantaAprobada> getPlantaAprobada()
        {
            var _model = new List<PlantaAprobada>();
            var _url = _catalogCertificate.urlCatalogs + "PlantaAprobada";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<PlantaAprobada>>(JSONObj["data"]);


            return _model;
        }

        public List<TipoCertificado> getTipoCertificado()
        {
            var _model = new List<TipoCertificado>();
            var _url = _catalogCertificate.urlCatalogs + "TipoCertificado";

            var client = new RestClient(_url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);

            _model = JsonConvert.DeserializeObject<List<TipoCertificado>>(JSONObj["data"]);

            return _model;
        }

        #endregion

        #region Analizador

        public IActionResult Analizador()
        {
            var analizador = getAnalizador();
            var producto = getProductos();
            AnalizadorViewModel _analizadorVM = new AnalizadorViewModel();

            _analizadorVM.AnalizadorList = analizador;
            _analizadorVM.AnalizadorFilter = analizador.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_ANALIZADOR.ToString()
                };
            });

            _analizadorVM.ProductosList = producto;
            _analizadorVM.ProductosFilter = producto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            return View(_analizadorVM);
        }

        public IActionResult ClearFiltersAnalizador()
        {
            return RedirectToAction("Analizador");
        }

        public JsonResult SaveOrEditAnalizador([FromBody] DtoAnalizador data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_ANALIZADOR))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && descripcion != null)
                        {
                            Analizador _analizador = new Analizador();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _analizador.iD_STATUS = _resultiDSTATUS;
                            _analizador.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _analizador.descripcion = descripcion;
                            _analizador.clavE_PALS = clavE_PALS;
                            _analizador.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Analizador";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Analizador, Analizador>(_url, _analizador);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Analizador, Analizador>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ANALIZADOR = data.iD_ANALIZADOR; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && descripcion != null)
                        {
                            Analizador _analizador = new Analizador();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _analizador.iD_STATUS = _resultiDSTATUS;
                            _analizador.iD_ANALIZADOR = int.Parse(iD_ANALIZADOR);
                            _analizador.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _analizador.descripcion = descripcion;
                            _analizador.clavE_PALS = data.clavE_PALS;
                            _analizador.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Analizador";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Analizador, Analizador>(_url + "/" + _analizador.iD_ANALIZADOR, _analizador);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Analizador, Analizador>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteAnalizador(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Analizador";
                Analizador model2 = new Analizador();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Analizador, Analizador>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Analizador>(JSONObj["data"]);

                model2.iD_ANALIZADOR = result.iD_ANALIZADOR;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.descripcion = result.descripcion;
                model2.iD_STATUS = 0;
                model2.clavE_PALS = result.clavE_PALS;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Analizador, Analizador>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Analizador, Analizador>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetAnalizadorHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var analizador = getAnalizador();
            var _url = _catalogCertificate.urlCatalogs + "Analizador";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Analizador, Analizador>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Analizador>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_ANALIZADOR' class='form-control' >";

                foreach (var item in analizador)
                {
                    if (entity.iD_ANALIZADOR == item.iD_ANALIZADOR)
                        idTag += "<option value='" + item.iD_ANALIZADOR + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_ANALIZADOR + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_ANALIZADOR + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td><input class='form-control' id='clavE_PALS' type='text'  value='" + entity.clavE_PALS + "'></td>" +
                "<td><input class='form-control' id='iD_PRODUCTO' type='text'  value='" + entity.iD_PRODUCTO + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Analizador Parametros

        public IActionResult AnalizadorParametros()
        {
            var analizadorParametrosExportExcel = getAnalizadorParametros();
            var analizadorParametros = getAnalizadorParametros();
            var analizador = getAnalizador();
            var producto = getProductos();
            var plantas = getPlantas();
            var parametros = getParametro();
            var metodos = getMetodo();

            AnalizadorParametrosViewModel _analizadorParametrosVM = new AnalizadorParametrosViewModel();

            _analizadorParametrosVM.AnalizadorParametrosExportExcelList = analizadorParametrosExportExcel;
            _analizadorParametrosVM.AnalizadorParametrosExportExcelFilter = analizadorParametrosExportExcel.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_ANALIZADOR_PARAM.ToString()
                };
            });

            _analizadorParametrosVM.AnalizadorParametrosList = analizadorParametros;
            _analizadorParametrosVM.AnalizadorParametrosFilter = analizadorParametros.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_ANALIZADOR_PARAM.ToString()
                };
            });

            _analizadorParametrosVM.AnalizadorList = analizador;
            _analizadorParametrosVM.AnalizadorFilter = analizador.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_ANALIZADOR.ToString()
                };
            });

            _analizadorParametrosVM.ProductosList = producto;
            _analizadorParametrosVM.ProductosFilter = producto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _analizadorParametrosVM.PlantasList = plantas;
            _analizadorParametrosVM.PlantasFilter = plantas.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PLANTA.ToString()
                };
            });

            _analizadorParametrosVM.ParametrosList = parametros;
            _analizadorParametrosVM.ParametrosFilter = parametros.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PARAMETRO.ToString()
                };
            });

            _analizadorParametrosVM.MetodoList = metodos;
            _analizadorParametrosVM.MetodoFilter = metodos.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_METODO.ToString()
                };
            });

            return View(_analizadorParametrosVM);
        }

        public IActionResult ClearFiltersAnalizadorParametros()
        {
            return RedirectToAction("AnalizadorParametros");
        }

        public JsonResult SaveOrEditAnalizadorParametros([FromBody] DtoAnalizadorParametros data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_ANALIZADOR_PARAM))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_ANALIZADOR_PARAM = data.iD_ANALIZADOR_PARAM?.Trim().Replace(" ", "").Split(",");
                        var iD_ANALIZADOR = data.iD_ANALIZADOR; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PARAMETRO = data.iD_PARAMETRO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_METODO = data.iD_METODO; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var limitE_INFERIOR = data.limitE_INFERIOR; //?.Trim().Replace(" ", "").Split(",");
                        var leyendA_REPORTE = data.leyendA_REPORTE; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_ANALIZADOR != null && descripcion != null)
                        {
                            AnalizadorParametros _analizadorParametros = new AnalizadorParametros();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _analizadorParametros.iD_STATUS = _resultiDSTATUS;
                            _analizadorParametros.iD_ANALIZADOR = int.Parse(iD_ANALIZADOR);
                            _analizadorParametros.iD_PLANTA = int.Parse(iD_PLANTA);
                            _analizadorParametros.iD_PARAMETRO = int.Parse(iD_PARAMETRO);
                            _analizadorParametros.iD_METODO = int.Parse(iD_METODO);
                            _analizadorParametros.descripcion = descripcion;
                            _analizadorParametros.limitE_INFERIOR = limitE_INFERIOR == string.Empty ? 0.ToString() : limitE_INFERIOR;
                            _analizadorParametros.leyendA_REPORTE = leyendA_REPORTE;
                            _analizadorParametros.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "AnalizadorParam";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<AnalizadorParametros, AnalizadorParametros>(_url, _analizadorParametros);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<AnalizadorParametros, AnalizadorParametros>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ANALIZADOR_PARAM = data.iD_ANALIZADOR_PARAM; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ANALIZADOR = data.iD_ANALIZADOR; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PARAMETRO = data.iD_PARAMETRO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_METODO = data.iD_METODO; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var limitE_INFERIOR = data.limitE_INFERIOR; //?.Trim().Replace(" ", "").Split(",");
                        var leyendA_REPORTE = data.leyendA_REPORTE; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_ANALIZADOR_PARAM != null && descripcion != null)
                        {
                            AnalizadorParametros _analizadorParametros = new AnalizadorParametros();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _analizadorParametros.iD_STATUS = _resultiDSTATUS;
                            _analizadorParametros.iD_ANALIZADOR_PARAM = int.Parse(iD_ANALIZADOR_PARAM);
                            _analizadorParametros.iD_ANALIZADOR = int.Parse(iD_ANALIZADOR);
                            _analizadorParametros.iD_PLANTA = int.Parse(iD_PLANTA);
                            _analizadorParametros.iD_PARAMETRO = int.Parse(iD_PARAMETRO);
                            _analizadorParametros.iD_METODO = int.Parse(iD_METODO);
                            _analizadorParametros.descripcion = descripcion;
                            _analizadorParametros.limitE_INFERIOR = limitE_INFERIOR;
                            _analizadorParametros.leyendA_REPORTE = leyendA_REPORTE;
                            _analizadorParametros.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "AnalizadorParam";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<AnalizadorParametros, AnalizadorParametros>(_url + "/" + _analizadorParametros.iD_ANALIZADOR_PARAM, _analizadorParametros);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<AnalizadorParametros, AnalizadorParametros>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteAnalizadorParametros(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "AnalizadorParam";
                AnalizadorParametros model2 = new AnalizadorParametros();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<AnalizadorParametros, AnalizadorParametros>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<AnalizadorParametros>(JSONObj["data"]);

                model2.iD_ANALIZADOR_PARAM = result.iD_ANALIZADOR_PARAM;
                model2.iD_ANALIZADOR = result.iD_ANALIZADOR;
                model2.iD_PLANTA = result.iD_PLANTA;
                model2.iD_PARAMETRO = result.iD_PARAMETRO;
                model2.iD_METODO = result.iD_METODO;
                model2.descripcion = result.descripcion;
                model2.limitE_INFERIOR = result.limitE_INFERIOR;
                model2.leyendA_REPORTE = result.leyendA_REPORTE;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<AnalizadorParametros, AnalizadorParametros>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<AnalizadorParametros, AnalizadorParametros>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetAnalizadorParametrosHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var analizadorParametros = getAnalizadorParametros();
            var planta = getPlantas();
            var parametro = getParametro();
            var metodo = getMetodo();

            var _url = _catalogCertificate.urlCatalogs + "AnalizadorParam";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<AnalizadorParametros, AnalizadorParametros>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<AnalizadorParametros>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_ANALIZADOR_PARAM' class='form-control' >";

                foreach (var item in analizadorParametros)
                {
                    if (entity.iD_ANALIZADOR_PARAM == item.iD_ANALIZADOR_PARAM)
                        idTag += "<option value='" + item.iD_ANALIZADOR_PARAM + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_ANALIZADOR_PARAM + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var plantaTag = "<select  id='iD_PLANTA' class='form-control' >";

                foreach (var item in planta)
                {
                    if (entity.iD_PLANTA == item.iD_PLANTA)
                        plantaTag += "<option value='" + item.iD_PLANTA + "' selected >" + item.descripcion + " </option>";
                    else
                        plantaTag += "<option value='" + item.iD_PLANTA + "'>" + item.descripcion + " </option>";
                }

                plantaTag += "</select>";

                var parametroTag = "<select  id='iD_PARAMETRO' class='form-control' >";

                foreach (var item in parametro)
                {
                    if (entity.iD_PARAMETRO == item.iD_PARAMETRO)
                        parametroTag += "<option value='" + item.iD_PARAMETRO + "' selected >" + item.descripcion + " </option>";
                    else
                        parametroTag += "<option value='" + item.iD_PARAMETRO + "'>" + item.descripcion + " </option>";
                }

                parametroTag += "</select>";

                var metodoTag = "<select id='iD_METODO' class='form-control' >";

                foreach (var item in metodo)
                {
                    if (entity.iD_METODO == item.iD_METODO)
                        metodoTag += "<option value='" + item.iD_METODO + "' selected >" + item.descripcion + " </option>";
                    else
                        metodoTag += "<option value='" + item.iD_METODO + "'>" + item.descripcion + " </option>";
                }

                metodoTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClickAnalizadorParametros(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_ANALIZADOR_PARAM + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td style='display: none'><input class='form-control' id='iD_ANALIZADOR' type='text' value='" + entity.iD_ANALIZADOR + "'></td>" +
                "<td>" + plantaTag + "</td>" +
                "<td>" + parametroTag + "</td>" +
                "<td>" + metodoTag + "</td>" +
                "<td><input class='form-control' id='limitE_INFERIOR' type='text'  value='" + entity.limitE_INFERIOR + "'></td>" +
                "<td><input class='form-control' id='leyendA_REPORTE' type='text'  value='" + entity.leyendA_REPORTE + "'></td>" +
                //"<td><input class='form-control' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        public JsonResult GetAnalyzerParameterById(string Id)
        {
            String response = String.Empty;

            var analizadorParametros = getAnalizadorParametros();
            var planta = getPlantas();
            var parametro = getParametro();
            var metodo = getMetodo();

            List<AnalizadorParametros> _lstAnalyzerParameter = new List<AnalizadorParametros>();
            List<Plantas> _lstPlants = new List<Plantas>();
            List<Parametro> _lstParameter = new List<Parametro>();
            List<Metodo> _lstMethod = new List<Metodo>();

            if (!string.IsNullOrEmpty(Id))
                _lstAnalyzerParameter = analizadorParametros.Where(x => x.iD_ANALIZADOR == int.Parse(Id)).ToList();
            else
                _lstAnalyzerParameter = new List<AnalizadorParametros>();

            if (planta != null && planta.Any())
                _lstPlants = planta;
            else
                _lstPlants = new List<Plantas>();

            if (parametro != null && parametro.Any())
                _lstParameter = parametro;
            else
                _lstParameter = new List<Parametro>();

            if (metodo != null && metodo.Any())
                _lstMethod = metodo;
            else
                _lstMethod = new List<Metodo>();

            var idTag = "<select id='iD_PRODUCTO_GRADO' class='form-control'>";

            foreach (var item in _lstAnalyzerParameter)
            {
                if (item.iD_ANALIZADOR_PARAM == item.iD_ANALIZADOR_PARAM)
                    idTag += "<option value='" + item.iD_ANALIZADOR_PARAM + "' selected >" + item.descripcion + " </option>";
                else
                    idTag += "<option value='" + item.iD_ANALIZADOR_PARAM + "'>" + item.descripcion + " </option>";

                idTag += "</select>";

                //var estatusTag = "<select id='iD_STATUS' class='form-control'>";

                //if (item.iD_STATUS == 1)
                //{
                //    estatusTag += "<option value='true' selected >Activo</option>";
                //    estatusTag += "<option value='false'>Inactivo</option>";
                //}
                //else
                //{
                //    estatusTag += "<option value='false' selected >Inactivo</option>";
                //    estatusTag += "<option value='true'>Activo</option>";
                //}

                //estatusTag += "</select>";

                string _estatus = string.Empty;

                if (item.iD_STATUS == 1)
                    _estatus = "Activo";
                else
                    _estatus = "Inactivo";

                string _descripcionPlanta = string.Empty;

                if (item.iD_PLANTA > 0)
                    _descripcionPlanta = _lstPlants.FirstOrDefault(x => x.iD_PLANTA == item.iD_PLANTA).descripcion;
                else
                    _descripcionPlanta = string.Empty;

                string _descripcionParametro = string.Empty;

                if (item.iD_PARAMETRO > 0)
                    _descripcionParametro = _lstParameter.FirstOrDefault(x => x.iD_PARAMETRO == item.iD_PARAMETRO).descripcion;
                else
                    _descripcionParametro = string.Empty;

                string _descripcionMetodo = string.Empty;

                if (item.iD_METODO > 0)
                    _descripcionMetodo = _lstMethod.FirstOrDefault(x => x.iD_METODO == item.iD_METODO).descripcion;
                else
                    _descripcionMetodo = string.Empty;

                response +=
                "<tr>" +
                    "<td></td>" +
                    "<td>" + _descripcionPlanta + "</td>" +
                    "<td>" + _descripcionParametro + "</td>" +
                    "<td>" + _descripcionMetodo + "</td>" +
                    "<td>" + item.limitE_INFERIOR + "</td>" +
                    "<td>" + item.leyendA_REPORTE + "</td>" +
                    "<td>" + _estatus + "</td>" +
                    "<td>" +
                        "<a href='javascript:void(0)' onclick='deleteOnClickAnalizadorParametros(this); return false;' data-id='" + item.iD_ANALIZADOR_PARAM + "' class='btn btn-default btn-xs'>" +
                            "<i class='fa fa-trash-alt'></i>" +
                        "</a>" +
                        "<a href='javascript:void(0)' onclick='editOnClickAnalizadorParametros(this);return false;' id='editData' class='btn btn-default btn-xs' data-id='" + item.iD_ANALIZADOR_PARAM + "'>" +
                            "<i class='fa fa-edit'></i>" +
                        "</a>" +
                    //"<a href='javascript:void(0)' onclick='refresh();return false;' class='btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                    //"<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + item.iD_GRADO + "'><i class='fa fa-save'></i></a>" +
                    "</td>" +
                "</tr>";
            }

            String _tableHead = String.Empty;

            _tableHead +=
                                    //"<div id='content' class='content'>" +
                                    //    "<div class='section-container section-with-top-border p-b-5'>" +
                                    //        "<div class='row'>" +
                                    //            "<div class='panel panel-primary'>" +
                                    //                "<div class='panel-heading'>" +
                                    //                    "<div class='btn-group pull-left'>" +
                                    //"<a href='javascript:void(0)' onclick='addEntryRolAlias(this); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                    //"<i class='fa fa-plus'></i> Agregar Registro" +
                                    //"</a>" +
                                    //"<a href='javascript:;' class='btn btn-white m-r-5' id='ExporttoExcel'>" +
                                    //    "<i class='fa fa-file-excel'></i> Exportar a excel" +
                                    //"</a>" +
                                    //"</div>" +
                                    //"<div class='btn-group pull-right'>" +
                                    //    "<a href='javascript:;' class='btn btn-white m-r-5' id='ClearFilters'>" +
                                    //        "<i class='fa fa-eraser'></i>Limpiar filtros" +
                                    //    "</a>" +
                                    //"</div>" +
                                    //"<h4 class='panel-title'>" +
                                    //"&nbsp;" +
                                    //"</h4>" +
                                    //"</div>" +

                                    //"<!-- Mensaje de error -->" +
                                    //"<div class='col-md-12' id='message'></div>" +

                                    //"<div class='panel-body'>" +
                                    //    "<div>" +
                                    "<table id='data-table-analizador-parametros-" + Id + "' class='table table-bordered table-hover'>" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<th class='no-sort' style='width:1%'></th>" +
                                                "<th style='white-space: nowrap'>Planta</th>" +
                                                "<th style='white-space: nowrap'>Parámetro</th>" +
                                                "<th style='white-space: nowrap'>Método</th>" +
                                                "<th style='white-space: nowrap'>Límite Inferior</th>" +
                                                "<th style='white-space: nowrap'>Leyenda Reporte</th>" +
                                                "<th style='white-space: nowrap'>Estatus</th>" +
                                                "<th style='white-space: nowrap; width: 1%'>" +
                                                    "<a href='javascript:void(0)' onclick='addEntryAnalizadorParametro(this, " + Id + "); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                                        "<i class='fa fa-plus'></i>" +
                                                    "</a>" +
                                                "</th> " +
                                            "</tr>" +
                                        "</thead>";

            String _tableFoot = String.Empty;

            _tableFoot +=
                                    "</table>"; // +
            //                    "</div>" +
            //                "</div>" +
            //            "</div>" +
            //        "</div>" +
            //    "</div>" +
            //"</div";

            return Json(new { Result = "Ok", Html = _tableHead + "<tbody>" + response + "</tbody>" + _tableFoot });
        }

        #endregion

        #region Clientes

        public IActionResult Clientes()
        {
            var clientes = getClientes();
            var tanque = getTanque();
            var productos = getProductos();
            var grados = getGrado();
            var tipoCertificado = getTipoCertificado();

            ClientesViewModel _clientesVM = new ClientesViewModel();

            _clientesVM.ClientesList = clientes;
            _clientesVM.ClientesFilter = clientes.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_CLIENTE.ToString()
                };
            });

            _clientesVM.TanqueList = tanque;
            _clientesVM.TanqueFilter = tanque.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TANQUE.ToString()
                };
            });

            _clientesVM.ProductosList = productos;
            _clientesVM.ProductosFilter = productos.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _clientesVM.GradosList = grados;
            _clientesVM.GradosFilter = grados.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_GRADO.ToString()
                };
            });

            _clientesVM.TipoCertificadoList = tipoCertificado;
            _clientesVM.TipoCertificadoFilter = tipoCertificado.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO.ToString()
                };
            });

            return View(_clientesVM);
        }

        public IActionResult ClearFiltersClientes()
        {
            return RedirectToAction("Clientes");
        }

        public JsonResult SaveOrEditClientes([FromBody] DtoClientes data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_CLIENTE))
                    {
                        var iD_CLIENTE_JDE = data.iD_CLIENTE_JDE;
                        var tanquE_PX = data.tanquE_PX;
                        var iD_PRODUCTO = data.iD_PRODUCTO;
                        var iD_GRADO = data.iD_GRADO;
                        var iD_CERTIFICADO = data.iD_CERTIFICADO;
                        var razoN_SOCIAL = data.razoN_SOCIAL;
                        var nombre = data.nombre;
                        var nO_EMP_VENDEDOR = data.nO_EMP_VENDEDOR;
                        var nombrE_VENDEDOR = data.nombrE_VENDEDOR;
                        var negocio = data.negocio;
                        var segmento = data.segmento;
                        var industria = data.industria;
                        var zona = data.zona;
                        var region = data.region;
                        var estado = data.estado;
                        var notas = data.notas;
                        var sellos = data.sellos;
                        var iD_STATUS = data.status;

                        if (iD_CLIENTE_JDE != null && tanquE_PX != null && iD_PRODUCTO != null && iD_GRADO != null && iD_CERTIFICADO != null)
                        {
                            Clientes _clientes = new Clientes();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "1")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _clientes.iD_CLIENTE_JDE = int.Parse(iD_CLIENTE_JDE);
                            _clientes.tanquE_PX = int.Parse(tanquE_PX);
                            _clientes.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _clientes.iD_GRADO = int.Parse(iD_GRADO);
                            _clientes.iD_CERTIFICADO = int.Parse(iD_CERTIFICADO);
                            _clientes.razoN_SOCIAL = razoN_SOCIAL;
                            _clientes.nombre = nombre;
                            _clientes.nO_EMP_VENDEDOR = int.Parse(nO_EMP_VENDEDOR);
                            _clientes.nombrE_VENDEDOR = nombrE_VENDEDOR;
                            _clientes.negocio = negocio;
                            _clientes.segmento = segmento;
                            _clientes.industria = industria;
                            _clientes.zona = zona;
                            _clientes.region = region;
                            _clientes.estado = estado;
                            _clientes.notas = notas;
                            _clientes.sellos = sellos;
                            _clientes.status = _resultiDSTATUS;
                            _clientes.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Cliente";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Clientes, Clientes>(_url, _clientes);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Clientes, Clientes>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_CLIENTE = data.iD_CLIENTE;
                        var iD_CLIENTE_JDE = data.iD_CLIENTE_JDE;
                        var tanquE_PX = data.tanquE_PX;
                        var iD_PRODUCTO = data.iD_PRODUCTO;
                        var iD_GRADO = data.iD_GRADO;
                        var iD_CERTIFICADO = data.iD_CERTIFICADO;
                        var razoN_SOCIAL = data.razoN_SOCIAL;
                        var nombre = data.nombre;
                        var nO_EMP_VENDEDOR = data.nO_EMP_VENDEDOR;
                        var nombrE_VENDEDOR = data.nombrE_VENDEDOR;
                        var negocio = data.negocio;
                        var segmento = data.segmento;
                        var industria = data.industria;
                        var zona = data.zona;
                        var region = data.region;
                        var estado = data.estado;
                        var notas = data.notas;
                        var sellos = data.sellos;
                        var iD_STATUS = data.status;

                        if (iD_CLIENTE != null && iD_CLIENTE_JDE != null && tanquE_PX != null && iD_PRODUCTO != null && iD_GRADO != null && iD_CERTIFICADO != null)
                        {
                            Clientes _clientes = new Clientes();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _clientes.iD_CLIENTE = int.Parse(iD_CLIENTE);
                            _clientes.iD_CLIENTE_JDE = int.Parse(iD_CLIENTE_JDE);
                            _clientes.tanquE_PX = int.Parse(tanquE_PX);
                            _clientes.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _clientes.iD_GRADO = int.Parse(iD_GRADO);
                            _clientes.iD_CERTIFICADO = int.Parse(iD_CERTIFICADO);
                            _clientes.razoN_SOCIAL = razoN_SOCIAL;
                            _clientes.nombre = nombre;
                            _clientes.nO_EMP_VENDEDOR = int.Parse(nO_EMP_VENDEDOR);
                            _clientes.nombrE_VENDEDOR = nombrE_VENDEDOR;
                            _clientes.negocio = negocio;
                            _clientes.segmento = segmento;
                            _clientes.industria = industria;
                            _clientes.zona = zona;
                            _clientes.region = region;
                            _clientes.estado = estado;
                            _clientes.notas = notas;
                            _clientes.sellos = sellos;
                            _clientes.status = _resultiDSTATUS;
                            _clientes.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Cliente";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Clientes, Clientes>(_url + "/" + _clientes.iD_CLIENTE, _clientes);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Clientes, Clientes>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteClientes(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Cliente";
                Clientes model2 = new Clientes();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Clientes, Clientes>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Clientes>(JSONObj["data"]);

                model2.iD_CLIENTE = result.iD_CLIENTE;
                model2.iD_CLIENTE_JDE = result.iD_CLIENTE_JDE;
                model2.tanquE_PX = result.tanquE_PX;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.iD_GRADO = result.iD_GRADO;
                model2.iD_CERTIFICADO = result.iD_CERTIFICADO;
                model2.razoN_SOCIAL = result.razoN_SOCIAL;
                model2.nombre = result.nombre;
                model2.nO_EMP_VENDEDOR = result.nO_EMP_VENDEDOR;
                model2.nombrE_VENDEDOR = result.nombrE_VENDEDOR;
                model2.negocio = result.negocio;
                model2.segmento = result.segmento;
                model2.industria = result.industria;
                model2.zona = result.zona;
                model2.region = result.region;
                model2.estado = result.estado;
                model2.notas = result.notas;
                model2.sellos = result.sellos;
                model2.status = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Clientes, Clientes>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Clientes, Clientes>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetClientesHTMLTagsById(string Id)
        {
            String response = String.Empty;
            var clientes = getClientes();
            var tanks = getTanque();
            var products = getProductos();
            var grade = getGrado();
            var typeCertificate = getTipoCertificado();
            var _url = _catalogCertificate.urlCatalogs + "Cliente";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Clientes, Clientes>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Clientes>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_CLIENTE' class='form-control' >";

                foreach (var item in clientes)
                {
                    if (entity.iD_CLIENTE == item.iD_CLIENTE)
                        idTag += "<option value='" + item.iD_CLIENTE + "' selected >" + item.nombre + " </option>";
                    else
                        idTag += "<option value='" + item.iD_CLIENTE + "'>" + item.nombre + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='status' class='form-control'>";

                if (entity.status == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var tanqueTag = "<select  id='tanquE_PX' class='form-control'>";

                foreach (var item in tanks)
                {
                    if (entity.tanquE_PX == item.iD_TANQUE)
                        tanqueTag += "<option value='" + item.iD_TANQUE + "' selected >" + item.descripcion + " </option>";
                    else
                        tanqueTag += "<option value='" + item.iD_TANQUE + "'>" + item.descripcion + " </option>";
                }

                tanqueTag += "</select>";

                var productoTag = "<select  id='iD_PRODUCTO' class='form-control'>";

                foreach (var item in products)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        productoTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        productoTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                productoTag += "</select>";

                var gradoTag = "<select  id='iD_GRADO' class='form-control'>";

                foreach (var item in grade)
                {
                    if (entity.iD_GRADO == item.iD_GRADO)
                        gradoTag += "<option value='" + item.iD_GRADO + "' selected >" + item.descripcion + " </option>";
                    else
                        gradoTag += "<option value='" + item.iD_GRADO + "'>" + item.descripcion + " </option>";
                }

                gradoTag += "</select>";

                var certificadoTag = "<select  id='iD_CERTIFICADO' class='form-control'>";

                foreach (var item in typeCertificate)
                {
                    if (entity.iD_CERTIFICADO == item.iD_TIPO)
                        certificadoTag += "<option value='" + item.iD_TIPO + "' selected >" + item.descripcion + " </option>";
                    else
                        certificadoTag += "<option value='" + item.iD_TIPO + "'>" + item.descripcion + " </option>";
                }

                certificadoTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_CLIENTE + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' id='iD_CLIENTE_JDE' type='text' value='" + entity.iD_CLIENTE_JDE + "'></td>" +
                "<td>" + tanqueTag + "</td>" +
                "<td>" + productoTag + "</td>" +
                "<td>" + gradoTag + "</td>" +
                "<td>" + certificadoTag + "</td>" +
                "<td><input class='form-control' id='razoN_SOCIAL' type='text' value='" + entity.razoN_SOCIAL + "'></td>" +
                "<td><input class='form-control' id='nombre' type='text' value='" + entity.nombre + "'></td>" +
                "<td><input class='form-control' id='nO_EMP_VENDEDOR' type='text' value='" + entity.nO_EMP_VENDEDOR + "'></td>" +
                "<td><input class='form-control' id='nombrE_VENDEDOR' type='text' value='" + entity.nombrE_VENDEDOR + "'></td>" +
                "<td><input class='form-control' id='negocio' type='text' value='" + entity.negocio + "'></td>" +
                "<td><input class='form-control' id='segmento' type='text' value='" + entity.segmento + "'></td>" +
                "<td><input class='form-control' id='industria' type='text' value='" + entity.industria + "'></td>" +
                "<td><input class='form-control' id='zona' type='text' value='" + entity.zona + "'></td>" +
                "<td><input class='form-control' id='region' type='text' value='" + entity.region + "'></td>" +
                "<td><input class='form-control' id='estado' type='text' value='" + entity.estado + "'></td>" +
                "<td><input class='form-control' id='notas' type='text' value='" + entity.notas + "'></td>" +
                "<td><input class='form-control' id='sellos' type='text' value='" + entity.sellos + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Especificacion Detalle

        public IActionResult EspecificacionDetalle()
        {
            var especificacionDetalle = getEspecificacionDetalle();
            var parametros = getParametro();
            var unidadMedida = getUnidadMedidas();

            EspecificacionDetalleViewModel _especificacionDetalleVM = new EspecificacionDetalleViewModel();

            _especificacionDetalleVM.EspecificacionDetalleList = especificacionDetalle;
            _especificacionDetalleVM.EspecificacionDetalleFilter = especificacionDetalle.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.observaciones,
                    Value = a.iD_ESPECIFICACION.ToString()
                };
            });

            _especificacionDetalleVM.ParametrosList = parametros;
            _especificacionDetalleVM.ParametrosFilter = parametros.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PARAMETRO.ToString()
                };
            });

            _especificacionDetalleVM.UnidadMedidaList = unidadMedida;
            _especificacionDetalleVM.UnidadMedidaFilter = unidadMedida.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_UNIDAD_MEDIDA.ToString()
                };
            });

            return View(_especificacionDetalleVM);
        }

        public IActionResult ClearFiltersEspecificacionDetalle()
        {
            return RedirectToAction("EspecificacionDetalle");
        }

        public JsonResult SaveOrEditEspecificacionDetalle([FromBody] DtoEspecificacionDetalle data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.id))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var id = data.id; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ESPECIFICACION = data.iD_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PARAMETRO = data.iD_PARAMETRO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_UNIDAD_MEDIDA = data.iD_UNIDAD_MEDIDA; //?.Trim().Replace(" ", "").Split(",");
                        var valoR_LIMITE_SUP = data.valoR_LIMITE_SUP; //?.Trim().Replace(" ", "").Split(",");
                        var valoR_LIMITE_INF = data.valoR_LIMITE_INF; //?.Trim().Replace(" ", "").Split(",");
                        var observaciones = data.observaciones; //?.Trim().Replace(" ", "").Split(",");
                        var orden = data.orden; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_ESPECIFICACION != null && observaciones != null)
                        {
                            EspecificacionDetalle _especificacionDetalle = new EspecificacionDetalle();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _especificacionDetalle.iD_STATUS = _resultiDSTATUS;
                            _especificacionDetalle.iD_ESPECIFICACION = int.Parse(iD_ESPECIFICACION);
                            _especificacionDetalle.iD_PARAMETRO = int.Parse(iD_PARAMETRO);
                            _especificacionDetalle.iD_UNIDAD_MEDIDA = int.Parse(iD_UNIDAD_MEDIDA);
                            _especificacionDetalle.valoR_LIMITE_SUP = decimal.Parse(valoR_LIMITE_SUP, CultureInfo.InvariantCulture);
                            _especificacionDetalle.valoR_LIMITE_INF = decimal.Parse(valoR_LIMITE_INF, CultureInfo.InvariantCulture);
                            _especificacionDetalle.observaciones = observaciones;
                            _especificacionDetalle.orden = int.Parse(orden);
                            _especificacionDetalle.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "EspecificacionDetalle";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<EspecificacionDetalle, EspecificacionDetalle>(_url, _especificacionDetalle);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<EspecificacionDetalle, EspecificacionDetalle>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                //ViewBag.Error = "true";
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var id = data.id; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ESPECIFICACION = data.iD_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PARAMETRO = data.iD_PARAMETRO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_UNIDAD_MEDIDA = data.iD_UNIDAD_MEDIDA; //?.Trim().Replace(" ", "").Split(",");
                        var valoR_LIMITE_SUP = data.valoR_LIMITE_SUP; //?.Trim().Replace(" ", "").Split(",");
                        var valoR_LIMITE_INF = data.valoR_LIMITE_INF; //?.Trim().Replace(" ", "").Split(",");
                        var observaciones = data.observaciones; //?.Trim().Replace(" ", "").Split(",");
                        var orden = data.orden; //?.Trim().Replace(" ", "").Split(",");

                        if (id != null && iD_ESPECIFICACION != null && observaciones != null)
                        {
                            EspecificacionDetalle _especificacionDetalle = new EspecificacionDetalle();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _especificacionDetalle.iD_STATUS = _resultiDSTATUS;
                            _especificacionDetalle.id = int.Parse(id);
                            _especificacionDetalle.iD_ESPECIFICACION = int.Parse(iD_ESPECIFICACION);
                            _especificacionDetalle.iD_PARAMETRO = int.Parse(iD_PARAMETRO);
                            _especificacionDetalle.iD_UNIDAD_MEDIDA = int.Parse(iD_UNIDAD_MEDIDA);
                            _especificacionDetalle.valoR_LIMITE_SUP = decimal.Parse(valoR_LIMITE_SUP, CultureInfo.InvariantCulture);
                            _especificacionDetalle.valoR_LIMITE_INF = decimal.Parse(valoR_LIMITE_INF, CultureInfo.InvariantCulture);
                            _especificacionDetalle.observaciones = observaciones;
                            _especificacionDetalle.orden = int.Parse(orden);
                            _especificacionDetalle.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "EspecificacionDetalle";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<EspecificacionDetalle, EspecificacionDetalle>(_url + "/" + _especificacionDetalle.id, _especificacionDetalle);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<EspecificacionDetalle, EspecificacionDetalle>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteEspecificacionDetalle(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "EspecificacionDetalle";
                EspecificacionDetalle model2 = new EspecificacionDetalle();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<EspecificacionDetalle, EspecificacionDetalle>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<EspecificacionDetalle>(JSONObj["data"]);

                model2.iD_ESPECIFICACION = result.iD_ESPECIFICACION;
                model2.iD_PARAMETRO = result.iD_PARAMETRO;
                model2.iD_UNIDAD_MEDIDA = result.iD_UNIDAD_MEDIDA;
                model2.valoR_LIMITE_SUP = result.valoR_LIMITE_SUP;
                model2.valoR_LIMITE_INF = result.valoR_LIMITE_INF;
                model2.observaciones = result.observaciones;
                model2.iD_STATUS = 0;
                model2.orden = result.orden;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<EspecificacionDetalle, EspecificacionDetalle>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<EspecificacionDetalle, EspecificacionDetalle>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetEspecificacionDetalleHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var especificacionDetalle = getEspecificacionDetalle();
            var parameter = getParametro();
            var unitMeasurement = getUnidadMedidas();
            var _url = _catalogCertificate.urlCatalogs + "EspecificacionDetalle";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<EspecificacionDetalle, EspecificacionDetalle>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<EspecificacionDetalle>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_ESPECIFICACION' class='form-control' >";

                foreach (var item in especificacionDetalle)
                {
                    if (entity.iD_ESPECIFICACION == item.iD_ESPECIFICACION)
                        idTag += "<option value='" + item.iD_ESPECIFICACION + "' selected >" + item.observaciones + " </option>";
                    else
                        idTag += "<option value='" + item.iD_ESPECIFICACION + "'>" + item.observaciones + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var parametroTag = "<select  id='iD_PARAMETRO' class='form-control'>";

                foreach (var item in parameter)
                {
                    if (entity.iD_PARAMETRO == item.iD_PARAMETRO)
                        parametroTag += "<option value='" + item.iD_PARAMETRO + "' selected >" + item.descripcion + " </option>";
                    else
                        parametroTag += "<option value='" + item.iD_PARAMETRO + "'>" + item.descripcion + " </option>";
                }

                parametroTag += "</select>";

                var unidadMedidaTag = "<select  id='iD_UNIDAD_MEDIDA' class='form-control'>";

                foreach (var item in unitMeasurement)
                {
                    if (entity.iD_UNIDAD_MEDIDA == item.iD_UNIDAD_MEDIDA)
                        unidadMedidaTag += "<option value='" + item.iD_UNIDAD_MEDIDA + "' selected >" + item.descripcion + " </option>";
                    else
                        unidadMedidaTag += "<option value='" + item.iD_UNIDAD_MEDIDA + "'>" + item.descripcion + " </option>";
                }

                unidadMedidaTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClickEspecificacionDetalle(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.id + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td style='display: none'><input class='form-control' id='iD_ESPECIFICACION' type='text'  value='" + entity.iD_ESPECIFICACION + "'></td>" +
                "<td>" + parametroTag + "</td>" +
                "<td>" + unidadMedidaTag + "</td>" +
                "<td><input class='form-control' id='valoR_LIMITE_SUP' type='text'  value='" + entity.valoR_LIMITE_SUP + "'></td>" +
                "<td><input class='form-control' id='valoR_LIMITE_INF' type='text'  value='" + entity.valoR_LIMITE_INF + "'></td>" +
                "<td><input class='form-control' id='observaciones' type='text'  value='" + entity.observaciones + "'></td>" +
                "<td><input class='form-control' id='orden' type='text'  value='" + entity.orden + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Especificacion Master

        public IActionResult EspecificacionMaster()
        {
            var especificacionDetalleExportExcel = getEspecificacionDetalle();
            var especificacionMasterExportExcel = getEspecificacionMaster();
            var especificacionMaster = getEspecificacionMaster();
            var plantas = getPlantas();
            var clientes = getClientes();
            var productos = getProductos();
            var grados = getGrado();
            var tipoEspecificacion = getTipoEspecificacion();
            var parametro = getParametro();
            var unidadMedida = getUnidadMedidas();

            EspecificacionMasterViewModel _especificacionMasterVM = new EspecificacionMasterViewModel();

            //_especificacionMasterVM.EspecificacionMasterList = especificacionMaster;
            //_especificacionMasterVM.EspecificacionMasterFilter = especificacionMaster.ConvertAll(a =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = a.tanquE_PX.ToString(), // Por definir
            //        Value = a.iD_ESPECIFICACION.ToString()
            //    };
            //});

            List<EspecificacionMasterExportExcel> lstSpecificationMasterExportExcels = new List<EspecificacionMasterExportExcel>();

            lstSpecificationMasterExportExcels = (from masters in especificacionMasterExportExcel
                                                  join details in especificacionDetalleExportExcel
                                                  on masters.iD_ESPECIFICACION equals details.iD_ESPECIFICACION // join on some property                                                  
                                                  into joinedList
                                                  from sub in joinedList.DefaultIfEmpty()
                                                  where masters.iD_TIPO_ESPECIFICACION.Equals(1)
                                                  select new EspecificacionMasterExportExcel
                                                  {
                                                      iD_PLANTA = masters.iD_PLANTA,
                                                      iD_PRODUCTO = masters.iD_PRODUCTO,
                                                      observaciones = masters.observaciones,
                                                      iD_STATUS = masters.iD_STATUS,
                                                      iD_PARAMETRO = sub == null ? 0 : sub.iD_PARAMETRO,
                                                      iD_UNIDAD_MEDIDA = sub == null ? 0 : sub.iD_UNIDAD_MEDIDA,
                                                      valoR_LIMITE_SUP = sub == null ? 0 : sub.valoR_LIMITE_SUP,
                                                      valoR_LIMITE_INF = sub == null ? 0 : sub.valoR_LIMITE_INF,
                                                      observaciones2 = sub == null ? string.Empty : sub.observaciones,
                                                      orden = sub == null ? 0 : sub.orden
                                                  }).ToList();

            _especificacionMasterVM.EspecificacionMasterExportExcelList = lstSpecificationMasterExportExcels;
            //_especificacionMasterVM.EspecificacionMasterExportExcelFilter = lstSpecificationMasterExportExcels.ConvertAll(a =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = a.descripcion.ToString(),
            //        Value = a.iD_PLANTA.ToString()
            //    };
            //});

            _especificacionMasterVM.EspecificacionMasterList = especificacionMaster.Where(x => x.iD_TIPO_ESPECIFICACION == 1).GroupBy(c => new
            {
                c.iD_ESPECIFICACION,
                c.iD_PLANTA,
                c.iD_PRODUCTO,
                c.observaciones,
                c.iD_STATUS
            })
            .Select(gcs => new EspecificacionMaster()
            {
                iD_ESPECIFICACION = gcs.Key.iD_ESPECIFICACION,
                iD_PLANTA = gcs.Key.iD_PLANTA,
                iD_PRODUCTO = gcs.Key.iD_PRODUCTO,
                observaciones = gcs.Key.observaciones,
                iD_STATUS = gcs.Key.iD_STATUS
            }).ToList();

            _especificacionMasterVM.EspecificacionMasterFilter = especificacionMaster.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.iD_ESPECIFICACION.ToString(), // Por definir
                    Value = a.iD_ESPECIFICACION.ToString()
                };
            });

            _especificacionMasterVM.PlantasList = plantas;
            _especificacionMasterVM.PlantasFilter = plantas.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PLANTA.ToString()
                };
            });

            _especificacionMasterVM.ClientesList = clientes;
            _especificacionMasterVM.ClientesFilter = clientes.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_CLIENTE.ToString()
                };
            });

            _especificacionMasterVM.ProductosList = productos;
            _especificacionMasterVM.ProductosFilter = productos.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _especificacionMasterVM.GradosList = grados;
            _especificacionMasterVM.GradosFilter = grados.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_GRADO.ToString()
                };
            });

            _especificacionMasterVM.TipoEspecificacionList = tipoEspecificacion;
            _especificacionMasterVM.TipoEspecificacionFilter = tipoEspecificacion.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO_ESPECIFICACION.ToString()
                };
            });

            _especificacionMasterVM.ParametroList = parametro;
            _especificacionMasterVM.ParametroFilter = parametro.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PARAMETRO.ToString()
                };
            });

            _especificacionMasterVM.UnidadMedidasList = unidadMedida;
            _especificacionMasterVM.UnidadMedidasFilter = unidadMedida.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_UNIDAD_MEDIDA.ToString()
                };
            });

            return View(_especificacionMasterVM);
        }

        public IActionResult ClearFiltersEspecificacionMaster()
        {
            return RedirectToAction("EspecificacionMaster");
        }

        public JsonResult SaveOrEditEspecificacionMaster([FromBody] DtoEspecificacionMaster data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_ESPECIFICACION))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_ESPECIFICACION = data.iD_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_CLIENTE = data.iD_CLIENTE; //?.Trim().Replace(" ", "").Split(",");
                        var tanquE_PX = data.tanquE_PX; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_ESPECIFICACION = data.iD_TIPO_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var observaciones = data.observaciones; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && observaciones != null)
                        {
                            EspecificacionMaster _especificacionMaster = new EspecificacionMaster();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _especificacionMaster.iD_STATUS = _resultiDSTATUS;
                            //_especificacionMaster.iD_ESPECIFICACION = int.Parse(iD_ESPECIFICACION);
                            _especificacionMaster.iD_PLANTA = int.Parse(iD_PLANTA);
                            _especificacionMaster.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _especificacionMaster.iD_CLIENTE = 1; // int.Parse(iD_CLIENTE);
                            _especificacionMaster.tanquE_PX = 0; // int.Parse(tanquE_PX);
                            _especificacionMaster.iD_GRADO = 1; // int.Parse(iD_GRADO);
                            _especificacionMaster.iD_TIPO_ESPECIFICACION = 1; // int.Parse(iD_TIPO_ESPECIFICACION);
                            _especificacionMaster.observaciones = observaciones;
                            _especificacionMaster.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url, _especificacionMaster);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ESPECIFICACION = data.iD_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_CLIENTE = data.iD_CLIENTE; //?.Trim().Replace(" ", "").Split(",");
                        var tanquE_PX = data.tanquE_PX; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_ESPECIFICACION = data.iD_TIPO_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var observaciones = data.observaciones; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && observaciones != null)
                        {
                            EspecificacionMaster _especificacionMaster = new EspecificacionMaster();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";

                            RestGenerico _restGeneric = new RestGenerico();
                            var _resultApi = _restGeneric.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + iD_ESPECIFICACION);
                            JsonDeserializer _jsonDeserialer = new JsonDeserializer();
                            var _jSONObject = _jsonDeserialer.Deserialize<Dictionary<string, string>>(_resultApi);
                            var _resultJson = JsonConvert.DeserializeObject<EspecificacionMaster>(_jSONObject["data"]);

                            _especificacionMaster.iD_STATUS = _resultiDSTATUS;
                            _especificacionMaster.iD_ESPECIFICACION = int.Parse(iD_ESPECIFICACION);
                            _especificacionMaster.iD_PLANTA = int.Parse(iD_PLANTA);
                            _especificacionMaster.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _especificacionMaster.iD_CLIENTE = _resultJson.iD_CLIENTE; // int.Parse(iD_CLIENTE);
                            _especificacionMaster.tanquE_PX = _resultJson.tanquE_PX; // int.Parse(tanquE_PX);
                            _especificacionMaster.iD_GRADO = _resultJson.iD_GRADO; // int.Parse(iD_GRADO);
                            _especificacionMaster.iD_TIPO_ESPECIFICACION = _resultJson.iD_TIPO_ESPECIFICACION; // int.Parse(iD_TIPO_ESPECIFICACION);
                            _especificacionMaster.observaciones = observaciones;
                            _especificacionMaster.usR_MODIFICA = 1;

                            //var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + _especificacionMaster.iD_ESPECIFICACION, _especificacionMaster);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteEspecificacionMaster(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                EspecificacionMaster model2 = new EspecificacionMaster();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<EspecificacionMaster>(JSONObj["data"]);

                model2.iD_ESPECIFICACION = result.iD_ESPECIFICACION;
                model2.iD_PLANTA = result.iD_PLANTA;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.iD_CLIENTE = result.iD_CLIENTE;
                model2.tanquE_PX = result.tanquE_PX;
                model2.iD_GRADO = result.iD_GRADO;
                model2.iD_TIPO_ESPECIFICACION = result.iD_TIPO_ESPECIFICACION;
                model2.observaciones = result.observaciones;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetEspecificacionMasterHTMLTagsById(string Id)
        {
            String response = String.Empty;
            var especificacionMaster = getEspecificacionMaster();
            var plants = getPlantas();
            var products = getProductos();
            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<EspecificacionMaster>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_ESPECIFICACION' class='form-control' >";

                foreach (var item in especificacionMaster)
                {
                    if (entity.iD_ESPECIFICACION == item.iD_ESPECIFICACION)
                        idTag += "<option value='" + item.iD_ESPECIFICACION + "' selected >" + item.tanquE_PX + " </option>";
                    else
                        idTag += "<option value='" + item.iD_ESPECIFICACION + "'>" + item.tanquE_PX + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var plantaTag = "<select  id='iD_PLANTA' class='form-control'>";

                foreach (var item in plants)
                {
                    if (entity.iD_PLANTA == item.iD_PLANTA)
                        plantaTag += "<option value='" + item.iD_PLANTA + "' selected >" + item.descripcion + " </option>";
                    else
                        plantaTag += "<option value='" + item.iD_PLANTA + "'>" + item.descripcion + " </option>";
                }

                plantaTag += "</select>";

                var productoTag = "<select  id='iD_PRODUCTO' class='form-control'>";

                foreach (var item in products)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        productoTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        productoTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                productoTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_ESPECIFICACION + "' ><i class='fa fa-save'></i></a> </td>" +
                //"<td>" + idTag + "</td>" +
                "<td>" + plantaTag + "</td>" +
                "<td>" + productoTag + "</td>" +
                "<td style='display: none;'><input class='form-control' id='iD_CLIENTE' type='text'  value='" + entity.iD_CLIENTE + "'></td>" +
                "<td style='display: none;'><input class='form-control' id='tanquE_PX' type='text'  value='" + entity.tanquE_PX + "'></td>" +
                "<td style='display: none;'><input class='form-control' id='iD_GRADO' type='text'  value='" + entity.iD_GRADO + "'></td>" +
                "<td style='display: none;'><input class='form-control' id='iD_TIPO_ESPECIFICACION' type='text'  value='" + entity.iD_TIPO_ESPECIFICACION + "'></td>" +
                "<td><input class='form-control' id='observaciones' type='text'  value='" + entity.observaciones + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        public JsonResult GetSpecificationMasterById(string Id)
        {
            String response = String.Empty;

            var especificacionDetalle = getEspecificacionDetalle();
            var parametro = getParametro();
            var unidadMedida = getUnidadMedidas();

            List<EspecificacionDetalle> _lstSpecificationDetail = new List<EspecificacionDetalle>();
            List<Parametro> _lstParameter = new List<Parametro>();
            List<UnidadMedidas> _lstUnityMeasurement = new List<UnidadMedidas>();

            if (!string.IsNullOrEmpty(Id))
                _lstSpecificationDetail = especificacionDetalle.Where(x => x.iD_ESPECIFICACION == int.Parse(Id)).ToList();
            else
                _lstSpecificationDetail = new List<EspecificacionDetalle>();

            if (parametro != null && parametro.Any())
                _lstParameter = parametro;
            else
                _lstParameter = new List<Parametro>();

            if (unidadMedida != null && unidadMedida.Any())
                _lstUnityMeasurement = unidadMedida;
            else
                _lstUnityMeasurement = new List<UnidadMedidas>();

            var idTag = "<select id='iD_PRODUCTO_GRADO' class='form-control'>";

            foreach (var item in _lstSpecificationDetail)
            {
                if (item.iD_ESPECIFICACION == item.iD_ESPECIFICACION)
                    idTag += "<option value='" + item.iD_ESPECIFICACION + "' selected >" + item.iD_ESPECIFICACION + " </option>";
                else
                    idTag += "<option value='" + item.iD_ESPECIFICACION + "'>" + item.iD_ESPECIFICACION + " </option>";

                idTag += "</select>";

                string _estatus = string.Empty;

                if (item.iD_STATUS == 1)
                    _estatus = "Activo";
                else
                    _estatus = "Inactivo";

                string _parametro = string.Empty;

                if (item.iD_PARAMETRO > 0)
                    _parametro = _lstParameter.FirstOrDefault(x => x.iD_PARAMETRO == item.iD_PARAMETRO).descripcion;
                else
                    _parametro = string.Empty;

                string _unidadMedida = string.Empty;

                if (item.iD_UNIDAD_MEDIDA > 0)
                    _unidadMedida = _lstUnityMeasurement.FirstOrDefault(x => x.iD_UNIDAD_MEDIDA == item.iD_UNIDAD_MEDIDA).descripcion;
                else
                    _unidadMedida = string.Empty;

                response +=
                "<tr>" +
                    "<td></td>" +
                    "<td>" + _parametro + "</td>" +
                    "<td>" + _unidadMedida + "</td>" +
                    "<td>" + item.valoR_LIMITE_SUP + "</td>" +
                    "<td>" + item.valoR_LIMITE_INF + "</td>" +
                    "<td>" + item.observaciones + "</td>" +
                    "<td>" + item.orden + "</td>" +
                    "<td>" + _estatus + "</td>" +
                    "<td>" +
                        "<a href='javascript:void(0)' onclick='deleteOnClickEspecificacionDetalle(this); return false;' data-id='" + item.id + "' class='btn btn-default btn-xs'>" +
                            "<i class='fa fa-trash-alt'></i>" +
                        "</a>" +
                        "<a href='javascript:void(0)' onclick='editOnClickEspecificacionDetalle(this);return false;' id='editData' class='btn btn-default btn-xs' data-id='" + item.id + "'>" +
                            "<i class='fa fa-edit'></i>" +
                        "</a>" +
                    //"<a href='javascript:void(0)' onclick='refresh();return false;' class='btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                    //"<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + item.iD_GRADO + "'><i class='fa fa-save'></i></a>" +
                    "</td>" +
                "</tr>";
            }

            String _tableHead = String.Empty;

            _tableHead +=
                                    //"<div id='content' class='content'>" +
                                    //    "<div class='section-container section-with-top-border p-b-5'>" +
                                    //       "<div class='row'>" +
                                    //            "<div class='panel panel-primary'>" +
                                    //"<div class='panel-heading'>" +
                                    //"<div class='btn-group pull-left'>" +
                                    //"<a href='javascript:void(0)' onclick='addEntryRolAlias(this); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                    //"<i class='fa fa-plus'></i> Agregar Registro" +
                                    //"</a>" +
                                    //"<a href='javascript:;' class='btn btn-white m-r-5' id='ExporttoExcel'>" +
                                    //    "<i class='fa fa-file-excel'></i> Exportar a excel" +
                                    //"</a>" +
                                    //"</div>" +
                                    //"<div class='btn-group pull-right'>" +
                                    //    "<a href='javascript:;' class='btn btn-white m-r-5' id='ClearFilters'>" +
                                    //        "<i class='fa fa-eraser'></i>Limpiar filtros" +
                                    //    "</a>" +
                                    //"</div>" +
                                    //"<h4 class='panel-title'>" +
                                    //"&nbsp;" +
                                    //"</h4>" +
                                    //"</div>" +

                                    //"<!-- Mensaje de error -->" +
                                    //"<div class='col-md-12' id='message'></div>" +

                                    //"<div class='panel-body'>" +
                                    //    "<div>" +                                    
                                    "<table id='data-table-especificacion-master-" + Id + "' class='table table-bordered table-hover'>" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<th class='no-sort' style='width:1%'></th>" +
                                                "<th style='white-space: nowrap'>Parámetro</th>" +
                                                "<th style='white-space: nowrap'>Unidad de Medida</th>" +
                                                "<th style='white-space: nowrap'>Valor Límite Superior</th>" +
                                                "<th style='white-space: nowrap'>Valor Límite Inferior</th>" +
                                                "<th style='white-space: nowrap'>Observaciones</th>" +
                                                "<th style='white-space: nowrap'>Orden</th>" +
                                                "<th style='white-space: nowrap'>Estatus</th>" +
                                                "<th style='white-space: nowrap; width: 1%'>" +
                                                    "<a href='javascript:void(0)' onclick='addEntryEspecificacionDetalle(this, " + Id + "); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                                        "<i class='fa fa-plus'></i>" +
                                                    "</a>" +
                                                "</th> " +
                                            "</tr>" +
                                        "</thead>";

            String _tableFoot = String.Empty;

            _tableFoot +=
                                    "</table>"; // +
            //                    "</div>" +
            //                "</div>" +
            //            "</div>" +
            //        "</div>" +
            //    "</div>" +
            //"</div";

            return Json(new { Result = "Ok", Html = _tableHead + "<tbody>" + response + "</tbody>" + _tableFoot });
        }

        #endregion

        #region Especificacion Master 2

        public IActionResult EspecificacionMaster2()
        {
            var especificacionDetalleExportExcel = getEspecificacionDetalle();
            var especificacionMasterExportExcel = getEspecificacionMaster();
            var especificacionMaster = getEspecificacionMaster();
            var plantas = getPlantas();
            var clientes = getClientes();
            var tanque = getTanque();
            var productos = getProductos();
            var grados = getGrado();
            var tipoEspecificacion = getTipoEspecificacion();
            var parametro = getParametro();
            var unidadMedida = getUnidadMedidas();

            EspecificacionMasterViewModel _especificacionMasterVM = new EspecificacionMasterViewModel();

            //_especificacionMasterVM.EspecificacionMasterList = especificacionMaster;
            //_especificacionMasterVM.EspecificacionMasterFilter = especificacionMaster.ConvertAll(a =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = a.tanquE_PX.ToString(), // Por definir
            //        Value = a.iD_ESPECIFICACION.ToString()
            //    };
            //});

            List<EspecificacionMasterExportExcel> lstSpecificationMasterExportExcels = new List<EspecificacionMasterExportExcel>();

            lstSpecificationMasterExportExcels = (from masters in especificacionMasterExportExcel
                                                  join details in especificacionDetalleExportExcel
                                                  on masters.iD_ESPECIFICACION equals details.iD_ESPECIFICACION // join on some property                                                  
                                                  into joinedList
                                                  from sub in joinedList.DefaultIfEmpty()
                                                  where masters.iD_TIPO_ESPECIFICACION.Equals(2)
                                                  select new EspecificacionMasterExportExcel
                                                  {
                                                      iD_CLIENTE = masters.iD_CLIENTE,
                                                      tanquE_PX = masters.tanquE_PX,
                                                      iD_PRODUCTO = masters.iD_PRODUCTO,
                                                      iD_GRADO = masters.iD_GRADO,
                                                      observaciones = masters.observaciones,
                                                      iD_STATUS = masters.iD_STATUS,
                                                      iD_PARAMETRO = sub == null ? 0 : sub.iD_PARAMETRO,
                                                      iD_UNIDAD_MEDIDA = sub == null ? 0 : sub.iD_UNIDAD_MEDIDA,
                                                      valoR_LIMITE_SUP = sub == null ? 0 : sub.valoR_LIMITE_SUP,
                                                      valoR_LIMITE_INF = sub == null ? 0 : sub.valoR_LIMITE_INF,
                                                      observaciones2 = sub == null ? string.Empty : sub.observaciones,
                                                      orden = sub == null ? 0 : sub.orden
                                                  }).ToList();

            _especificacionMasterVM.EspecificacionMasterExportExcelList = lstSpecificationMasterExportExcels;

            _especificacionMasterVM.EspecificacionMasterList = especificacionMaster.Where(x => x.iD_TIPO_ESPECIFICACION == 2).GroupBy(c => new
            {
                c.iD_ESPECIFICACION,
                c.iD_CLIENTE,
                c.tanquE_PX,
                c.iD_PRODUCTO,
                c.iD_GRADO,
                c.observaciones,
                c.iD_STATUS
            })
            .Select(gcs => new EspecificacionMaster()
            {
                iD_ESPECIFICACION = gcs.Key.iD_ESPECIFICACION,
                iD_CLIENTE = gcs.Key.iD_CLIENTE,
                tanquE_PX = gcs.Key.tanquE_PX,
                iD_PRODUCTO = gcs.Key.iD_PRODUCTO,
                iD_GRADO = gcs.Key.iD_GRADO,
                observaciones = gcs.Key.observaciones,
                iD_STATUS = gcs.Key.iD_STATUS
            }).ToList();

            _especificacionMasterVM.EspecificacionMasterFilter = especificacionMaster.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.iD_ESPECIFICACION.ToString(), // Por definir
                    Value = a.iD_ESPECIFICACION.ToString()
                };
            });

            _especificacionMasterVM.PlantasList = plantas;
            _especificacionMasterVM.PlantasFilter = plantas.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PLANTA.ToString()
                };
            });

            _especificacionMasterVM.ClientesList = clientes;
            _especificacionMasterVM.ClientesFilter = clientes.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_CLIENTE.ToString()
                };
            });

            _especificacionMasterVM.TanqueList = tanque;
            _especificacionMasterVM.TanqueFilter = tanque.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TANQUE.ToString()
                };
            });

            _especificacionMasterVM.ProductosList = productos;
            _especificacionMasterVM.ProductosFilter = productos.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _especificacionMasterVM.GradosList = grados;
            _especificacionMasterVM.GradosFilter = grados.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_GRADO.ToString()
                };
            });

            _especificacionMasterVM.TipoEspecificacionList = tipoEspecificacion;
            _especificacionMasterVM.TipoEspecificacionFilter = tipoEspecificacion.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO_ESPECIFICACION.ToString()
                };
            });

            _especificacionMasterVM.ParametroList = parametro;
            _especificacionMasterVM.ParametroFilter = parametro.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PARAMETRO.ToString()
                };
            });

            _especificacionMasterVM.UnidadMedidasList = unidadMedida;
            _especificacionMasterVM.UnidadMedidasFilter = unidadMedida.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_UNIDAD_MEDIDA.ToString()
                };
            });

            return View(_especificacionMasterVM);
        }

        public IActionResult ClearFiltersEspecificacionMaster2()
        {
            return RedirectToAction("EspecificacionMaster2");
        }

        public JsonResult SaveOrEditEspecificacionMaster2([FromBody] DtoEspecificacionMaster data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_ESPECIFICACION))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_ESPECIFICACION = data.iD_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_CLIENTE = data.iD_CLIENTE; //?.Trim().Replace(" ", "").Split(",");
                        var tanquE_PX = data.tanquE_PX; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_ESPECIFICACION = data.iD_TIPO_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var observaciones = data.observaciones; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && observaciones != null)
                        {
                            EspecificacionMaster _especificacionMaster = new EspecificacionMaster();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _especificacionMaster.iD_STATUS = _resultiDSTATUS;
                            //_especificacionMaster.iD_ESPECIFICACION = int.Parse(iD_ESPECIFICACION);
                            _especificacionMaster.iD_PLANTA = 1;
                            _especificacionMaster.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _especificacionMaster.iD_CLIENTE = int.Parse(iD_CLIENTE);
                            _especificacionMaster.tanquE_PX = int.Parse(tanquE_PX);
                            _especificacionMaster.iD_GRADO = int.Parse(iD_GRADO);
                            _especificacionMaster.iD_TIPO_ESPECIFICACION = 2; // int.Parse(iD_TIPO_ESPECIFICACION);
                            _especificacionMaster.observaciones = observaciones;
                            _especificacionMaster.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url, _especificacionMaster);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ESPECIFICACION = data.iD_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_CLIENTE = data.iD_CLIENTE; //?.Trim().Replace(" ", "").Split(",");
                        var tanquE_PX = data.tanquE_PX; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_ESPECIFICACION = data.iD_TIPO_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var observaciones = data.observaciones; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && observaciones != null)
                        {
                            EspecificacionMaster _especificacionMaster = new EspecificacionMaster();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";

                            RestGenerico _restGeneric = new RestGenerico();
                            var _resultApi = _restGeneric.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + iD_ESPECIFICACION);
                            JsonDeserializer _jsonDeserialer = new JsonDeserializer();
                            var _jSONObject = _jsonDeserialer.Deserialize<Dictionary<string, string>>(_resultApi);
                            var _resultJson = JsonConvert.DeserializeObject<EspecificacionMaster>(_jSONObject["data"]);

                            _especificacionMaster.iD_STATUS = _resultiDSTATUS;
                            _especificacionMaster.iD_ESPECIFICACION = int.Parse(iD_ESPECIFICACION);
                            _especificacionMaster.iD_PLANTA = _resultJson.iD_PLANTA;
                            _especificacionMaster.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _especificacionMaster.iD_CLIENTE = int.Parse(iD_CLIENTE);
                            _especificacionMaster.tanquE_PX = int.Parse(tanquE_PX);
                            _especificacionMaster.iD_GRADO = int.Parse(iD_GRADO);
                            _especificacionMaster.iD_TIPO_ESPECIFICACION = _resultJson.iD_TIPO_ESPECIFICACION; // int.Parse(iD_TIPO_ESPECIFICACION);
                            _especificacionMaster.observaciones = observaciones;
                            _especificacionMaster.usR_MODIFICA = 1;

                            //var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + _especificacionMaster.iD_ESPECIFICACION, _especificacionMaster);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteEspecificacionMaster2(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                EspecificacionMaster model2 = new EspecificacionMaster();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<EspecificacionMaster>(JSONObj["data"]);

                model2.iD_ESPECIFICACION = result.iD_ESPECIFICACION;
                model2.iD_PLANTA = result.iD_PLANTA;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.iD_CLIENTE = result.iD_CLIENTE;
                model2.tanquE_PX = result.tanquE_PX;
                model2.iD_GRADO = result.iD_GRADO;
                model2.iD_TIPO_ESPECIFICACION = result.iD_TIPO_ESPECIFICACION;
                model2.observaciones = result.observaciones;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetEspecificacionMaster2HTMLTagsById(string Id)
        {
            String response = String.Empty;
            var especificacionMaster = getEspecificacionMaster();
            var clients = getClientes();
            var tanks = getTanque();
            var products = getProductos();
            var grade = getGrado();
            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<EspecificacionMaster>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_ESPECIFICACION' class='form-control' >";

                foreach (var item in especificacionMaster)
                {
                    if (entity.iD_ESPECIFICACION == item.iD_ESPECIFICACION)
                        idTag += "<option value='" + item.iD_ESPECIFICACION + "' selected >" + item.tanquE_PX + " </option>";
                    else
                        idTag += "<option value='" + item.iD_ESPECIFICACION + "'>" + item.tanquE_PX + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var clienteTag = "<select  id='iD_CLIENTE' class='form-control'>";

                foreach (var item in clients)
                {
                    if (entity.iD_CLIENTE == item.iD_CLIENTE)
                        clienteTag += "<option value='" + item.iD_CLIENTE + "' selected >" + item.nombre + " </option>";
                    else
                        clienteTag += "<option value='" + item.iD_CLIENTE + "'>" + item.nombre + " </option>";
                }

                clienteTag += "</select>";

                var tanqueTag = "<select  id='tanquE_PX' class='form-control'>";

                foreach (var item in tanks)
                {
                    if (entity.tanquE_PX == item.iD_TANQUE)
                        tanqueTag += "<option value='" + item.iD_TANQUE + "' selected >" + item.descripcion + " </option>";
                    else
                        tanqueTag += "<option value='" + item.iD_TANQUE + "'>" + item.descripcion + " </option>";
                }

                tanqueTag += "</select>";

                var productoTag = "<select  id='iD_PRODUCTO' class='form-control'>";

                foreach (var item in products)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        productoTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        productoTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                productoTag += "</select>";

                var gradoTag = "<select  id='iD_GRADO' class='form-control'>";

                foreach (var item in grade)
                {
                    if (entity.iD_GRADO == item.iD_GRADO)
                        gradoTag += "<option value='" + item.iD_GRADO + "' selected >" + item.descripcion + " </option>";
                    else
                        gradoTag += "<option value='" + item.iD_GRADO + "'>" + item.descripcion + " </option>";
                }

                gradoTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_ESPECIFICACION + "' ><i class='fa fa-save'></i></a> </td>" +
                //"<td>" + idTag + "</td>" +
                "<td style='display: none;'><input class='form-control' id='iD_PLANTA' type='text'  value='>" + entity.iD_PLANTA + "'></td>" +
                "<td>" + clienteTag + "</td>" +
                "<td>" + tanqueTag + "</td>" +
                "<td>" + productoTag + "</td>" +
                "<td>" + gradoTag + "</td>" +
                "<td style='display: none;'><input class='form-control' id='iD_TIPO_ESPECIFICACION' type='text'  value='" + entity.iD_TIPO_ESPECIFICACION + "'></td>" +
                "<td><input class='form-control' id='observaciones' type='text'  value='" + entity.observaciones + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Especificacion Master 3

        public IActionResult EspecificacionMaster3()
        {
            var especificacionDetalleExportExcel = getEspecificacionDetalle();
            var especificacionMasterExportExcel = getEspecificacionMaster();
            var especificacionMaster = getEspecificacionMaster();
            var plantas = getPlantas();
            var clientes = getClientes();
            var productos = getProductos();
            var grados = getGrado();
            var tipoEspecificacion = getTipoEspecificacion();
            var parametro = getParametro();
            var unidadMedida = getUnidadMedidas();

            EspecificacionMasterViewModel _especificacionMasterVM = new EspecificacionMasterViewModel();

            //_especificacionMasterVM.EspecificacionMasterList = especificacionMaster;
            //_especificacionMasterVM.EspecificacionMasterFilter = especificacionMaster.ConvertAll(a =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = a.tanquE_PX.ToString(), // Por definir
            //        Value = a.iD_ESPECIFICACION.ToString()
            //    };
            //});

            List<EspecificacionMasterExportExcel> lstSpecificationMasterExportExcels = new List<EspecificacionMasterExportExcel>();

            lstSpecificationMasterExportExcels = (from masters in especificacionMasterExportExcel
                                                  join details in especificacionDetalleExportExcel
                                                  on masters.iD_ESPECIFICACION equals details.iD_ESPECIFICACION // join on some property                                                  
                                                  into joinedList
                                                  from sub in joinedList.DefaultIfEmpty()
                                                  where masters.iD_TIPO_ESPECIFICACION.Equals(3)
                                                  select new EspecificacionMasterExportExcel
                                                  {
                                                      iD_PRODUCTO = masters.iD_PRODUCTO,
                                                      iD_GRADO = masters.iD_GRADO,
                                                      observaciones = masters.observaciones,
                                                      iD_STATUS = masters.iD_STATUS,
                                                      iD_PARAMETRO = sub == null ? 0 : sub.iD_PARAMETRO,
                                                      iD_UNIDAD_MEDIDA = sub == null ? 0 : sub.iD_UNIDAD_MEDIDA,
                                                      valoR_LIMITE_SUP = sub == null ? 0 : sub.valoR_LIMITE_SUP,
                                                      valoR_LIMITE_INF = sub == null ? 0 : sub.valoR_LIMITE_INF,
                                                      observaciones2 = sub == null ? string.Empty : sub.observaciones,
                                                      orden = sub == null ? 0 : sub.orden
                                                  }).ToList();

            _especificacionMasterVM.EspecificacionMasterExportExcelList = lstSpecificationMasterExportExcels;

            _especificacionMasterVM.EspecificacionMasterList = especificacionMaster.Where(x => x.iD_TIPO_ESPECIFICACION == 3).GroupBy(c => new
            {
                c.iD_ESPECIFICACION,
                c.iD_PRODUCTO,
                c.iD_GRADO,
                c.observaciones,
                c.iD_STATUS
            })
            .Select(gcs => new EspecificacionMaster()
            {
                iD_ESPECIFICACION = gcs.Key.iD_ESPECIFICACION,
                iD_PRODUCTO = gcs.Key.iD_PRODUCTO,
                iD_GRADO = gcs.Key.iD_GRADO,
                observaciones = gcs.Key.observaciones,
                iD_STATUS = gcs.Key.iD_STATUS
            }).ToList();

            _especificacionMasterVM.EspecificacionMasterFilter = especificacionMaster.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.iD_ESPECIFICACION.ToString(), // Por definir
                    Value = a.iD_ESPECIFICACION.ToString()
                };
            });

            _especificacionMasterVM.PlantasList = plantas;
            _especificacionMasterVM.PlantasFilter = plantas.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PLANTA.ToString()
                };
            });

            _especificacionMasterVM.ClientesList = clientes;
            _especificacionMasterVM.ClientesFilter = clientes.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_CLIENTE.ToString()
                };
            });

            _especificacionMasterVM.ProductosList = productos;
            _especificacionMasterVM.ProductosFilter = productos.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _especificacionMasterVM.GradosList = grados;
            _especificacionMasterVM.GradosFilter = grados.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_GRADO.ToString()
                };
            });

            _especificacionMasterVM.TipoEspecificacionList = tipoEspecificacion;
            _especificacionMasterVM.TipoEspecificacionFilter = tipoEspecificacion.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO_ESPECIFICACION.ToString()
                };
            });

            _especificacionMasterVM.ParametroList = parametro;
            _especificacionMasterVM.ParametroFilter = parametro.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PARAMETRO.ToString()
                };
            });

            _especificacionMasterVM.UnidadMedidasList = unidadMedida;
            _especificacionMasterVM.UnidadMedidasFilter = unidadMedida.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_UNIDAD_MEDIDA.ToString()
                };
            });

            return View(_especificacionMasterVM);
        }

        public IActionResult ClearFiltersEspecificacionMaster3()
        {
            return RedirectToAction("EspecificacionMaster3");
        }

        public JsonResult SaveOrEditEspecificacionMaster3([FromBody] DtoEspecificacionMaster data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_ESPECIFICACION))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_ESPECIFICACION = data.iD_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_CLIENTE = data.iD_CLIENTE; //?.Trim().Replace(" ", "").Split(",");
                        var tanquE_PX = data.tanquE_PX; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_ESPECIFICACION = data.iD_TIPO_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var observaciones = data.observaciones; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && observaciones != null)
                        {
                            EspecificacionMaster _especificacionMaster = new EspecificacionMaster();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _especificacionMaster.iD_STATUS = _resultiDSTATUS;
                            //_especificacionMaster.iD_ESPECIFICACION = int.Parse(iD_ESPECIFICACION);
                            _especificacionMaster.iD_PLANTA = 1;
                            _especificacionMaster.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _especificacionMaster.iD_CLIENTE = 1; // int.Parse(iD_CLIENTE);
                            _especificacionMaster.tanquE_PX = 0; // int.Parse(tanquE_PX);
                            _especificacionMaster.iD_GRADO = int.Parse(iD_GRADO);
                            _especificacionMaster.iD_TIPO_ESPECIFICACION = 3; // int.Parse(iD_TIPO_ESPECIFICACION);
                            _especificacionMaster.observaciones = observaciones;
                            _especificacionMaster.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url, _especificacionMaster);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ESPECIFICACION = data.iD_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_CLIENTE = data.iD_CLIENTE; //?.Trim().Replace(" ", "").Split(",");
                        var tanquE_PX = data.tanquE_PX; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_ESPECIFICACION = data.iD_TIPO_ESPECIFICACION; //?.Trim().Replace(" ", "").Split(",");
                        var observaciones = data.observaciones; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && observaciones != null)
                        {
                            EspecificacionMaster _especificacionMaster = new EspecificacionMaster();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";

                            RestGenerico _restGeneric = new RestGenerico();
                            var _resultApi = _restGeneric.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + iD_ESPECIFICACION);
                            JsonDeserializer _jsonDeserialer = new JsonDeserializer();
                            var _jSONObject = _jsonDeserialer.Deserialize<Dictionary<string, string>>(_resultApi);
                            var _resultJson = JsonConvert.DeserializeObject<EspecificacionMaster>(_jSONObject["data"]);

                            _especificacionMaster.iD_STATUS = _resultiDSTATUS;
                            _especificacionMaster.iD_ESPECIFICACION = int.Parse(iD_ESPECIFICACION);
                            _especificacionMaster.iD_PLANTA = _resultJson.iD_PLANTA;
                            _especificacionMaster.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _especificacionMaster.iD_CLIENTE = _resultJson.iD_CLIENTE; // int.Parse(iD_CLIENTE);
                            _especificacionMaster.tanquE_PX = _resultJson.tanquE_PX; // int.Parse(tanquE_PX);
                            _especificacionMaster.iD_GRADO = int.Parse(iD_GRADO);
                            _especificacionMaster.iD_TIPO_ESPECIFICACION = _resultJson.iD_TIPO_ESPECIFICACION; // int.Parse(iD_TIPO_ESPECIFICACION);
                            _especificacionMaster.observaciones = observaciones;
                            _especificacionMaster.usR_MODIFICA = 1;

                            //var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + _especificacionMaster.iD_ESPECIFICACION, _especificacionMaster);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteEspecificacionMaster3(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
                EspecificacionMaster model2 = new EspecificacionMaster();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<EspecificacionMaster>(JSONObj["data"]);

                model2.iD_ESPECIFICACION = result.iD_ESPECIFICACION;
                model2.iD_PLANTA = result.iD_PLANTA;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.iD_CLIENTE = result.iD_CLIENTE;
                model2.tanquE_PX = result.tanquE_PX;
                model2.iD_GRADO = result.iD_GRADO;
                model2.iD_TIPO_ESPECIFICACION = result.iD_TIPO_ESPECIFICACION;
                model2.observaciones = result.observaciones;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetEspecificacionMaster3HTMLTagsById(string Id)
        {
            String response = String.Empty;
            var especificacionMaster = getEspecificacionMaster();
            var products = getProductos();
            var grade = getGrado();
            var _url = _catalogCertificate.urlCatalogs + "EspecificacionMaster";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<EspecificacionMaster, EspecificacionMaster>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<EspecificacionMaster>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_ESPECIFICACION' class='form-control' >";

                foreach (var item in especificacionMaster)
                {
                    if (entity.iD_ESPECIFICACION == item.iD_ESPECIFICACION)
                        idTag += "<option value='" + item.iD_ESPECIFICACION + "' selected >" + item.tanquE_PX + " </option>";
                    else
                        idTag += "<option value='" + item.iD_ESPECIFICACION + "'>" + item.tanquE_PX + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var productoTag = "<select  id='iD_PRODUCTO' class='form-control'>";

                foreach (var item in products)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        productoTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        productoTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                productoTag += "</select>";

                var gradoTag = "<select  id='iD_GRADO' class='form-control'>";

                foreach (var item in grade)
                {
                    if (entity.iD_GRADO == item.iD_GRADO)
                        gradoTag += "<option value='" + item.iD_GRADO + "' selected >" + item.descripcion + " </option>";
                    else
                        gradoTag += "<option value='" + item.iD_GRADO + "'>" + item.descripcion + " </option>";
                }

                gradoTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_ESPECIFICACION + "' ><i class='fa fa-save'></i></a> </td>" +
                //"<td>" + idTag + "</td>" +
                "<td style='display: none;'><input class='form-control' id='iD_PLANTA' type='text'  value='>" + entity.iD_PLANTA + "'></td>" +
                "<td>" + productoTag + "</td>" +
                "<td style='display: none;'><input class='form-control' id='iD_CLIENTE' type='text'  value='" + entity.iD_CLIENTE + "'></td>" +
                "<td style='display: none;'><input class='form-control' id='tanquE_PX' type='text'  value='" + entity.tanquE_PX + "'></td>" +
                "<td>" + gradoTag + "</td>" +
                "<td style='display: none;'><input class='form-control' id='iD_TIPO_ESPECIFICACION' type='text'  value='" + entity.iD_TIPO_ESPECIFICACION + "'></td>" +
                "<td><input class='form-control' id='observaciones' type='text'  value='" + entity.observaciones + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Grado

        public IActionResult Grado()
        {
            var grado = getGrado();
            GradoViewModel _gradoVM = new GradoViewModel();

            _gradoVM.GradosList = grado;
            _gradoVM.GradosFilter = grado.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_GRADO.ToString()
                };
            });

            return View(_gradoVM);
        }

        public IActionResult ClearFiltersGrado()
        {
            return RedirectToAction("Grado");
        }

        public JsonResult SaveOrEditGrado([FromBody] DtoGrados data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_GRADO))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var gradO_ESPECIAL = data.gradO_ESPECIAL; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (gradO_ESPECIAL != null && descripcion != null && iD_STATUS != null)
                        {
                            Grados _grados = new Grados();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _grados.iD_STATUS = _resultiDSTATUS;
                            _grados.gradO_ESPECIAL = int.Parse(gradO_ESPECIAL);
                            _grados.descripcion = descripcion;
                            _grados.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Grado";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Grados, Grados>(_url, _grados);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Grados, Grados>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var gradO_ESPECIAL = data.gradO_ESPECIAL; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (gradO_ESPECIAL != null && descripcion != null)
                        {
                            Grados _grados = new Grados();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _grados.iD_STATUS = _resultiDSTATUS;
                            _grados.iD_GRADO = int.Parse(iD_GRADO);
                            _grados.gradO_ESPECIAL = int.Parse(gradO_ESPECIAL);
                            _grados.descripcion = descripcion;
                            _grados.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Grado";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Grados, Grados>(_url + "/" + _grados.iD_GRADO, _grados);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Grados, Grados>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteGrado(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Grado";
                Grados model2 = new Grados();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Grados, Grados>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Grados>(JSONObj["data"]);

                model2.iD_GRADO = result.iD_GRADO;
                model2.descripcion = result.descripcion;
                model2.gradO_ESPECIAL = result.gradO_ESPECIAL;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Grados, Grados>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Grados, Grados>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetGradoHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var grado = getGrado();
            var _url = _catalogCertificate.urlCatalogs + "Grado";

            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Grados, Grados>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Grados>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_GRADO' class='form-control' >";

                foreach (var item in grado)
                {
                    if (entity.iD_GRADO == item.iD_GRADO)
                        idTag += "<option value='" + item.iD_GRADO + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_GRADO + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_GRADO + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' maxlength='50' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td><input class='form-control' id='gradO_ESPECIAL' type='number'  value='" + entity.gradO_ESPECIAL + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Metodo

        public IActionResult Metodo()
        {
            var metodo = getMetodo();

            MetodoViewModel _metodoVM = new MetodoViewModel();

            _metodoVM.MetodoList = metodo;
            _metodoVM.MetodoFilter = metodo.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_METODO.ToString()
                };
            });

            return View(_metodoVM);
        }

        public IActionResult ClearFiltersMetodo()
        {
            return RedirectToAction("Metodo");
        }

        public JsonResult SaveOrEditMetodo([FromBody] DtoMetodo data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_METODO))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_METODO = data.iD_METODO?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_STATUS != null && descripcion != null)
                        {
                            Metodo _metodo = new Metodo();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "1")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _metodo.iD_STATUS = _resultiDSTATUS;
                            //_metodo.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                            //_metodo.iD_METODO = int.Parse(iD_METODO[0].ToString());
                            _metodo.descripcion = descripcion;
                            _metodo.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Metodo";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Metodo, Metodo>(_url, _metodo);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Metodo, Metodo>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_METODO = data.iD_METODO; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_METODO != null && descripcion != null)
                        {
                            Metodo _metodo = new Metodo();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            //int resultiDStatus = 0;

                            //if (Int32.TryParse(iD_STATUS[0].ToString(), out resultiDStatus))
                            //    _metodo.iD_STATUS = resultiDStatus;

                            //_grados.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                            _metodo.iD_STATUS = _resultiDSTATUS;
                            _metodo.iD_METODO = int.Parse(iD_METODO);
                            _metodo.descripcion = descripcion;
                            _metodo.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Metodo";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Metodo, Metodo>(_url + "/" + _metodo.iD_METODO, _metodo);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Metodo, Metodo>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteMetodo(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Metodo";
                Metodo model2 = new Metodo();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Metodo, Metodo>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Metodo>(JSONObj["data"]);

                model2.iD_METODO = result.iD_METODO;
                model2.descripcion = result.descripcion;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Metodo, Metodo>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Metodo, Metodo>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetMetodoHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var metodo = getMetodo();
            var _url = _catalogCertificate.urlCatalogs + "Metodo";

            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Metodo, Metodo>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Metodo>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_METODO' class='form-control' >";

                foreach (var item in metodo)
                {
                    if (entity.iD_METODO == item.iD_METODO)
                        idTag += "<option value='" + item.iD_METODO + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_METODO + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_METODO + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' maxlength='50' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Pais

        public IActionResult Pais()
        {
            var paises = getPaises();

            PaisesViewModel _paisesVM = new PaisesViewModel();

            _paisesVM.PaisesList = paises;
            _paisesVM.PaisesFilter = paises.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PAIS
                };
            });

            return View(_paisesVM);
        }

        public IActionResult ClearFiltersPais()
        {
            return RedirectToAction("Pais");
        }

        public JsonResult SaveOrEditPais([FromBody] DtoPaises data)
        {
            var _countryApi = getPaises();

            try
            {
                if (data != null)
                {
                    if (_countryApi != null)
                    {

                        bool _countryDB = _countryApi.Any(x => x.iD_PAIS == data.iD_PAIS);

                        if (!_countryDB)
                        {
                            var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                            var iD_PAIS = data.iD_PAIS; //?.Trim().Replace(" ", "").Split(",");
                            var nombre = data.nombre; //?.Trim().Replace(" ", "").Split(",");

                            if (iD_PAIS != null && nombre != null)
                            {
                                Paises _paises = new Paises();

                                string _iDSTATUS = iD_STATUS;

                                int _resultiDSTATUS;

                                if (_iDSTATUS == "1")
                                    _resultiDSTATUS = 1;
                                else
                                    _resultiDSTATUS = 0;

                                _paises.iD_STATUS = _resultiDSTATUS;
                                //_paises.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                                _paises.iD_PAIS = iD_PAIS;
                                _paises.nombre = nombre;
                                _paises.identificador = "";
                                _paises.usR_ALTA = 1;

                                var _url = _catalogCertificate.urlCatalogs + "Paises";

                                RestGenerico _rest = new RestGenerico();
                                var _result = _rest.postApi<Paises, Paises>(_url, _paises);
                                JsonDeserializer deserial = new JsonDeserializer();
                                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                                var _error = JSONObj["error"];

                                if (_error.Trim() == "False")
                                {
                                    _result = _rest.getApi<Paises, Paises>(_url);
                                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                                }
                                else
                                {
                                    throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                }
                            }
                        }
                        else
                        {
                            //edit selected row

                            var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                            var iD_PAIS = data.iD_PAIS; //?.Trim().Replace(" ", "").Split(",");
                            var nombre = data.nombre; //?.Trim().Replace(" ", "").Split(",");

                            if (iD_PAIS != null && nombre != null)
                            {
                                Paises _paises = new Paises();

                                string _iDSTATUS = iD_STATUS;

                                int _resultiDSTATUS;

                                if (_iDSTATUS == "true")
                                    _resultiDSTATUS = 1;
                                else
                                    _resultiDSTATUS = 0;

                                //int resultiDStatus = 0;

                                //if (Int32.TryParse(iD_STATUS[0].ToString(), out resultiDStatus))
                                //    _paises.iD_STATUS = resultiDStatus;

                                _paises.iD_STATUS = _resultiDSTATUS;
                                //_grados.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                                _paises.iD_PAIS = iD_PAIS;
                                _paises.nombre = nombre;
                                _paises.usR_MODIFICA = 1;

                                var _url = _catalogCertificate.urlCatalogs + "Paises";
                                RestGenerico _rest = new RestGenerico();
                                var _result = _rest.postApi<Paises, Paises>(_url + "/" + _paises.iD_PAIS, _paises);
                                JsonDeserializer deserial = new JsonDeserializer();
                                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                                var _error = JSONObj["error"];

                                if (_error.Trim() == "False")
                                {
                                    _result = _rest.getApi<Paises, Paises>(_url);
                                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                                }
                                else
                                {
                                    throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeletePais(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Paises";
                Paises model2 = new Paises();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Paises, Paises>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Paises>(JSONObj["data"]);

                model2.iD_PAIS = result.iD_PAIS;
                model2.nombre = result.nombre;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Paises, Paises>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Paises, Paises>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetPaisHTMLTagsById(string Id)
        {
            String response = String.Empty;
            var paises = getPaises();
            var _url = _catalogCertificate.urlCatalogs + "Paises";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Paises, Paises>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Paises>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select id='iD_PAIS' class='form-control' >";

                foreach (var item in paises)
                {
                    if (entity.iD_PAIS == item.iD_PAIS)
                        idTag += "<option value='" + item.iD_PAIS + "' selected >" + item.nombre + " </option>";
                    else
                        idTag += "<option value='" + item.iD_PAIS + "'>" + item.nombre + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response +=
                "<tr>" +
                    "<td>" +
                        "<a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_PAIS + "' ><i class='fa fa-save'></i></a>" +
                    "</td>" +
                    "<td><input class='form-control' maxlength='3' id='iD_PAIS' type='text' value='" + entity.iD_PAIS + "' readonly></td>" +
                    "<td><input class='form-control' maxlength='50' id='nombre' type='text' value='" + entity.nombre + "'></td>" +
                    "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Parametro

        public IActionResult Parametro()
        {
            var parametro = getParametro();
            var unidadMedida = getUnidadMedidas();

            ParametroViewModel _parametroVM = new ParametroViewModel();

            _parametroVM.ParametroList = parametro;
            _parametroVM.ParametroFilter = parametro.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PARAMETRO.ToString()
                };
            });

            _parametroVM.UnidadMedidasList = unidadMedida;
            _parametroVM.UnidadMedidasFilter = unidadMedida.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_UNIDAD_MEDIDA.ToString()
                };
            });

            return View(_parametroVM);
        }

        public IActionResult ClearFiltersParametro()
        {
            return RedirectToAction("Parametro");
        }

        public JsonResult SaveOrEditParametro([FromBody] DtoParametro data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_PARAMETRO))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_PARAMETRO = data.iD_PARAMETRO?.Trim().Replace(" ", "").Split(",");
                        var iD_UNIDAD_MEDIDA = data.iD_UNIDAD_MEDIDA; //?.Trim().Replace(" ", "").Split(",");
                        var decimaleS_CERTIFICADO = data.decimaleS_CERTIFICADO; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");
                        var leyenda = data.leyenda; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_UNIDAD_MEDIDA != null && descripcion != null)
                        {
                            Parametro _parametro = new Parametro();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _parametro.iD_STATUS = _resultiDSTATUS;
                            //_parametro.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                            //_parametro.iD_PARAMETRO = int.Parse(iD_PARAMETRO[0].ToString());
                            _parametro.iD_UNIDAD_MEDIDA = int.Parse(iD_UNIDAD_MEDIDA);
                            _parametro.decimaleS_CERTIFICADO = int.Parse(decimaleS_CERTIFICADO);
                            _parametro.clavE_PALS = clavE_PALS;
                            _parametro.leyenda = leyenda;
                            _parametro.descripcion = descripcion;
                            _parametro.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Parametro";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Parametro, Parametro>(_url, _parametro);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Parametro, Parametro>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PARAMETRO = data.iD_PARAMETRO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_UNIDAD_MEDIDA = data.iD_UNIDAD_MEDIDA; //?.Trim().Replace(" ", "").Split(",");
                        var decimaleS_CERTIFICADO = data.decimaleS_CERTIFICADO; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");
                        var leyenda = data.leyenda; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PARAMETRO != null && descripcion != null)
                        {
                            Parametro _parametro = new Parametro();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _parametro.iD_STATUS = _resultiDSTATUS;
                            _parametro.iD_PARAMETRO = int.Parse(iD_PARAMETRO);
                            _parametro.iD_UNIDAD_MEDIDA = int.Parse(iD_UNIDAD_MEDIDA);
                            _parametro.decimaleS_CERTIFICADO = int.Parse(decimaleS_CERTIFICADO);
                            _parametro.clavE_PALS = clavE_PALS;
                            _parametro.leyenda = leyenda;
                            _parametro.descripcion = descripcion;
                            _parametro.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Parametro";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Parametro, Parametro>(_url + "/" + _parametro.iD_PARAMETRO, _parametro);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Parametro, Parametro>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteParametro(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Parametro";
                Parametro model2 = new Parametro();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Parametro, Parametro>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Parametro>(JSONObj["data"]);

                model2.iD_PARAMETRO = result.iD_PARAMETRO;
                model2.iD_UNIDAD_MEDIDA = result.iD_UNIDAD_MEDIDA;
                model2.decimaleS_CERTIFICADO = result.decimaleS_CERTIFICADO;
                model2.clavE_PALS = result.clavE_PALS;
                model2.leyenda = result.leyenda;
                model2.descripcion = result.descripcion;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Parametro, Parametro>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Parametro, Parametro>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetParametroHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var parametro = getParametro();
            var unitMeasure = getUnidadMedidas();
            var _url = _catalogCertificate.urlCatalogs + "Parametro";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Parametro, Parametro>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Parametro>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_PARAMETRO' class='form-control' >";

                foreach (var item in parametro)
                {
                    if (entity.iD_PARAMETRO == item.iD_PARAMETRO)
                        idTag += "<option value='" + item.iD_PARAMETRO + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_PARAMETRO + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";


                var unidadMedida = "<select  id='iD_UNIDAD_MEDIDA' class='form-control' >";

                foreach (var item in unitMeasure)
                {
                    if (entity.iD_UNIDAD_MEDIDA == item.iD_UNIDAD_MEDIDA)
                        unidadMedida += "<option value='" + item.iD_UNIDAD_MEDIDA + "' selected >" + item.descripcion + " </option>";
                    else
                        unidadMedida += "<option value='" + item.iD_UNIDAD_MEDIDA + "'>" + item.descripcion + " </option>";
                }

                unidadMedida += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_PARAMETRO + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td>" + unidadMedida + "</td>" +
                "<td><input class='form-control' id='decimaleS_CERTIFICADO' type='number'  value='" + entity.decimaleS_CERTIFICADO + "'></td>" +
                "<td><input class='form-control' maxlength='10' id='clavE_PALS' type='text'  value='" + entity.clavE_PALS + "'></td>" +
                "<td><input class='form-control' maxlength='50' id='leyenda' type='text'  value='" + entity.leyenda + "'></td>" +
                "<td><input class='form-control' maxlength='150' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Plantas

        public IActionResult Planta()
        {
            var plants = getPlantas();
            var country = getPaises();
            var supplyType = getTipoSuministro();
            //var sourceSupply = getFuenteSuministro();
            //var approvedPlant = getPlantaAprobada();

            PlantaViewModel _plantaVM = new PlantaViewModel();

            if (plants != null)
            {
                _plantaVM.PlantaList = plants;
                _plantaVM.PlantasFilter = plants.ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Text = a.descripcion,
                        Value = a.iD_PLANTA.ToString()
                    };
                });
            }

            if (country != null)
            {
                _plantaVM.PaisesList = country;
                _plantaVM.PaisesFilter = country.ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Text = a.nombre,
                        Value = a.iD_PAIS.ToString()
                    };
                });
            }

            if (supplyType != null)
            {
                _plantaVM.TipoSuministrosList = supplyType;
                _plantaVM.TipoSuministrosFilter = supplyType.ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Text = a.descripcion,
                        Value = a.iD_TIPO_SUMINISTRO.ToString()
                    };
                });
            }

            //if (sourceSupply != null)
            //{
            //    _plantaVM.FuenteSuministroList = sourceSupply;
            //    _plantaVM.FuenteSuministroFilter = sourceSupply.ConvertAll(a =>
            //    {
            //        return new SelectListItem()
            //        {
            //            Text = a.descripcion,
            //            Value = a.ID_FUENTE_SUMINISTRO.ToString()
            //        };
            //    });
            //}

            //if (approvedPlant != null)
            //{
            //    _plantaVM.PlantaAprobadaList = approvedPlant;
            //    _plantaVM.PlantaAprobadFilter = approvedPlant.ConvertAll(a =>
            //    {
            //        return new SelectListItem()
            //        {
            //            Text = a.descripcion,
            //            Value = a.ID_PLANTA_APROBADA.ToString()
            //        };
            //    });
            //}

            return View(_plantaVM);
        }

        public IActionResult ClearFiltersPlanta()
        {
            return RedirectToAction("Planta");
        }

        public JsonResult SaveOrEditPlanta([FromBody] DtoPlantas data)
        {
            try
            {
                if (data == null)
                {
                    return Json(new { Result = "Fail", Message = "Los datos proporcionados son nulos" });
                }

                Plantas planta = new Plantas();
                string url = _catalogCertificate.urlCatalogs + "Planta";
                RestGenerico restClient = new RestGenerico();
                JsonDeserializer deserializer = new JsonDeserializer();

                int idStatus = ParseStatus(data.iD_STATUS);
                int? idPlanta = ParseNullableInt(data.iD_PLANTA);
                int? idTipoSuministro = ParseNullableInt(data.iD_TIPO_SUMINISTRO);
                int? idFuenteSuministro = ParseNullableInt(data.ID_FUENTE_SUMINISTRO);
                int? idPlantaAprobada = ParseNullableInt(data.ID_PLANTA_APROBADA);

                planta.iD_STATUS = idStatus;
                planta.iD_PLANTA = idPlanta;
                planta.iD_PAIS = data.iD_PAIS ?? throw new ArgumentNullException(nameof(data.iD_PAIS), "El ID de País es obligatorio");
                planta.iD_TIPO_SUMINISTRO = idTipoSuministro;
                planta.descripcion = data.descripcion ?? throw new ArgumentNullException(nameof(data.descripcion), "La descripción es obligatoria");
                planta.clavE_CERTIFICADO = data.clavE_CERTIFICADO ?? throw new ArgumentNullException(nameof(data.clavE_CERTIFICADO), "La clave de certificado es obligatoria");
                planta.ID_FUENTE_SUMINISTRO = idFuenteSuministro;
                planta.ID_PLANTA_APROBADA = idPlantaAprobada;
                planta.identificador = ""; //data.identi

                if (idPlanta == null)
                {
                    planta.usR_ALTA = 1;

                    var response = restClient.postApi<Plantas, Plantas>(url, planta);
                    var result = deserializer.Deserialize<Dictionary<string, string>>(response);

                    if (result["error"].Trim().Equals("False", StringComparison.OrdinalIgnoreCase))
                    {
                        return Json(new { Result = "Ok" });
                    }
                    else
                    {
                        throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                    }
                }
                else
                {
                    planta.usR_MODIFICA = 1;

                    var response = restClient.postApi<Plantas, Plantas>($"{url}/{idPlanta}", planta);
                    var result = deserializer.Deserialize<Dictionary<string, string>>(response);
                    var error = result["error"];

                    if (result["error"].Trim().Equals("False", StringComparison.OrdinalIgnoreCase))
                    {
                        return Json(new { Result = "Ok" });
                    }
                    else
                    {
                        throw new Exception("Error al actualizar los datos. Verifique la información enviada.");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                return Json(new { Result = "Fail", Message = $"Error en los datos: {ex.Message}" });
            }
            catch (FormatException ex)
            {
                return Json(new { Result = "Fail", Message = $"Error de formato: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }
        }

        public JsonResult DeletePlanta(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Planta";
                Plantas model2 = new Plantas();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Plantas, Plantas>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Plantas>(JSONObj["data"]);

                model2.iD_PLANTA = result.iD_PLANTA;
                model2.iD_PAIS = result.iD_PAIS;
                model2.iD_TIPO_SUMINISTRO = result.iD_TIPO_SUMINISTRO;
                model2.identificador = result.identificador;
                model2.descripcion = result.descripcion;
                model2.clavE_CERTIFICADO = result.clavE_CERTIFICADO;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;
                model2.ID_FUENTE_SUMINISTRO = result.ID_FUENTE_SUMINISTRO;
                model2.ID_PLANTA_APROBADA = result.ID_PLANTA_APROBADA;

                _result = _rest.postApi<Plantas, Plantas>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var _error = JSONObj["error"];
                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Plantas, Plantas>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                    //model = JsonConvert.DeserializeObject<List<Plantas>>(JSONObj["data"]);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetPlantaHTMLTagsById(string Id)
        {
            String response = String.Empty;
            var plants = getPlantas();
            var country = getPaises();
            var supplyType = getTipoSuministro();
            //var sourceSupply = getFuenteSuministro();
            //var approvedPlant = getPlantaAprobada();
            var _url = _catalogCertificate.urlCatalogs + "Planta";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Plantas, Plantas>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Plantas>(JSONObj["data"]);

            if (entity != null)
            {
                var plantasTag = "<select  id='iD_PLANTA' class='form-control' >";

                foreach (var item in plants)
                {
                    if (entity.iD_PLANTA == item.iD_PLANTA)
                        plantasTag += "<option value='" + item.iD_PLANTA + "' selected >" + item.descripcion + " </option>";
                    else
                        plantasTag += "<option value='" + item.iD_PLANTA + "'>" + item.descripcion + " </option>";
                }

                plantasTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var paisTag = "<select  id='iD_PAIS' class='form-control' >";

                foreach (var item in country)
                {
                    if (entity.iD_PAIS == item.iD_PAIS)
                        paisTag += "<option value='" + item.iD_PAIS + "' selected >" + item.nombre + " </option>";
                    else
                        paisTag += "<option value='" + item.iD_PAIS + "'>" + item.nombre + " </option>";
                }

                paisTag += "</select>";

                var tipoSuministroTag = "<select  id='iD_TIPO_SUMINISTRO' class='form-control' >";

                foreach (var item in supplyType)
                {
                    if (entity.iD_TIPO_SUMINISTRO == item.iD_TIPO_SUMINISTRO)
                        tipoSuministroTag += "<option value='" + item.iD_TIPO_SUMINISTRO + "' selected >" + item.descripcion + " </option>";
                    else
                        tipoSuministroTag += "<option value='" + item.iD_TIPO_SUMINISTRO + "'>" + item.descripcion + " </option>";
                }

                tipoSuministroTag += "</select>";

                //var fuenteSuministroTag = "<select  id='ID_FUENTE_SUMINISTRO' class='form-control' >";

                //foreach (var item in sourceSupply)
                //{
                //    if (entity.ID_FUENTE_SUMINISTRO == item.ID_FUENTE_SUMINISTRO)
                //        fuenteSuministroTag += "<option value='" + item.ID_FUENTE_SUMINISTRO + "' selected >" + item.descripcion + " </option>";
                //    else
                //        fuenteSuministroTag += "<option value='" + item.ID_FUENTE_SUMINISTRO + "'>" + item.descripcion + " </option>";
                //}

                //fuenteSuministroTag += "</select>";

                //var plantaAprobadaTag = "<select  id='ID_PLANTA_APROBADA' class='form-control' >";

                //foreach (var item in approvedPlant)
                //{
                //    if (entity.ID_PLANTA_APROBADA == item.ID_PLANTA_APROBADA)
                //        plantaAprobadaTag += "<option value='" + item.ID_PLANTA_APROBADA + "' selected >" + item.descripcion + " </option>";
                //    else
                //        plantaAprobadaTag += "<option value='" + item.ID_PLANTA_APROBADA + "'>" + item.descripcion + " </option>";
                //}

                //plantaAprobadaTag += "</select>";

                response +=
                "<tr>" +
                    "<td> " +
                        "<a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_PLANTA + "' ><i class='fa fa-save'></i></a> " +
                    "</td>" +
                    //"<td>" + plantasTag + "</td>" +
                    "<td>" + paisTag + "</td>" +
                    "<td>" + tipoSuministroTag + "</td>" +
                    "<td><input class='form-control' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                    "<td><input class='form-control' id='clavE_CERTIFICADO' type='text'  value='" + entity.clavE_CERTIFICADO + "'></td>" +
                    //"<td>" + fuenteSuministroTag + "</td>" +
                    //"<td>" + plantaAprobadaTag + "</td>" +
                    "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Planta Producto

        public IActionResult PlantaProducto()
        {
            var plantaExportExcel = getPlantas();
            var plantaProductoExportExcel = getPlantaProducto();
            var plantaProducto = getPlantaProducto();
            var plantas = getPlantas();
            var producto = getProductos();
            var country = getPaises();
            var supplyType = getTipoSuministro();

            PlantaProductoViewModel _plantaProductoVM = new PlantaProductoViewModel();

            List<PlantaProductoExportExcel> plantaProductoExportExcels = new List<PlantaProductoExportExcel>();

            plantaProductoExportExcels = (from plants in plantaExportExcel
                                          join plantsProduct in plantaProductoExportExcel
                                          on plants.iD_PLANTA equals plantsProduct.iD_PLANTA // join on some property                                          
                                          into joinedList
                                          from sub in joinedList.DefaultIfEmpty()
                                          select new PlantaProductoExportExcel
                                          {
                                              iD_PAIS = plants.iD_PAIS,
                                              iD_TIPO_SUMINISTRO = plants.iD_TIPO_SUMINISTRO,
                                              descripcion = plants.descripcion,
                                              clavE_CERTIFICADO = plants.clavE_CERTIFICADO,
                                              identificador = plants.identificador,
                                              iD_STATUS = plants.iD_STATUS,
                                              iD_PLANTA = sub == null ? 0 : sub.iD_PLANTA,
                                              iD_PRODUCTO = sub == null ? 0 : sub.iD_PRODUCTO
                                          }).ToList();

            _plantaProductoVM.PlantaProductoExportExcelList = plantaProductoExportExcels;
            //_plantaProductoVM.PlantaProductoExportExcelFilter = plantaProductoExportExcels.ConvertAll(a =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = a.descripcion.ToString(),
            //        Value = a.iD_PLANTA.ToString()
            //    };
            //});

            _plantaProductoVM.PlantaProductoList = plantaProducto;
            _plantaProductoVM.PlantaProductoFilter = plantaProducto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.iD_PLANTA_PRODUCTO.ToString(),
                    Value = a.iD_PLANTA_PRODUCTO.ToString()
                };
            });

            _plantaProductoVM.PlantasList = plantas;
            _plantaProductoVM.PlantasFilter = plantas.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PLANTA.ToString()
                };
            });

            _plantaProductoVM.ProductosList = producto;
            _plantaProductoVM.ProductosFilter = producto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _plantaProductoVM.PaisesList = country;
            _plantaProductoVM.PaisesFilter = country.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PAIS.ToString()
                };
            });

            _plantaProductoVM.TipoSuministrosList = supplyType;
            _plantaProductoVM.TipoSuministrosFilter = supplyType.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO_SUMINISTRO.ToString()
                };
            });

            return View(_plantaProductoVM);
        }

        public IActionResult ClearFiltersPlantaProducto()
        {
            return RedirectToAction("PlantaProducto");
        }

        public JsonResult SaveOrEditPlantaProducto([FromBody] DtoPlantaProducto data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_PLANTA_PRODUCTO))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_PLANTA_PRODUCTO = data.iD_PLANTA_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO != null && iD_STATUS != null)
                        {
                            PlantaProducto _plantaProducto = new PlantaProducto();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _plantaProducto.iD_STATUS = _resultiDSTATUS;
                            //_plantaProducto.iD_PLANTA_PRODUCTO = int.Parse(iD_PLANTA_PRODUCTO);
                            _plantaProducto.iD_PLANTA = int.Parse(iD_PLANTA);
                            _plantaProducto.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _plantaProducto.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "PlantaProducto";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<PlantaProducto, PlantaProducto>(_url, _plantaProducto);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<PlantaProducto, PlantaProducto>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA_PRODUCTO = data.iD_PLANTA_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PLANTA_PRODUCTO != null && iD_STATUS != null)
                        {
                            PlantaProducto _plantaProducto = new PlantaProducto();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _plantaProducto.iD_STATUS = _resultiDSTATUS;
                            _plantaProducto.iD_PLANTA_PRODUCTO = int.Parse(iD_PLANTA_PRODUCTO);
                            _plantaProducto.iD_PLANTA = int.Parse(iD_PLANTA);
                            _plantaProducto.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _plantaProducto.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "PlantaProducto";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<PlantaProducto, PlantaProducto>(_url + "/" + _plantaProducto.iD_PLANTA_PRODUCTO, _plantaProducto);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<PlantaProducto, PlantaProducto>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeletePlantaProducto(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "PlantaProducto";
                PlantaProducto model2 = new PlantaProducto();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<PlantaProducto, PlantaProducto>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<PlantaProducto>(JSONObj["data"]);

                model2.iD_PLANTA_PRODUCTO = result.iD_PLANTA_PRODUCTO;
                model2.iD_PLANTA = result.iD_PLANTA;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<PlantaProducto, PlantaProducto>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<PlantaProducto, PlantaProducto>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetPlantaProductoHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var plantaProducto = getPlantaProducto();
            var producto = getProductos();
            var _url = _catalogCertificate.urlCatalogs + "PlantaProducto";

            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<PlantaProducto, PlantaProducto>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<PlantaProducto>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_PLANTA_PRODUCTO' class='form-control' >";

                foreach (var item in plantaProducto)
                {
                    if (entity.iD_PLANTA_PRODUCTO == item.iD_PLANTA_PRODUCTO)
                        idTag += "<option value='" + item.iD_PLANTA_PRODUCTO + "' selected >" + item.iD_PLANTA_PRODUCTO + " </option>";
                    else
                        idTag += "<option value='" + item.iD_PLANTA_PRODUCTO + "'>" + item.iD_PLANTA_PRODUCTO + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var productoTag = "<select  id='iD_PRODUCTO' class='form-control' >";

                foreach (var item in producto)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        productoTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        productoTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                productoTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClickPlantaProducto(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_PLANTA_PRODUCTO + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td style='display: none'><input class='form-control' id='iD_PLANTA' type='text'  value='" + entity.iD_PLANTA + "'></td>" +
                "<td>" + productoTag + "</td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        public JsonResult GetPlantProductById(string Id)
        {
            String response = String.Empty;

            var plantaProducto = getPlantaProducto();
            var producto = getProductos();

            List<PlantaProducto> _lstPlantProduct = new List<PlantaProducto>();
            List<Productos> _lstProduct = new List<Productos>();

            if (!string.IsNullOrEmpty(Id))
                _lstPlantProduct = plantaProducto.Where(x => x.iD_PLANTA == int.Parse(Id)).ToList();
            else
                _lstPlantProduct = new List<PlantaProducto>();

            if (producto != null && producto.Any())
                _lstProduct = producto;
            else
                _lstProduct = new List<Productos>();

            var idTag = "<select id='iD_PRODUCTO_GRADO' class='form-control'>";

            foreach (var item in _lstPlantProduct)
            {
                if (item.iD_PLANTA_PRODUCTO == item.iD_PLANTA_PRODUCTO)
                    idTag += "<option value='" + item.iD_PLANTA_PRODUCTO + "' selected >" + item.iD_PLANTA_PRODUCTO + " </option>";
                else
                    idTag += "<option value='" + item.iD_PLANTA_PRODUCTO + "'>" + item.iD_PLANTA_PRODUCTO + " </option>";

                idTag += "</select>";

                //var estatusTag = "<select id='iD_STATUS' class='form-control'>";

                //if (item.iD_STATUS == 1)
                //{
                //    estatusTag += "<option value='true' selected >Activo</option>";
                //    estatusTag += "<option value='false'>Inactivo</option>";
                //}
                //else
                //{
                //    estatusTag += "<option value='false' selected >Inactivo</option>";
                //    estatusTag += "<option value='true'>Activo</option>";
                //}

                //estatusTag += "</select>";

                string _estatus = string.Empty;

                if (item.iD_STATUS == 1)
                    _estatus = "Activo";
                else
                    _estatus = "Inactivo";

                string _descripcionProducto = string.Empty;

                if (item.iD_PRODUCTO > 0)
                    _descripcionProducto = _lstProduct.FirstOrDefault(x => x.iD_PRODUCTO == item.iD_PRODUCTO).nombre;
                else
                    _descripcionProducto = string.Empty;

                response +=
                "<tr>" +
                    "<td></td>" +
                    "<td>" + _descripcionProducto + "</td>" +
                    "<td>" + _estatus + "</td>" +
                    "<td>" +
                        "<a href='javascript:void(0)' onclick='deleteOnClickPlantaProducto(this); return false;' data-id='" + item.iD_PLANTA_PRODUCTO + "' class='btn btn-default btn-xs'>" +
                            "<i class='fa fa-trash-alt'></i>" +
                        "</a>" +
                        "<a href='javascript:void(0)' onclick='editOnClickPlantaProducto(this);return false;' id='editData' class='btn btn-default btn-xs' data-id='" + item.iD_PLANTA_PRODUCTO + "'>" +
                            "<i class='fa fa-edit'></i>" +
                        "</a>" +
                    //"<a href='javascript:void(0)' onclick='refresh();return false;' class='btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                    //"<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + item.iD_GRADO + "'><i class='fa fa-save'></i></a>" +
                    "</td>" +
                "</tr>";
            }

            String _tableHead = String.Empty;

            _tableHead +=
                                    //"<div id='content' class='content'>" +
                                    //    "<div class='section-container section-with-top-border p-b-5'>" +
                                    //       "<div class='row'>" +
                                    //            "<div class='panel panel-primary'>" +
                                    //"<div class='panel-heading'>" +
                                    //"<div class='btn-group pull-left'>" +
                                    //"<a href='javascript:void(0)' onclick='addEntryRolAlias(this); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                    //"<i class='fa fa-plus'></i> Agregar Registro" +
                                    //"</a>" +
                                    //"<a href='javascript:;' class='btn btn-white m-r-5' id='ExporttoExcel'>" +
                                    //    "<i class='fa fa-file-excel'></i> Exportar a excel" +
                                    //"</a>" +
                                    //"</div>" +
                                    //"<div class='btn-group pull-right'>" +
                                    //    "<a href='javascript:;' class='btn btn-white m-r-5' id='ClearFilters'>" +
                                    //        "<i class='fa fa-eraser'></i>Limpiar filtros" +
                                    //    "</a>" +
                                    //"</div>" +
                                    //"<h4 class='panel-title'>" +
                                    //"&nbsp;" +
                                    //"</h4>" +
                                    //"</div>" +

                                    //"<!-- Mensaje de error -->" +
                                    //"<div class='col-md-12' id='message'></div>" +

                                    //"<div class='panel-body'>" +
                                    //    "<div>" +
                                    "<table id='data-table-planta-producto-" + Id + "' class='table table-bordered table-hover'>" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<th class='no-sort' style='width:1%'></th>" +
                                                "<th style='white-space: nowrap;'>Producto</th>" +
                                                "<th style='white-space: nowrap'>Estatus</th>" +
                                                "<th style='white-space: nowrap; width: 1%'>" +
                                                    "<a href='javascript:void(0)' onclick='addEntryPlantaProducto(this, " + Id + "); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                                        "<i class='fa fa-plus'></i>" +
                                                    "</a>" +
                                                "</th> " +
                                            "</tr>" +
                                        "</thead>";

            String _tableFoot = String.Empty;

            _tableFoot +=
                                    "</table>"; // +
            //                    "</div>" +
            //                "</div>" +
            //            "</div>" +
            //        "</div>" +
            //    "</div>" +
            //"</div";

            return Json(new { Result = "Ok", Html = _tableHead + "<tbody>" + response + "</tbody>" + _tableFoot });
        }

        #endregion

        #region Producto

        public IActionResult Producto()
        {
            var producto = getProductos();

            ProductoViewModel _productoVM = new ProductoViewModel();

            _productoVM.ProductosList = producto;
            _productoVM.ProductosFilter = producto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            return View(_productoVM);
        }

        public IActionResult ClearFiltersProducto()
        {
            return RedirectToAction("Producto");
        }

        public JsonResult SaveOrEditProducto([FromBody] DtoProductos data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_PRODUCTO))
                    {
                        //var iD_PRODUCTO = data.iD_PRODUCTO?.Trim().Replace(" ", "").Split(",");
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");
                        var nombre = data.nombre; //?.Trim().Replace(" ", "").Split(",");
                        var nombrE_ESP = data.nombrE_ESP; //?.Trim().Replace(" ", "").Split(",");

                        if (clavE_PALS != null && nombre != null)
                        {
                            Productos _producto = new Productos();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _producto.iD_PRODUCTO = 0; // int.Parse(iD_PRODUCTO[0].ToString());
                            _producto.iD_STATUS = _resultiDSTATUS;
                            _producto.clavE_PALS = clavE_PALS;
                            _producto.nombre = nombre;
                            _producto.nombrE_ESP = nombrE_ESP;
                            _producto.identificador = "";
                            _producto.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Producto";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Productos, Productos>(_url, _producto);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Productos, Productos>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");
                        var nombre = data.nombre; //?.Trim().Replace(" ", "").Split(",");
                        var nombrE_ESP = data.nombrE_ESP; //?.Trim().Replace(" ", "").Split(",");

                        if (clavE_PALS != null && nombre != null)
                        {
                            TipoSuministros _tSuministro = new TipoSuministros();

                            Productos _producto = new Productos();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            //int resultiDPRODUCTO = 0;

                            //if (Int32.TryParse(iD_PRODUCTO[0].ToString(), out resultiDPRODUCTO))
                            //    _producto.iD_PRODUCTO = resultiDPRODUCTO;

                            _producto.iD_STATUS = _resultiDSTATUS;
                            _producto.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _producto.clavE_PALS = clavE_PALS;
                            _producto.nombre = nombre;
                            _producto.nombrE_ESP = nombrE_ESP;
                            _producto.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Producto";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Productos, Productos>(_url + "/" + _producto.iD_PRODUCTO, _producto);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Productos, Productos>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteProducto(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Producto";
                Productos model2 = new Productos();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Productos, Productos>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Productos>(JSONObj["data"]);

                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.clavE_PALS = result.clavE_PALS;
                model2.nombre = result.nombre;
                model2.nombrE_ESP = result.nombrE_ESP;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Productos, Productos>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Productos, Productos>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetProductoHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var producto = getProductos();
            var _url = _catalogCertificate.urlCatalogs + "Producto";

            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Productos, Productos>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Productos>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_PRODUCTO' class='form-control' >";

                foreach (var item in producto)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        idTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        idTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_PRODUCTO + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' maxlength='30' id='clavE_PALS' type='text'  value='" + entity.clavE_PALS + "'></td>" +
                "<td><input class='form-control' maxlength='50' id='nombre' type='text'  value='" + entity.nombre + "'></td>" +
                "<td><input class='form-control' maxlength='50' id='nombrE_ESP' type='text'  value='" + entity.nombrE_ESP + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Producto Grados

        public IActionResult ProductoGrados()
        {
            var productoGradoExportExcel = getProductoGrado();
            var productoGrado = getProductoGrado();
            var producto = getProductos();
            var grado = getGrado();

            ProductoGradoViewModel _productoGradoVM = new ProductoGradoViewModel();

            _productoGradoVM.ProductoGradoExportExcelList = productoGradoExportExcel;
            _productoGradoVM.ProductoGradoExportExcelFilter = productoGradoExportExcel.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO_GRADO.ToString()
                };
            });

            _productoGradoVM.ProductoGradoList = productoGrado.Where(x => x.iD_STATUS == 1).GroupBy(c => new
            {
                //c.iD_PRODUCTO_GRADO,
                c.clavE_PALS,
                c.nombre,
                c.nombrE_ESP,
                c.iD_STATUS
            })
            .Select(gcs => new ProductoGrado()
            {
                //iD_PRODUCTO_GRADO = gcs.Key.iD_PRODUCTO_GRADO,
                //iD_GRADO = gcs.Key.iD_GRADO,
                //iD_PRODUCTO = gcs.Key.iD_PRODUCTO,
                clavE_PALS = gcs.Key.clavE_PALS,
                nombre = gcs.Key.nombre,
                nombrE_ESP = gcs.Key.nombrE_ESP,
                iD_STATUS = gcs.Key.iD_STATUS
            }).ToList();

            _productoGradoVM.ProductoGradoFilter = productoGrado.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO_GRADO.ToString()
                };
            });

            _productoGradoVM.ProductosList = producto;
            _productoGradoVM.ProductosFilter = producto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _productoGradoVM.GradosList = grado;
            _productoGradoVM.GradosFilter = grado.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_GRADO.ToString()
                };
            });

            return View(_productoGradoVM);
        }

        public IActionResult ClearFiltersProductoGrados()
        {
            return RedirectToAction("ProductoGrados");
        }

        public JsonResult SaveOrEditProductoGrados([FromBody] DtoProductoGrado data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_PRODUCTO_GRADO))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_PRODUCTO_GRADO = data.iD_PRODUCTO_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");
                        var nombre = data.nombre; //?.Trim().Replace(" ", "").Split(",");
                        var nombrE_ESP = data.nombrE_ESP; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_GRADO != null && nombre != null)
                        {
                            ProductoGrado _productoGrado = new ProductoGrado();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _productoGrado.iD_STATUS = _resultiDSTATUS;
                            //_productoGrado.iD_PRODUCTO_GRADO = int.Parse(iD_PRODUCTO_GRADO);
                            _productoGrado.iD_GRADO = int.Parse(iD_GRADO);
                            _productoGrado.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _productoGrado.clavE_PALS = clavE_PALS;
                            _productoGrado.nombre = nombre;
                            _productoGrado.nombrE_ESP = nombrE_ESP;
                            _productoGrado.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "ProductoGrado";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<ProductoGrado, ProductoGrado>(_url, _productoGrado);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<ProductoGrado, ProductoGrado>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO_GRADO = data.iD_PRODUCTO_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_GRADO = data.iD_GRADO; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");
                        var nombre = data.nombre; //?.Trim().Replace(" ", "").Split(",");
                        var nombrE_ESP = data.nombrE_ESP; //?.Trim().Replace(" ", "").Split(",");

                        if (clavE_PALS != null && nombre != null)
                        {
                            ProductoGrado _productoGrado = new ProductoGrado();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _productoGrado.iD_STATUS = _resultiDSTATUS;
                            _productoGrado.iD_PRODUCTO_GRADO = int.Parse(iD_PRODUCTO_GRADO);
                            _productoGrado.iD_GRADO = int.Parse(iD_GRADO);
                            _productoGrado.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _productoGrado.clavE_PALS = clavE_PALS;
                            _productoGrado.nombre = nombre;
                            _productoGrado.nombrE_ESP = nombrE_ESP;
                            _productoGrado.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "ProductoGrado";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<ProductoGrado, ProductoGrado>(_url + "/" + _productoGrado.iD_PRODUCTO_GRADO, _productoGrado);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<ProductoGrado, ProductoGrado>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteProductoGrados(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "ProductoGrado";
                ProductoGrado model2 = new ProductoGrado();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<ProductoGrado, ProductoGrado>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<ProductoGrado>(JSONObj["data"]);

                model2.iD_PRODUCTO_GRADO = result.iD_PRODUCTO_GRADO;
                model2.iD_GRADO = result.iD_GRADO;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.clavE_PALS = result.clavE_PALS;
                model2.nombre = result.nombre;
                model2.nombrE_ESP = result.nombrE_ESP;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<ProductoGrado, ProductoGrado>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<ProductoGrado, ProductoGrado>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetProductoGradosHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var productoGrado = getProductoGrado();
            var grado = getGrado();
            var producto = getProductos();
            var _url = _catalogCertificate.urlCatalogs + "ProductoGrado";

            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<ProductoGrado, ProductoGrado>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<ProductoGrado>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_PRODUCTO_GRADO' class='form-control' >";

                foreach (var item in productoGrado)
                {
                    if (entity.iD_PRODUCTO_GRADO == item.iD_PRODUCTO_GRADO)
                        idTag += "<option value='" + item.iD_PRODUCTO_GRADO + "' selected >" + item.nombre + " </option>";
                    else
                        idTag += "<option value='" + item.iD_PRODUCTO_GRADO + "'>" + item.nombre + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var gradoTag = "<select  id='iD_GRADO' class='form-control' >";

                foreach (var item in grado)
                {
                    if (entity.iD_GRADO == item.iD_GRADO)
                        gradoTag += "<option value='" + item.iD_GRADO + "' selected >" + item.descripcion + " </option>";
                    else
                        gradoTag += "<option value='" + item.iD_GRADO + "'>" + item.descripcion + " </option>";
                }

                gradoTag += "</select>";

                var productoTag = "<select  id='iD_PRODUCTO' class='form-control' >";

                foreach (var item in producto)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        productoTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        productoTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                productoTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClickProductoGrados(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_PRODUCTO_GRADO + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td>" + gradoTag + "</td>" +
                "<td style='display: none;'>" + productoTag + "</td>" +
                "<td style='display: none;'><input class='form-control' id='clavE_PALS' type='text' value='" + entity.clavE_PALS + "'></td>" +
                "<td style='display: none;'><input class='form-control' id='nombre' type='text' value='" + entity.nombre + "'></td>" +
                "<td style='display: none;'><input class='form-control' id='nombrE_ESP' type='text' value='" + entity.nombrE_ESP + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        public JsonResult GetProductGradesById(string Id)
        {
            String response = String.Empty;

            var productoGrado = getProductoGrado();
            var grado = getGrado();

            List<ProductoGrado> _lstProductGrade = new List<ProductoGrado>();
            List<Grados> _lstGrade = new List<Grados>();

            if (!string.IsNullOrEmpty(Id))
                _lstProductGrade = productoGrado.Where(x => x.clavE_PALS == Id).ToList();
            else
                _lstProductGrade = new List<ProductoGrado>();

            if (grado != null && grado.Any())
                _lstGrade = grado;
            else
                _lstGrade = new List<Grados>();

            var idTag = "<select id='iD_PRODUCTO_GRADO' class='form-control'>";

            foreach (var item in _lstProductGrade)
            {
                if (item.iD_PRODUCTO_GRADO == item.iD_PRODUCTO_GRADO)
                    idTag += "<option value='" + item.iD_PRODUCTO_GRADO + "' selected >" + item.nombre + " </option>";
                else
                    idTag += "<option value='" + item.iD_PRODUCTO_GRADO + "'>" + item.nombre + " </option>";

                idTag += "</select>";

                //var estatusTag = "<select id='iD_STATUS' class='form-control'>";

                //if (item.iD_STATUS == 1)
                //{
                //    estatusTag += "<option value='true' selected >Activo</option>";
                //    estatusTag += "<option value='false'>Inactivo</option>";
                //}
                //else
                //{
                //    estatusTag += "<option value='false' selected >Inactivo</option>";
                //    estatusTag += "<option value='true'>Activo</option>";
                //}

                //estatusTag += "</select>";

                string _estatus = string.Empty;

                if (item.iD_STATUS == 1)
                    _estatus = "Activo";
                else
                    _estatus = "Inactivo";

                string _descripcion = string.Empty;

                if (item.iD_GRADO > 0)
                    _descripcion = _lstGrade.FirstOrDefault(x => x.iD_GRADO == item.iD_GRADO).descripcion;
                else
                    _descripcion = string.Empty;

                response +=
                "<tr>" +
                    "<td></td>" +
                    "<td>" + _descripcion + "</td>" +
                    "<td>" + _estatus + "</td>" +
                    "<td>" +
                        "<a href='javascript:void(0)' onclick='deleteOnClickProductoGrados(this); return false;' data-id='" + item.iD_PRODUCTO_GRADO + "' class='btn btn-default btn-xs'>" +
                            "<i class='fa fa-trash-alt'></i>" +
                        "</a>" +
                        "<a href='javascript:void(0)' onclick='editOnClickProductoGrados(this);return false;' id='editData' class='btn btn-default btn-xs' data-id='" + item.iD_PRODUCTO_GRADO + "'>" +
                            "<i class='fa fa-edit'></i>" +
                        "</a>" +
                    //"<a href='javascript:void(0)' onclick='refresh();return false;' class='btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                    //"<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + item.iD_GRADO + "'><i class='fa fa-save'></i></a>" +
                    "</td>" +
                "</tr>";
            }

            String _tableHead = String.Empty;

            _tableHead +=
                                    //"<div id='content' class='content'>" +
                                    //    "<div class='section-container section-with-top-border p-b-5'>" +
                                    //       "<div class='row'>" +
                                    //            "<div class='panel panel-primary'>" +
                                    //"<div class='panel-heading'>" +
                                    //"<div class='btn-group pull-left'>" +
                                    //"<a href='javascript:void(0)' onclick='addEntryRolAlias(this); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                    //"<i class='fa fa-plus'></i> Agregar Registro" +
                                    //"</a>" +
                                    //"<a href='javascript:;' class='btn btn-white m-r-5' id='ExporttoExcel'>" +
                                    //    "<i class='fa fa-file-excel'></i> Exportar a excel" +
                                    //"</a>" +
                                    //"</div>" +
                                    //"<div class='btn-group pull-right'>" +
                                    //    "<a href='javascript:;' class='btn btn-white m-r-5' id='ClearFilters'>" +
                                    //        "<i class='fa fa-eraser'></i>Limpiar filtros" +
                                    //    "</a>" +
                                    //"</div>" +
                                    //"<h4 class='panel-title'>" +
                                    //"&nbsp;" +
                                    //"</h4>" +
                                    //"</div>" +

                                    //"<!-- Mensaje de error -->" +
                                    //"<div class='col-md-12' id='message'></div>" +

                                    //"<div class='panel-body'>" +
                                    //    "<div>" +                                    
                                    "<table id='data-table-producto-grados-" + Id + "' class='table table-bordered table-hover'>" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<th class='no-sort' style='width:1%'></th>" +
                                                "<th style='white-space: nowrap;'>Grado</th>" +
                                                "<th style='white-space: nowrap;'>Estatus</th>" +
                                                "<th style='white-space: nowrap; width: 1%'>" +
                                                    "<a href='javascript:void(0)' onclick='addEntryProductoGrados(this,\"" + Id + "\", " + _lstProductGrade[0].iD_PRODUCTO + "); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                                        "<i class='fa fa-plus'></i>" +
                                                    "</a>" +
                                                "</th> " +
                                            "</tr>" +
                                        "</thead>";

            String _tableFoot = String.Empty;

            _tableFoot +=
                                    "</table>"; // +
            //                    "</div>" +
            //                "</div>" +
            //            "</div>" +
            //        "</div>" +
            //    "</div>" +
            //"</div";

            return Json(new { Result = "Ok", Html = _tableHead + "<tbody>" + response + "</tbody>" + _tableFoot });
        }

        #endregion

        #region Producto Parametros

        public IActionResult ProductoParametros()
        {
            var productoParametros = getProductoParametros();
            var producto = getProductos();
            var parametros = getParametro();

            ProductoParametroViewModel _productoParametrosVM = new ProductoParametroViewModel();

            _productoParametrosVM.ProductoParametroList = productoParametros;
            _productoParametrosVM.ProductoParametroFilter = productoParametros.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO_PARAMETRO.ToString()
                };
            });

            _productoParametrosVM.ProductosList = producto;
            _productoParametrosVM.ProductosFilter = producto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _productoParametrosVM.ParametroList = parametros;
            _productoParametrosVM.ParametroFilter = parametros.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PARAMETRO.ToString()
                };
            });

            return View(_productoParametrosVM);
        }

        public IActionResult ClearFiltersProductoParametros()
        {
            return RedirectToAction("ProductoParametros");
        }

        public JsonResult SaveOrEditProductoParametros([FromBody] DtoProductoParametro data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_PRODUCTO_PARAMETRO))
                    {
                        var iD_STATUS = data.iD_STATUS?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO_PARAMETRO = data.iD_PRODUCTO_PARAMETRO?.Trim().Replace(" ", "").Split(",");
                        var iD_PARAMETRO = data.iD_PARAMETRO?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS?.Trim().Replace(" ", "").Split(",");
                        var nombre = data.nombre?.Trim().Replace(" ", "").Split(",");
                        var nombrE_ESP = data.nombrE_ESP?.Trim().Replace(" ", "").Split(",");

                        if (iD_PRODUCTO_PARAMETRO != null && nombre != null)
                        {
                            ProductoParametro _productoParametro = new ProductoParametro();

                            _productoParametro.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                            _productoParametro.iD_PRODUCTO_PARAMETRO = int.Parse(iD_PRODUCTO_PARAMETRO[0].ToString());
                            _productoParametro.iD_PARAMETRO = int.Parse(iD_PARAMETRO[0].ToString());
                            _productoParametro.iD_PRODUCTO = int.Parse(iD_PRODUCTO[0].ToString());
                            _productoParametro.clavE_PALS = clavE_PALS[0].ToString();
                            _productoParametro.nombre = nombre[0].ToString();
                            _productoParametro.nombrE_ESP = nombrE_ESP[0].ToString();
                            _productoParametro.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Producto";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<ProductoParametro, ProductoParametro>(_url, _productoParametro);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<ProductoParametro, ProductoParametro>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO_PARAMETRO = data.iD_PRODUCTO_PARAMETRO?.Trim().Replace(" ", "").Split(",");
                        var iD_PARAMETRO = data.iD_PARAMETRO?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS?.Trim().Replace(" ", "").Split(",");
                        var nombre = data.nombre?.Trim().Replace(" ", "").Split(",");
                        var nombrE_ESP = data.nombrE_ESP?.Trim().Replace(" ", "").Split(",");

                        if (clavE_PALS != null && nombre != null)
                        {
                            ProductoGrado _productoGrado = new ProductoGrado();

                            int resultiDStatus = 0;
                            int resultiDPRODUCTOPARAMETRO = 0;
                            int resultiDPARAMETRO = 0;
                            int resultiDPRODUCTO = 0;

                            if (Int32.TryParse(iD_STATUS[0].ToString(), out resultiDStatus))
                                _productoGrado.iD_STATUS = resultiDStatus;

                            if (Int32.TryParse(iD_PRODUCTO_PARAMETRO[0].ToString(), out resultiDPRODUCTOPARAMETRO))
                                _productoGrado.iD_PRODUCTO_GRADO = resultiDPRODUCTOPARAMETRO;

                            if (Int32.TryParse(iD_PARAMETRO[0].ToString(), out resultiDPARAMETRO))
                                _productoGrado.iD_GRADO = resultiDPARAMETRO;

                            if (Int32.TryParse(iD_PRODUCTO[0].ToString(), out resultiDPRODUCTO))
                                _productoGrado.iD_PRODUCTO = resultiDPRODUCTO;

                            //_productoGrado.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                            //_productoGrado.iD_PRODUCTO_PARAMETRO = int.Parse(iD_PRODUCTO_PARAMETRO[0].ToString());
                            //_productoGrado.iD_PARAMETRO = int.Parse(iD_PARAMETRO[0].ToString());
                            //_productoGrado.iD_PRODUCTO = int.Parse(iD_PRODUCTO[0].ToString());
                            _productoGrado.clavE_PALS = clavE_PALS[0].ToString();
                            _productoGrado.nombre = nombre[0].ToString();
                            _productoGrado.nombrE_ESP = nombrE_ESP[0].ToString();
                            _productoGrado.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Producto";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<ProductoGrado, ProductoGrado>(_url + "/" + _productoGrado.iD_PRODUCTO_GRADO, _productoGrado);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<ProductoGrado, ProductoGrado>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteProductoParametros(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "ProductoParametro";
                ProductoParametro model2 = new ProductoParametro();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<ProductoParametro, ProductoParametro>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<ProductoParametro>(JSONObj["data"]);

                model2.iD_PRODUCTO_PARAMETRO = result.iD_PRODUCTO_PARAMETRO;
                model2.iD_PARAMETRO = result.iD_PARAMETRO;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.clavE_PALS = result.clavE_PALS;
                model2.nombre = result.nombre;
                model2.nombrE_ESP = result.nombrE_ESP;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<ProductoParametro, ProductoParametro>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<ProductoParametro, ProductoParametro>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetProductoParametrosHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var productoParametros = getProductoParametros();
            var _url = _catalogCertificate.urlCatalogs + "ProductoParametro";

            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<ProductoParametro, ProductoParametro>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<ProductoParametro>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='productoParametros' class='form-control' >";

                foreach (var item in productoParametros)
                {
                    if (entity.iD_PRODUCTO_PARAMETRO == item.iD_PRODUCTO_PARAMETRO)
                        idTag += "<option value='" + item.iD_PRODUCTO_PARAMETRO + "' selected >" + item.nombre + " </option>";
                    else
                        idTag += "<option value='" + item.iD_PRODUCTO_PARAMETRO + "'>" + item.nombre + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='status' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_PRODUCTO_PARAMETRO + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' id='iD_PARAMETRO' type='text'  value='" + entity.iD_PARAMETRO + "'></td>" +
                "<td><input class='form-control' id='iD_PRODUCTO' type='text'  value='" + entity.iD_PRODUCTO + "'></td>" +
                "<td><input class='form-control' id='clavE_PALS' type='text'  value='" + entity.clavE_PALS + "'></td>" +
                "<td><input class='form-control' id='nombre' type='text'  value='" + entity.nombre + "'></td>" +
                "<td><input class='form-control' id='nombrE_ESP' type='text'  value='" + entity.nombrE_ESP + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Rol

        public IActionResult Rol()
        {
            var rol = getRol();

            RolViewModel _rolVM = new RolViewModel();

            _rolVM.RolList = rol;
            _rolVM.RolFilter = rol.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_ROL.ToString()
                };
            });

            return View(_rolVM);
        }

        public IActionResult ClearFiltersRol()
        {
            return RedirectToAction("Rol");
        }

        public JsonResult SaveOrEditRol([FromBody] DtoRol data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_ROL))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");                        
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_STATUS != null && descripcion != null)
                        {
                            Rol _rol = new Rol();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _rol.iD_STATUS = _resultiDSTATUS;
                            _rol.descripcion = descripcion;
                            _rol.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Rol";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Rol, Rol>(_url, _rol);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Rol, Rol>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ROL = data.iD_ROL; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_ROL != null && descripcion != null)
                        {
                            Rol _rol = new Rol();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _rol.iD_STATUS = _resultiDSTATUS;
                            _rol.iD_ROL = int.Parse(iD_ROL);
                            _rol.descripcion = descripcion;
                            _rol.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Rol";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Rol, Rol>(_url + "/" + _rol.iD_ROL, _rol);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Rol, Rol>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteRol(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Rol";
                Rol model2 = new Rol();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Rol, Rol>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Rol>(JSONObj["data"]);

                model2.iD_ROL = result.iD_ROL;
                model2.descripcion = result.descripcion;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Rol, Rol>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Rol, Rol>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetRolHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var rol = getRol();
            var _url = _catalogCertificate.urlCatalogs + "Rol";

            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Rol, Rol>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Rol>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_ROL' class='form-control' >";

                foreach (var item in rol)
                {
                    if (entity.iD_ROL == item.iD_ROL)
                        idTag += "<option value='" + item.iD_ROL + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_ROL + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_ROL + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Rol Alias

        public IActionResult RolAlias()
        {
            var rolAliasExportExce = getRolAlias();
            var rolAlias = getRolAlias();
            var rol = getRol();
            var paises = getPaises();

            RolAliasViewModel _rolAliasVM = new RolAliasViewModel();

            _rolAliasVM.RolAliasExportExcelList = rolAliasExportExce;
            _rolAliasVM.RolAliasExportExcelFilter = rolAliasExportExce.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_ROL.ToString()
                };
            });

            _rolAliasVM.RolAliasList = rolAlias.Where(x => x.iD_STATUS == 1).GroupBy(c => new
            {
                c.iD_ROL,
                //c.descripcion,
                c.iD_STATUS
            })
            .Select(gcs => new RolAlias()
            {
                iD_ROL = gcs.Key.iD_ROL,
                //descripcion = gcs.Key.descripcion,                
                iD_STATUS = gcs.Key.iD_STATUS
            }).ToList();

            _rolAliasVM.RolAliasFilter = rolAlias.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_ROL.ToString()
                };
            });

            _rolAliasVM.RolList = rol;
            _rolAliasVM.RolFilter = rol.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_ROL.ToString()
                };
            });

            _rolAliasVM.PaisesList = paises;
            _rolAliasVM.PaisesFilter = paises.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PAIS
                };
            });

            return View(_rolAliasVM);
        }

        public IActionResult ClearFiltersRolAlias()
        {
            return RedirectToAction("RolAlias");
        }

        public JsonResult SaveOrEditRolAlias([FromBody] DtoRolAlias data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_ROL_ALIAS))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_ROL_ALIAS = data.iD_ROL_ALIAS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ROL = data.iD_ROL; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PAIS = data.iD_PAIS; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_ROL != null && descripcion != null)
                        {
                            RolAlias _rolAlias = new RolAlias();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _rolAlias.iD_STATUS = _resultiDSTATUS;
                            //_rolAlias.iD_ROL_ALIAS = int.Parse(iD_ROL_ALIAS[0].ToString());
                            _rolAlias.iD_ROL = int.Parse(iD_ROL);
                            _rolAlias.iD_PAIS = iD_PAIS;
                            _rolAlias.descripcion = descripcion;
                            _rolAlias.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "RolAlias";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<RolAlias, RolAlias>(_url, _rolAlias);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<RolAlias, RolAlias>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ROL_ALIAS = data.iD_ROL_ALIAS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_ROL = data.iD_ROL; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PAIS = data.iD_PAIS; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_ROL_ALIAS != null && descripcion != null)
                        {
                            RolAlias _rolAlias = new RolAlias();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _rolAlias.iD_STATUS = _resultiDSTATUS;
                            _rolAlias.iD_ROL_ALIAS = int.Parse(iD_ROL_ALIAS);
                            _rolAlias.iD_ROL = int.Parse(iD_ROL);
                            _rolAlias.iD_PAIS = iD_PAIS;
                            _rolAlias.descripcion = descripcion;
                            _rolAlias.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "RolAlias";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<RolAlias, RolAlias>(_url + "/" + _rolAlias.iD_ROL_ALIAS, _rolAlias);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<RolAlias, RolAlias>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteRolAlias(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "RolAlias";
                RolAlias model2 = new RolAlias();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<RolAlias, RolAlias>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var response = JsonConvert.DeserializeObject<List<RolAlias>>(JSONObj["data"]);

                if (response != null && response.Any())
                {
                    foreach (var result in response)
                    {
                        model2.iD_ROL_ALIAS = result.iD_ROL_ALIAS;
                        model2.iD_ROL = result.iD_ROL;
                        model2.iD_PAIS = result.iD_PAIS;
                        model2.descripcion = result.descripcion;
                        model2.iD_STATUS = 0;
                        model2.usR_MODIFICA = 1;

                        _result = _rest.postApi<RolAlias, RolAlias>(_url + "/" + Id, model2);
                        JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                        var _error = JSONObj["error"];

                        if (_error.Trim() == "False")
                        {
                            ViewBag.Error = "false";
                            _result = _rest.getApi<RolAlias, RolAlias>(_url);
                            JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                        }
                        else
                        {
                            throw new Exception("El registro no existe");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetRolAliasHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var rolAlias = getRolAlias();
            var rol = getRol();
            var pais = getPaises();

            var _url = _catalogCertificate.urlCatalogs + "RolAlias";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<RolAlias, RolAlias>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var jsonResponse = JsonConvert.DeserializeObject<List<RolAlias>>(JSONObj["data"]);

            if (jsonResponse != null && jsonResponse.Any())
            {
                foreach (var entity in jsonResponse)
                {

                    var idTag = "<select  id='iD_ROL_ALIAS' class='form-control' >";

                    foreach (var item in rolAlias)
                    {
                        if (entity.iD_ROL_ALIAS == item.iD_ROL_ALIAS)
                            idTag += "<option value='" + item.iD_ROL_ALIAS + "' selected >" + item.descripcion + " </option>";
                        else
                            idTag += "<option value='" + item.iD_ROL_ALIAS + "'>" + item.descripcion + " </option>";
                    }

                    idTag += "</select>";

                    var rolTag = "<select  id='iD_ROL' class='form-control' >";

                    foreach (var item in rol)
                    {
                        if (entity.iD_ROL == item.iD_ROL)
                            rolTag += "<option value='" + item.iD_ROL + "' selected >" + item.descripcion + " </option>";
                        else
                            rolTag += "<option value='" + item.iD_ROL + "'>" + item.descripcion + " </option>";
                    }

                    rolTag += "</select>";

                    var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                    if (entity.iD_STATUS == 1)
                    {
                        estatusTag += "<option value='true' selected >Activo</option>";
                        estatusTag += "<option value='false' >Inactivo</option>";
                    }
                    else
                    {
                        estatusTag += "<option value='false' selected >Inactivo</option>";
                        estatusTag += "<option value='true' >Activo</option>";
                    }

                    estatusTag += "</select>";

                    var paisTag = "<select  id='iD_PAIS' class='form-control' >";

                    foreach (var item in pais)
                    {
                        if (entity.iD_PAIS == item.iD_PAIS)
                            paisTag += "<option value='" + item.iD_PAIS + "' selected >" + item.nombre + " </option>";
                        else
                            paisTag += "<option value='" + item.iD_PAIS + "'>" + item.nombre + " </option>";
                    }

                    paisTag += "</select>";

                    response += "<tr>" +
                    "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                            "<a href='javascript:void(0)' onclick='saveOnClickRolAlias(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_ROL_ALIAS + "' ><i class='fa fa-save'></i></a> </td>" +
                    "<td style='display: none;'>" + rolTag + "</td>" +
                    "<td>" + paisTag + "</td>" +
					"<td><input class='form-control' maxlength='50' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                    "<td>" + estatusTag + "</td>" +
                    "</tr>";
                }
            }

            return Json(new { Result = "Ok", Html = response });
        }

        public JsonResult GetRoleAliasById(string Id)
        {
            String response = String.Empty;

            var rolAlias = getRolAlias();
            var pais = getPaises();

            List<RolAlias> _lstRoleAlias = new List<RolAlias>();
            List<Paises> _lstCountry = new List<Paises>();

            if (!string.IsNullOrEmpty(Id))
                _lstRoleAlias = rolAlias.Where(x => x.iD_ROL == int.Parse(Id)).ToList();
            else
                _lstRoleAlias = new List<RolAlias>();

            if (pais != null && pais.Any())
                _lstCountry = pais;
            else
                _lstCountry = new List<Paises>();

            foreach (var item in _lstRoleAlias)
            {
                var idTag = "<select id='iD_ROL_ALIAS' class='form-control'>";

                if (item.iD_ROL_ALIAS == item.iD_ROL_ALIAS)
                    idTag += "<option value='" + item.iD_ROL_ALIAS + "' selected >" + item.descripcion + " </option>";
                else
                    idTag += "<option value='" + item.iD_ROL_ALIAS + "'>" + item.descripcion + " </option>";

                idTag += "</select>";

                //var estatusTag = "<select id='iD_STATUS' class='form-control'>";

                //if (item.iD_STATUS == 1)
                //{
                //    estatusTag += "<option value='true' selected >Activo</option>";
                //    estatusTag += "<option value='false'>Inactivo</option>";
                //}
                //else
                //{
                //    estatusTag += "<option value='false' selected >Inactivo</option>";
                //    estatusTag += "<option value='true'>Activo</option>";
                //}

                //estatusTag += "</select>";

                string _estatus = string.Empty;

                if (item.iD_STATUS == 1)
                    _estatus = "Activo";
                else
                    _estatus = "Inactivo";

                string _descripcion = string.Empty;

                if (item.iD_PAIS != null)
                    _descripcion = _lstCountry.FirstOrDefault(x => x.iD_PAIS == item.iD_PAIS).nombre;
                else
                    _descripcion = string.Empty;

                response +=
                "<tr>" +
                    "<td></td>" +
                    "<td style='display: none;'>" + item.iD_ROL + "</td>" +
                    "<td>" +
                        _descripcion +
                    //"<input class='form-control' id='" + item.iD_PAIS + "' type='text'  value='" + _descripcion + "'>" +
                    "</td>" +
                    "<td>" +
                        item.descripcion +
                    //"<input class='form-control' id='" + item.descripcion + "' type='text'  value='" + item.descripcion + "'>" +
                    "</td>" +
                    "<td>" +
                        _estatus +
                    "</td>" +
                    "<td>" +
                        "<a href='javascript:void(0)' onclick='deleteOnClickRolAlias(this); return false;' data-id='" + item.iD_ROL_ALIAS + "' class='btn btn-default btn-xs'>" +
                            "<i class='fa fa-trash-alt'></i>" +
                        "</a>" +
                        "<a href='javascript:void(0)' onclick='editOnClickRolAlias(this); return false;' id='editData' class='btn btn-default btn-xs' data-id='" + item.iD_ROL_ALIAS + "'>" +
                            "<i class='fa fa-edit'></i>" +
                        "</a>" +
                    //"<a href='javascript:void(0)' onclick='refresh();return false;' class='btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                    //"<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + item.iD_PAIS + "'><i class='fa fa-save'></i></a>" +
                    "</td>" +
                "</tr>";
            }

            String _tableHead = String.Empty;

            _tableHead +=
                                    //"<div id='content' class='content'>" +
                                    //    "<div class='section-container section-with-top-border p-b-5'>" +
                                    //       "<div class='row'>" +
                                    //            "<div class='panel panel-primary'>" +
                                    //"<div class='panel-heading'>" +
                                    //"<div class='btn-group pull-left'>" +
                                    //"<a href='javascript:void(0)' onclick='addEntryRolAlias(this); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                    //"<i class='fa fa-plus'></i> Agregar Registro" +
                                    //"</a>" +
                                    //"<a href='javascript:;' class='btn btn-white m-r-5' id='ExporttoExcel'>" +
                                    //    "<i class='fa fa-file-excel'></i> Exportar a excel" +
                                    //"</a>" +
                                    //"</div>" +
                                    //"<div class='btn-group pull-right'>" +
                                    //    "<a href='javascript:;' class='btn btn-white m-r-5' id='ClearFilters'>" +
                                    //        "<i class='fa fa-eraser'></i>Limpiar filtros" +
                                    //    "</a>" +
                                    //"</div>" +
                                    //"<h4 class='panel-title'>" +
                                    //"&nbsp;" +
                                    //"</h4>" +
                                    //"</div>" +

                                    //"<!-- Mensaje de error -->" +
                                    //"<div class='col-md-12' id='message'></div>" +

                                    //"<div class='panel-body'>" +
                                    //    "<div>" +
                                    "<table id='data-table-rol-alias-" + Id + "' class='table table-bordered table-hover'>" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<th class='no-sort' style='width:1%'></th>" +
                                                "<th style='white-space: nowrap; display: none;'>IdRole</th>" +
                                                "<th style='white-space: nowrap;'>País</th>" +
                                                "<th style='white-space: nowrap;'>Descripción</th>" +
                                                "<th style='white-space: nowrap;'>Estatus</th>" +
                                                "<th style='white-space: nowrap; width: 1%'>" +
                                                    "<a href='javascript:void(0)' onclick='addEntryRolAlias(this, " + Id + "); return false;' class='waves-effect waves-light btn btn-white m-r-5' id='add-entry'>" +
                                                        "<i class='fa fa-plus'></i>" +
                                                    "</a>" +
                                                "</th> " +
                                            "</tr>" +
                                        "</thead>";

            String _tableFoot = String.Empty;

            _tableFoot +=
                                    "</table>"; // +
            //                    "</div>" +
            //                "</div>" +
            //            "</div>" +
            //        "</div>" +
            //    "</div>" +
            //"</div";

            return Json(new { Result = "Ok", Html = _tableHead + "<tbody>" + response + "</tbody>" + _tableFoot });
        }

        #endregion

        #region Status Pipa

        public IActionResult StatusPipa()
        {
            var statusPipa = getStatusPipa();

            StatusPipaViewModel _statusPipaVM = new StatusPipaViewModel();

            _statusPipaVM.StatusPipasList = statusPipa;
            _statusPipaVM.StatusPipasFilter = statusPipa.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_STATUS_PIPA.ToString()
                };
            });

            return View(_statusPipaVM);
        }

        public IActionResult ClearFiltersStatusPipa()
        {
            return RedirectToAction("StatusPipa");
        }

        public JsonResult SaveOrEditStatusPipa([FromBody] DtoStatusPipas data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_STATUS_PIPA))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (descripcion != null && iD_STATUS != null)
                        {
                            StatusPipas _statusPipas = new StatusPipas();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _statusPipas.iD_STATUS = _resultiDSTATUS;
                            _statusPipas.descripcion = descripcion;
                            _statusPipas.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "StatusPipa";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<StatusPipas, StatusPipas>(_url, _statusPipas);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<StatusPipas, StatusPipas>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_STATUS_PIPA = data.iD_STATUS_PIPA; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (descripcion != null)
                        {
                            StatusPipas _statusPipas = new StatusPipas();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _statusPipas.iD_STATUS = _resultiDSTATUS;
                            _statusPipas.iD_STATUS_PIPA = int.Parse(iD_STATUS_PIPA);
                            _statusPipas.descripcion = descripcion;
                            _statusPipas.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "StatusPipa";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<StatusPipas, StatusPipas>(_url + "/" + _statusPipas.iD_STATUS_PIPA, _statusPipas);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<StatusPipas, StatusPipas>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteStatusPipa(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "StatusPipa";
                StatusPipas model2 = new StatusPipas();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<StatusPipas, StatusPipas>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<StatusPipas>(JSONObj["data"]);

                model2.iD_STATUS_PIPA = result.iD_STATUS_PIPA;
                model2.descripcion = result.descripcion;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<StatusPipas, StatusPipas>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<StatusPipas, StatusPipas>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetStatusPipaHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var statusPipa = getStatusPipa();
            var _url = _catalogCertificate.urlCatalogs + "StatusPipa";

            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<StatusPipas, StatusPipas>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<StatusPipas>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_STATUS_PIPA' class='form-control' >";

                foreach (var item in statusPipa)
                {
                    if (entity.iD_STATUS_PIPA == item.iD_STATUS_PIPA)
                        idTag += "<option value='" + item.iD_STATUS_PIPA + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_STATUS_PIPA + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_STATUS_PIPA + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' maxlength='50' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Tanque

        public IActionResult Tanque()
        {
            var tanque = getTanque();
            var plantas = getPlantas();
            var producto = getProductos();

            Models.CertCatalogosViewModels.TanqueViewModel _tanqueVM = new Models.CertCatalogosViewModels.TanqueViewModel();

            _tanqueVM.TanqueList = tanque;
            _tanqueVM.TanqueFilter = tanque.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TANQUE.ToString()
                };
            });

            _tanqueVM.PlantasList = plantas;
            _tanqueVM.PlantasFilter = plantas.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_PLANTA.ToString()
                };
            });

            _tanqueVM.ProductosList = producto;
            _tanqueVM.ProductosFilter = producto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            return View(_tanqueVM);
        }

        public IActionResult ClearFiltersTanque()
        {
            return RedirectToAction("Tanque");
        }

        public JsonResult SaveOrEditTanque([FromBody] DtoTanque data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_TANQUE))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_PLANTA != null && iD_PRODUCTO != null && descripcion != null)
                        {
                            Models.CertCatalogosViewModels.Tanque _tanque = new Models.CertCatalogosViewModels.Tanque();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _tanque.iD_STATUS = _resultiDSTATUS;
                            _tanque.descripcion = descripcion;
                            _tanque.iD_PLANTA = int.Parse(iD_PLANTA);
                            _tanque.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _tanque.clavE_PALS = clavE_PALS;
                            _tanque.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Tanque";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Models.CertCatalogosViewModels.Tanque, Models.CertCatalogosViewModels.Tanque>(_url, _tanque);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Models.CertCatalogosViewModels.Tanque, Models.CertCatalogosViewModels.Tanque>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TANQUE = data.iD_TANQUE; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PLANTA = data.iD_PLANTA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_TANQUE != null)
                        {
                            Models.CertCatalogosViewModels.Tanque _tanque = new Models.CertCatalogosViewModels.Tanque();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _tanque.iD_STATUS = _resultiDSTATUS;
                            _tanque.iD_TANQUE = int.Parse(iD_TANQUE);
                            _tanque.iD_PLANTA = int.Parse(iD_PLANTA);
                            _tanque.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _tanque.descripcion = descripcion;
                            _tanque.clavE_PALS = clavE_PALS;
                            _tanque.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Tanque";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Models.CertCatalogosViewModels.Tanque, Models.CertCatalogosViewModels.Tanque>(_url + "/" + _tanque.iD_TANQUE, _tanque);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Models.CertCatalogosViewModels.Tanque, Models.CertCatalogosViewModels.Tanque>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteTanque(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Tanque";
                Models.CertCatalogosViewModels.Tanque model2 = new Models.CertCatalogosViewModels.Tanque();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Models.CertCatalogosViewModels.Tanque, Models.CertCatalogosViewModels.Tanque>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Models.CertCatalogosViewModels.Tanque>(JSONObj["data"]);

                model2.iD_TANQUE = result.iD_TANQUE;
                model2.descripcion = result.descripcion;
                model2.iD_PLANTA = result.iD_PLANTA;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.clavE_PALS = result.clavE_PALS;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Models.CertCatalogosViewModels.Tanque, Models.CertCatalogosViewModels.Tanque>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Models.CertCatalogosViewModels.Tanque, Models.CertCatalogosViewModels.Tanque>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetTanqueHTMLTagsById(string Id)
        {
            String response = String.Empty;
            var tanque = getTanque();
            var plants = getPlantas();
            var product = getProductos();
            var _url = _catalogCertificate.urlCatalogs + "Tanque";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Models.CertCatalogosViewModels.Tanque, Models.CertCatalogosViewModels.Tanque>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Models.CertCatalogosViewModels.Tanque>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_TANQUE' class='form-control' >";

                foreach (var item in tanque)
                {
                    if (entity.iD_TANQUE == item.iD_TANQUE)
                        idTag += "<option value='" + item.iD_TANQUE + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_TANQUE + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var plantasTag = "<select  id='iD_PLANTA' class='form-control' >";

                foreach (var item in plants)
                {
                    if (entity.iD_PLANTA == item.iD_PLANTA)
                        plantasTag += "<option value='" + item.iD_PLANTA + "' selected >" + item.descripcion + " </option>";
                    else
                        plantasTag += "<option value='" + item.iD_PLANTA + "'>" + item.descripcion + " </option>";
                }

                plantasTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var productosTag = "<select  id='iD_PRODUCTO' class='form-control' >";

                foreach (var item in product)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        productosTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        productosTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                productosTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_TANQUE + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' id='descripcion' maxlength='20' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td><input class='form-control' id='clavE_PALS' maxlength='100' type='text'  value='" + entity.clavE_PALS + "'></td>" +
                "<td>" + plantasTag + "</td>" +
                "<td>" + productosTag + "</td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Tipo Especificacion

        public IActionResult TipoEspecificacion()
        {
            var tipoEspecificacion = getTipoEspecificacion();

            TipoEspecificacionViewModel _tipoEspecificacionVM = new TipoEspecificacionViewModel();

            _tipoEspecificacionVM.TipoEspecificacionList = tipoEspecificacion;
            _tipoEspecificacionVM.TipoEspecificacionFilter = tipoEspecificacion.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO_ESPECIFICACION.ToString()
                };
            });

            return View(_tipoEspecificacionVM);
        }

        public IActionResult ClearFiltersTipoEspecificacion()
        {
            return RedirectToAction("TipoEspecificacion");
        }

        public JsonResult SaveOrEditTipoEspecificacion([FromBody] DtoTipoEspecificacion data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_TIPO_ESPECIFICACION))
                    {
                        var iD_STATUS = data.iD_STATUS?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_ESPECIFICACION = data.iD_TIPO_ESPECIFICACION?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion?.Trim().Replace(" ", "").Split(",");

                        if (iD_TIPO_ESPECIFICACION != null && descripcion != null)
                        {
                            TipoEspecificacion _tipoEspecificacion = new TipoEspecificacion();

                            _tipoEspecificacion.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                            _tipoEspecificacion.iD_TIPO_ESPECIFICACION = int.Parse(iD_TIPO_ESPECIFICACION[0].ToString());
                            _tipoEspecificacion.descripcion = descripcion[0].ToString();
                            _tipoEspecificacion.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "TipoEspecificacion";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<TipoEspecificacion, TipoEspecificacion>(_url, _tipoEspecificacion);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<TipoEspecificacion, TipoEspecificacion>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_ESPECIFICACION = data.iD_TIPO_ESPECIFICACION?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion?.Trim().Replace(" ", "").Split(",");

                        if (iD_TIPO_ESPECIFICACION != null && descripcion != null)
                        {
                            TipoEspecificacion _tipoEspecificacion = new TipoEspecificacion();

                            int resultiDStatus = 0;
                            int resultiDTIPOESPECIFICACION = 0;

                            if (Int32.TryParse(iD_STATUS[0].ToString(), out resultiDStatus))
                                _tipoEspecificacion.iD_STATUS = resultiDStatus;

                            if (Int32.TryParse(iD_TIPO_ESPECIFICACION[0].ToString(), out resultiDTIPOESPECIFICACION))
                                _tipoEspecificacion.iD_TIPO_ESPECIFICACION = resultiDTIPOESPECIFICACION;

                            //_tipoEspecificacion.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                            //_tipoEspecificacion.iD_ROL = int.Parse(iD_ROL[0].ToString());
                            _tipoEspecificacion.descripcion = descripcion[0].ToString();
                            _tipoEspecificacion.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "TipoEspecificacion";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<TipoEspecificacion, TipoEspecificacion>(_url + "/" + _tipoEspecificacion.iD_TIPO_ESPECIFICACION, _tipoEspecificacion);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<TipoEspecificacion, TipoEspecificacion>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteTipoEspecificacion(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "TipoEspecificacion";
                TipoEspecificacion model2 = new TipoEspecificacion();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<TipoEspecificacion, TipoEspecificacion>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<TipoEspecificacion>(JSONObj["data"]);

                model2.iD_TIPO_ESPECIFICACION = result.iD_TIPO_ESPECIFICACION;
                model2.descripcion = result.descripcion;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<TipoEspecificacion, TipoEspecificacion>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<TipoEspecificacion, TipoEspecificacion>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetTipoEspecificacionHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var tipoEspecificacion = getTipoEspecificacion();
            var _url = _catalogCertificate.urlCatalogs + "TipoEspecificacion";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<TipoEspecificacion, TipoEspecificacion>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<TipoEspecificacion>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='tipoEspecificacion' class='form-control' >";

                foreach (var item in tipoEspecificacion)
                {
                    if (entity.iD_TIPO_ESPECIFICACION == item.iD_TIPO_ESPECIFICACION)
                        idTag += "<option value='" + item.iD_TIPO_ESPECIFICACION + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_TIPO_ESPECIFICACION + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='status' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_TIPO_ESPECIFICACION + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Tipo Suministro

        public IActionResult TipoSuministro()
        {
            var tSuministro = getTipoSuministro();

            TipoSuministroViewModel _tSuministroVM = new TipoSuministroViewModel();

            _tSuministroVM.TipoSuministrosList = tSuministro;
            _tSuministroVM.TipoSuministrosFilter = tSuministro.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO_SUMINISTRO.ToString()
                };
            });

            return View(_tSuministroVM);
        }

        public IActionResult ClearFiltersTipoSuministro()
        {
            return RedirectToAction("TipoSuministro");
        }

        public JsonResult SaveOrEditTipoSuministro([FromBody] DtoTipoSuministros data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_TIPO_SUMINISTRO))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_SUMINISTRO = data.iD_TIPO_SUMINISTRO; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (descripcion != null && iD_TIPO_SUMINISTRO != null)
                        {
                            TipoSuministros _tSuministro = new TipoSuministros();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "1")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _tSuministro.iD_STATUS = _resultiDSTATUS;
                            //_tSuministro.iD_STATUS = int.Parse(iD_STATUS[0].ToString());
                            //_tSuministro.iD_TIPO_SUMINISTRO = int.Parse(iD_TIPO_SUMINISTRO[0].ToString());
                            _tSuministro.descripcion = descripcion;
                            _tSuministro.identificador = "";
                            _tSuministro.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "TipoSuministro";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<TipoSuministros, TipoSuministros>(_url, _tSuministro);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<TipoSuministros, TipoSuministros>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                //ViewBag.Error = "true";
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_SUMINISTRO = data.iD_TIPO_SUMINISTRO; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");

                        if (descripcion != null && iD_TIPO_SUMINISTRO != null)
                        {
                            TipoSuministros _tSuministro = new TipoSuministros();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            //int resultiDStatus = 0;

                            //if (Int32.TryParse(iD_STATUS[0].ToString(), out resultiDStatus))
                            //    _tSuministro.iD_STATUS = resultiDStatus;

                            _tSuministro.iD_STATUS = _resultiDSTATUS;
                            _tSuministro.iD_TIPO_SUMINISTRO = int.Parse(iD_TIPO_SUMINISTRO);
                            _tSuministro.descripcion = descripcion;
                            _tSuministro.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "TipoSuministro";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<TipoSuministros, TipoSuministros>(_url + "/" + _tSuministro.iD_TIPO_SUMINISTRO, _tSuministro);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<TipoSuministros, TipoSuministros>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteTipoSuministro(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "TipoSuministro";
                TipoSuministros model2 = new TipoSuministros();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<TipoSuministros, TipoSuministros>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<TipoSuministros>(JSONObj["data"]);

                model2.iD_TIPO_SUMINISTRO = result.iD_TIPO_SUMINISTRO;
                model2.descripcion = result.descripcion;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<TipoSuministros, TipoSuministros>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<TipoSuministros, TipoSuministros>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetTipoSuministroHTMLTagsById(string Id)
        {
            String response = String.Empty;
            var tSuministro = getTipoSuministro();
            var _url = _catalogCertificate.urlCatalogs + "TipoSuministro";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<TipoSuministros, TipoSuministros>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<TipoSuministros>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_TIPO_SUMINISTRO' class='form-control' >";

                foreach (var item in tSuministro)
                {
                    if (entity.iD_TIPO_SUMINISTRO == item.iD_TIPO_SUMINISTRO)
                        idTag += "<option value='" + item.iD_TIPO_SUMINISTRO + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_TIPO_SUMINISTRO + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_TIPO_SUMINISTRO + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' maxlength='50' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Tipo Transporte

        public IActionResult TipoTransporte()
        {
            var tipoTransporte = getTipoTransporte();

            TipoTransporteViewModel _tipoTransporteVM = new TipoTransporteViewModel();

            _tipoTransporteVM.TipoTransporteList = tipoTransporte;
            _tipoTransporteVM.TipoTransporteFilter = tipoTransporte.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO_TRANSPORTE.ToString()
                };
            });

            return View(_tipoTransporteVM);
        }

        public IActionResult ClearFiltersTipoTransporte()
        {
            return RedirectToAction("TipoTransporte");
        }

        public JsonResult SaveOrEditTipoTransporte([FromBody] DtoTipoTransporte data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_TIPO_TRANSPORTE))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var analizar = data.analizar; //?.Trim().Replace(" ", "").Split(",");

                        if (descripcion != null && analizar != null && iD_STATUS != null)
                        {
                            TipoTransporte _tipoTransporte = new TipoTransporte();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _tipoTransporte.iD_STATUS = _resultiDSTATUS;
                            _tipoTransporte.descripcion = descripcion;
                            _tipoTransporte.analizar = analizar;
                            _tipoTransporte.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "TipoTransporte";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<TipoTransporte, TipoTransporte>(_url, _tipoTransporte);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<TipoTransporte, TipoTransporte>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_TRANSPORTE = data.iD_TIPO_TRANSPORTE; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var analizar = data.analizar; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_TIPO_TRANSPORTE != null && descripcion != null && analizar != null && iD_STATUS != null)
                        {
                            TipoTransporte _tipoTransporte = new TipoTransporte();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _tipoTransporte.iD_STATUS = _resultiDSTATUS;
                            _tipoTransporte.iD_TIPO_TRANSPORTE = int.Parse(iD_TIPO_TRANSPORTE);
                            _tipoTransporte.descripcion = descripcion;
                            _tipoTransporte.analizar = analizar;
                            _tipoTransporte.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "TipoTransporte";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<TipoTransporte, TipoTransporte>(_url + "/" + _tipoTransporte.iD_TIPO_TRANSPORTE, _tipoTransporte);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<TipoTransporte, TipoTransporte>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteTipoTransporte(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "TipoTransporte";
                TipoTransporte model2 = new TipoTransporte();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<TipoTransporte, TipoTransporte>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<TipoTransporte>(JSONObj["data"]);

                model2.iD_TIPO_TRANSPORTE = result.iD_TIPO_TRANSPORTE;
                model2.descripcion = result.descripcion;
                model2.analizar = result.analizar;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<TipoTransporte, TipoTransporte>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<TipoTransporte, TipoTransporte>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetTipoTransporteHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var tipoTransporte = getTipoTransporte();
            var _url = _catalogCertificate.urlCatalogs + "TipoTransporte";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<TipoTransporte, TipoTransporte>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<TipoTransporte>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_TIPO_TRANSPORTE' class='form-control' >";

                foreach (var item in tipoTransporte)
                {
                    if (entity.iD_TIPO_TRANSPORTE == item.iD_TIPO_TRANSPORTE)
                        idTag += "<option value='" + item.iD_TIPO_TRANSPORTE + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_TIPO_TRANSPORTE + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_TIPO_TRANSPORTE + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' maxlength='50' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td><input class='form-control' maxlength='1' id='analizar' type='text'  value='" + entity.analizar + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Transporte

        public IActionResult Transporte()
        {
            var transporte = getTransporte();
            var tipoTransporte = getTipoTransporte();
            var paises = getPaises();
            var producto = getProductos();
            var statusPipa = getStatusPipa();

            TransporteViewModel _transporteVM = new TransporteViewModel();

            _transporteVM.TransporteList = transporte;
            _transporteVM.TransporteFilter = transporte.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TRANSPORTE.ToString()
                };
            });

            _transporteVM.TipoTransporteList = tipoTransporte;
            _transporteVM.TipoTransporteFilter = tipoTransporte.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_TIPO_TRANSPORTE.ToString()
                };
            });

            _transporteVM.PaisesList = paises;
            _transporteVM.PaisesFilter = paises.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PAIS
                };
            });

            _transporteVM.ProductosList = producto;
            _transporteVM.ProductosFilter = producto.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nombre,
                    Value = a.iD_PRODUCTO.ToString()
                };
            });

            _transporteVM.StatusPipasList = statusPipa;
            _transporteVM.StatusPipasFilter = statusPipa.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_STATUS_PIPA.ToString()
                };
            });

            return View(_transporteVM);
        }

        public IActionResult ClearFiltersTransporte()
        {
            return RedirectToAction("Transporte");
        }

        public JsonResult SaveOrEditTransporte([FromBody] DtoTransporte data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_TRANSPORTE))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_TRANSPORTE = data.iD_TRANSPORTE?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_TRANSPORTE = data.iD_TIPO_TRANSPORTE; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PAIS = data.iD_PAIS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_STATUS_PIPA = data.iD_STATUS_PIPA?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_TIPO_TRANSPORTE != null && iD_PAIS != null && descripcion != null && clavE_PALS != null && iD_PRODUCTO != null)
                        {
                            Transporte _transporte = new Transporte();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _transporte.iD_STATUS = _resultiDSTATUS;
                            //_transporte.iD_TRANSPORTE = int.Parse(iD_TRANSPORTE[0].ToString());
                            _transporte.iD_TIPO_TRANSPORTE = int.Parse(iD_TIPO_TRANSPORTE);
                            _transporte.iD_PAIS = iD_PAIS;
                            _transporte.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            _transporte.iD_STATUS_PIPA = 0; // int.Parse(iD_STATUS_PIPA[0].ToString());
                            _transporte.descripcion = descripcion;
                            _transporte.clavE_PALS = clavE_PALS;
                            _transporte.usR_ALTA = 1;
                            _transporte.iD_GRADO = "1";

                            var _url = _catalogCertificate.urlCatalogs + "Transporte";

                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Transporte, Transporte>(_url, _transporte);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Transporte, Transporte>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row

                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TRANSPORTE = data.iD_TRANSPORTE; //?.Trim().Replace(" ", "").Split(",");
                        var iD_TIPO_TRANSPORTE = data.iD_TIPO_TRANSPORTE; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PAIS = data.iD_PAIS; //?.Trim().Replace(" ", "").Split(",");
                        var iD_PRODUCTO = data.iD_PRODUCTO; //?.Trim().Replace(" ", "").Split(",");
                        //var iD_STATUS_PIPA = data.iD_STATUS_PIPA?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");

                        if (iD_TRANSPORTE != null && descripcion != null)
                        {
                            Transporte _transporte = new Transporte();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            _transporte.iD_STATUS = _resultiDSTATUS;
                            _transporte.iD_TRANSPORTE = int.Parse(iD_TRANSPORTE);
                            _transporte.iD_TIPO_TRANSPORTE = int.Parse(iD_TIPO_TRANSPORTE);
                            _transporte.iD_PAIS = iD_PAIS;
                            _transporte.iD_PRODUCTO = int.Parse(iD_PRODUCTO);
                            //_transporte.iD_STATUS_PIPA = int.Parse(iD_STATUS_PIPA[0].ToString());
                            _transporte.descripcion = descripcion;
                            _transporte.clavE_PALS = clavE_PALS;
                            _transporte.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "Transporte";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<Transporte, Transporte>(_url + "/" + _transporte.iD_TRANSPORTE, _transporte);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<Transporte, Transporte>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteTransporte(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "Transporte";
                Transporte model2 = new Transporte();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<Transporte, Transporte>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<Transporte>(JSONObj["data"]);

                model2.iD_TRANSPORTE = result.iD_TRANSPORTE;
                model2.iD_TIPO_TRANSPORTE = result.iD_TIPO_TRANSPORTE;
                model2.iD_PAIS = result.iD_PAIS;
                model2.iD_PRODUCTO = result.iD_PRODUCTO;
                model2.iD_STATUS_PIPA = result.iD_STATUS_PIPA;
                model2.descripcion = result.descripcion;
                model2.clavE_PALS = result.clavE_PALS;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<Transporte, Transporte>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);

                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<Transporte, Transporte>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetTransporteHTMLTagsById(string Id)
        {
            String response = String.Empty;

            var transporte = getTransporte();
            var typeTransport = getTipoTransporte();
            var country = getPaises();
            var producto = getProductos();
            var statusPipa = getStatusPipa();
            var _url = _catalogCertificate.urlCatalogs + "Transporte";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<Transporte, Transporte>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<Transporte>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_TRANSPORTE' class='form-control' >";

                foreach (var item in transporte)
                {
                    if (entity.iD_TRANSPORTE == item.iD_TRANSPORTE)
                        idTag += "<option value='" + item.iD_TRANSPORTE + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_TRANSPORTE + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var tipoTransporteTag = "<select  id='iD_TIPO_TRANSPORTE' class='form-control' >";

                foreach (var item in typeTransport)
                {
                    if (entity.iD_TIPO_TRANSPORTE == item.iD_TIPO_TRANSPORTE)
                        tipoTransporteTag += "<option value='" + item.iD_TIPO_TRANSPORTE + "' selected >" + item.descripcion + " </option>";
                    else
                        tipoTransporteTag += "<option value='" + item.iD_TIPO_TRANSPORTE + "'>" + item.descripcion + " </option>";
                }

                tipoTransporteTag += "</select>";

                var paisesTag = "<select  id='iD_PAIS' class='form-control' >";

                foreach (var item in country)
                {
                    if (entity.iD_PAIS == item.iD_PAIS)
                        paisesTag += "<option value='" + item.iD_PAIS + "' selected >" + item.nombre + " </option>";
                    else
                        paisesTag += "<option value='" + item.iD_PAIS + "'>" + item.nombre + " </option>";
                }

                paisesTag += "</select>";

                var productoTag = "<select  id='iD_PRODUCTO' class='form-control' >";

                foreach (var item in producto)
                {
                    if (entity.iD_PRODUCTO == item.iD_PRODUCTO)
                        productoTag += "<option value='" + item.iD_PRODUCTO + "' selected >" + item.nombre + " </option>";
                    else
                        productoTag += "<option value='" + item.iD_PRODUCTO + "'>" + item.nombre + " </option>";
                }

                productoTag += "</select>";

                var statusPipaTag = "<select  id='iD_STATUS_PIPA' class='form-control' >";

                foreach (var item in statusPipa)
                {
                    if (entity.iD_STATUS_PIPA == item.iD_STATUS_PIPA)
                        statusPipaTag += "<option value='" + item.iD_STATUS_PIPA + "' selected >" + item.descripcion + " </option>";
                    else
                        statusPipaTag += "<option value='" + item.iD_STATUS_PIPA + "'>" + item.descripcion + " </option>";
                }

                statusPipaTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_TRANSPORTE + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td>" + tipoTransporteTag + "</td>" +
                "<td>" + paisesTag + "</td>" +
                "<td><input class='form-control' maxlength='20' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td><input class='form-control' maxlength='30' id='clavE_PALS' type='text'  value='" + entity.clavE_PALS + "'></td>" +
                "<td>" + productoTag + "</td>" +
                "<td style='display: none;'>" + statusPipaTag + "</td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Unidad de Medida

        public IActionResult UnidadMedida()
        {
            var unidadMedida = getUnidadMedidas();
            UnidadMedidaViewModel _unidadMedidaVM = new UnidadMedidaViewModel();

            _unidadMedidaVM.UnidadMedidasList = unidadMedida;
            _unidadMedidaVM.UnidadMedidasFilter = unidadMedida.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.descripcion,
                    Value = a.iD_UNIDAD_MEDIDA.ToString()
                };
            });

            return View(_unidadMedidaVM);
        }

        public IActionResult ClearFiltersUnidadMedida()
        {
            return RedirectToAction("UnidadMedida");
        }

        public JsonResult SaveOrEditUnidadMedida([FromBody] DtoUnidadMedidas data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.iD_UNIDAD_MEDIDA))
                    {
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var binario = data.binario; //?.Trim().Replace(" ", "").Split(",");
                        var tipo = data.tipo; //?.Trim().Replace(" ", "").Split(",");

                        if (clavE_PALS != null && descripcion != null)
                        {
                            UnidadMedidas _unidadMedida = new UnidadMedidas();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            string _binario = binario;

                            bool _resultbinario;

                            if (_binario == "true")
                                _resultbinario = true;
                            else
                                _resultbinario = false;

                            _unidadMedida.iD_STATUS = _resultiDSTATUS;
                            _unidadMedida.clavE_PALS = clavE_PALS;
                            _unidadMedida.descripcion = descripcion;
                            _unidadMedida.binario = _resultbinario;
                            _unidadMedida.tipo = int.Parse(tipo);
                            _unidadMedida.usR_ALTA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "UnidadMedida";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<UnidadMedidas, UnidadMedidas>(_url, _unidadMedida);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<UnidadMedidas, UnidadMedidas>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                    else
                    {
                        //edit selected row
                        var iD_UNIDAD_MEDIDA = data.iD_UNIDAD_MEDIDA; //?.Trim().Replace(" ", "").Split(",");
                        var iD_STATUS = data.iD_STATUS; //?.Trim().Replace(" ", "").Split(",");
                        var clavE_PALS = data.clavE_PALS; //?.Trim().Replace(" ", "").Split(",");
                        var descripcion = data.descripcion; //?.Trim().Replace(" ", "").Split(",");
                        var binario = data.binario; //?.Trim().Replace(" ", "").Split(",");
                        var tipo = data.tipo; //?.Trim().Replace(" ", "").Split(",");

                        if (clavE_PALS != null && descripcion != null)
                        {
                            UnidadMedidas _unidadMedida = new UnidadMedidas();

                            string _iDSTATUS = iD_STATUS;

                            int _resultiDSTATUS;

                            if (_iDSTATUS == "true")
                                _resultiDSTATUS = 1;
                            else
                                _resultiDSTATUS = 0;

                            string _binario = binario;

                            bool _resultbinario;

                            if (_binario == "true")
                                _resultbinario = true;
                            else
                                _resultbinario = false;

                            _unidadMedida.iD_STATUS = _resultiDSTATUS;
                            _unidadMedida.iD_UNIDAD_MEDIDA = int.Parse(iD_UNIDAD_MEDIDA);
                            _unidadMedida.clavE_PALS = clavE_PALS;
                            _unidadMedida.descripcion = descripcion;
                            _unidadMedida.binario = _resultbinario;
                            _unidadMedida.tipo = int.Parse(tipo);
                            _unidadMedida.usR_MODIFICA = 1;

                            var _url = _catalogCertificate.urlCatalogs + "UnidadMedida";
                            RestGenerico _rest = new RestGenerico();
                            var _result = _rest.postApi<UnidadMedidas, UnidadMedidas>(_url + "/" + _unidadMedida.iD_UNIDAD_MEDIDA, _unidadMedida);
                            JsonDeserializer deserial = new JsonDeserializer();
                            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            var _error = JSONObj["error"];

                            if (_error.Trim() == "False")
                            {
                                _result = _rest.getApi<UnidadMedidas, UnidadMedidas>(_url);
                                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                            }
                            else
                            {
                                throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult DeleteUnidadMedida(String Id)
        {
            try
            {
                var _url = _catalogCertificate.urlCatalogs + "UnidadMedida";
                UnidadMedidas model2 = new UnidadMedidas();
                RestGenerico _rest = new RestGenerico();
                var _result = _rest.getApi<UnidadMedidas, UnidadMedidas>(_url + "/" + Id);
                JsonDeserializer deserial = new JsonDeserializer();
                var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var result = JsonConvert.DeserializeObject<UnidadMedidas>(JSONObj["data"]);

                model2.iD_UNIDAD_MEDIDA = result.iD_UNIDAD_MEDIDA;
                model2.descripcion = result.descripcion;
                model2.clavE_PALS = result.clavE_PALS;
                model2.binario = result.binario;
                model2.tipo = result.tipo;
                model2.iD_STATUS = 0;
                model2.usR_MODIFICA = 1;

                _result = _rest.postApi<UnidadMedidas, UnidadMedidas>(_url + "/" + Id, model2);
                JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                var _error = JSONObj["error"];

                if (_error.Trim() == "False")
                {
                    ViewBag.Error = "false";
                    _result = _rest.getApi<UnidadMedidas, UnidadMedidas>(_url);
                    JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        public JsonResult GetUnidadMedidaHTMLTagsById(string Id)
        {
            String response = String.Empty;
            var unidadMedida = getUnidadMedidas();
            var _url = _catalogCertificate.urlCatalogs + "UnidadMedida";
            RestGenerico _rest = new RestGenerico();
            var _result = _rest.getApi<UnidadMedidas, UnidadMedidas>(_url + "/" + Id);
            JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(_result);
            var entity = JsonConvert.DeserializeObject<UnidadMedidas>(JSONObj["data"]);

            if (entity != null)
            {
                var idTag = "<select  id='iD_UNIDAD_MEDIDA' class='form-control' >";

                foreach (var item in unidadMedida)
                {
                    if (entity.iD_UNIDAD_MEDIDA == item.iD_UNIDAD_MEDIDA)
                        idTag += "<option value='" + item.iD_UNIDAD_MEDIDA + "' selected >" + item.descripcion + " </option>";
                    else
                        idTag += "<option value='" + item.iD_UNIDAD_MEDIDA + "'>" + item.descripcion + " </option>";
                }

                idTag += "</select>";

                var estatusTag = "<select  id='iD_STATUS' class='form-control'>";

                if (entity.iD_STATUS == 1)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";

                var binarioTag = "<select  id='binario' class='form-control'>";

                if (entity.binario == true)
                {
                    binarioTag += "<option value='true' selected >Sí</option>";
                    binarioTag += "<option value='false' >No</option>";
                }
                else
                {
                    binarioTag += "<option value='false' selected >No</option>";
                    binarioTag += "<option value='true' >Sí</option>";
                }

                binarioTag += "</select>";

                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.iD_UNIDAD_MEDIDA + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' maxlength='20' id='descripcion' type='text'  value='" + entity.descripcion + "'></td>" +
                "<td><input class='form-control' maxlength='10' id='clavE_PALS' type='text'  value='" + entity.clavE_PALS + "'></td>" +
                 "<td>" + binarioTag + "</td>" +
                "<td><input class='form-control' id='tipo' type='text'  value='" + entity.tipo + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }

            return Json(new { Result = "Ok", Html = response });
        }

        #endregion

        #region Private Methods 
        private int ParseStatus(string status)
        {
            return status == "1" || status.Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
        }

        private int ParseMandatoryInt(string value, string errorMessage)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new FormatException(errorMessage);
        }

        private int? ParseNullableInt(string value)
        {
            return int.TryParse(value, out int result) ? result : (int?)null;
        }
        #endregion
    }
}
