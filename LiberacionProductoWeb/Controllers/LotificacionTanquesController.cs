﻿
using DevExpress.DataAccess.Native.Web;
using LiberacionProducto.Entities.Entities.Lotificacion;
using LiberacionProducto.Services.Implementation.Lotificacion;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.Lotificacion;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LiberacionProducto.Entities.Entities.Lotificacion.ListadoLotificacion;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace LiberacionProductoWeb.Controllers
{
    public class LotificacionTanquesController : Controller
    {
        private readonly ILogger<AccessController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        IStringLocalizer<Localize.Resource> _resource;
        private readonly IPrincipalService _principalService;
        private readonly LotificacionService _lotificacionService;


        public LotificacionTanquesController(
            ILogger<AccessController> logger,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<Localize.Resource> resource,
            IConfiguration config,
            IPrincipalService principalService,
            LotificacionService lotificacionService
            )
        {
            _logger = logger;
            _userManager = userManager;
            _resource = resource;
            _config = config;
            _principalService = principalService;
            _lotificacionService = lotificacionService;
        }

        public IConfiguration _config { get; }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Lotificacion()
        {
            LotificacionViewModel model = new LotificacionViewModel();
            model.Plant = new Models.ConfigViewModels.Plant();
            model.Product = new Models.CatalogsViewModels.Product();

            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (userInfo != null)
                {
                    model.ExternalId = userInfo.ExternalId;
                }

                model.FechaActual = DateTime.Now;
                var plantaxUsuario = await _lotificacionService.GetPlantasUsuario(userInfo.MexeUsuario);
                var plantas = await _lotificacionService.GetPlantasAsync(plantaxUsuario.PlantasUsuario);
                model.CatPlanta = plantas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListadoLotes()
        {
            LotificacionViewModel model = new LotificacionViewModel();
            model.Plant = new Models.ConfigViewModels.Plant();
            model.Product = new Models.CatalogsViewModels.Product();

            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (userInfo != null)
                {
                    model.ExternalId = userInfo.ExternalId;
                    model.NombreUsuario = userInfo.NombreUsuario;
                }

                var plantaxUsuario = await _lotificacionService.GetPlantasUsuario(userInfo.MexeUsuario);
                var plantas = await _lotificacionService.GetPlantasAsync(plantaxUsuario.PlantasUsuario);
                model.CatPlanta = plantas;

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorPrincipal"));
                _logger.LogError(ex, "Error.");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PruebaUltima()
        {
            LotificacionViewModel model = new LotificacionViewModel();
            model.Plant = new Models.ConfigViewModels.Plant();
            model.Product = new Models.CatalogsViewModels.Product();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> GenerarLote(LotificacionData data)
        {
            try
            {
                var descPlanta = _lotificacionService.GetDescripcionPlanta(data.MasterData.IdPlanta);
                var descProducto = _lotificacionService.GetDescripcionProducto(Convert.ToInt32(data.MasterData.IdProducto));

                string IdLoteNuevo = _lotificacionService.GeneraIdLote(
                    data.MasterData.IdProducto, descProducto.Result.DescProducto.Trim(), data.MasterData.IdPlanta, descPlanta.Result.DescPlanta.Trim(), data.MasterData.IdTanqueDesc);
                data.MasterData.IdLote = IdLoteNuevo;

                var retVal = _lotificacionService.GuardarDatos(data);
                if (string.IsNullOrEmpty(retVal) || retVal.Contains("Error"))
                {
                    return BadRequest(retVal);
                }
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al guardar los datos: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditarLote(LotificacionData data)
        {
            try
            {
                //var descPlanta = _lotificacionService.GetDescripcionPlanta(data.MasterData.IdPlanta);
                //var descProducto = _lotificacionService.GetDescripcionProducto(Convert.ToInt32(data.MasterData.IdProducto));

                //string IdLoteNuevo = _lotificacionService.GeneraIdLote(
                //    data.MasterData.IdProducto, descProducto.Result.DescProducto.Trim(), data.MasterData.IdPlanta, descPlanta.Result.DescPlanta.Trim(), data.MasterData.IdTanqueDesc);
                //data.MasterData.IdLote = IdLoteNuevo;

                var retVal = _lotificacionService.EditarDatosLote(data);
                if (string.IsNullOrEmpty(retVal) || retVal.Contains("Error"))
                {
                    return BadRequest(retVal);
                }
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al guardar los datos: " + ex.Message);
            }
        }



        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CancelarLote(CancelarLoteData data)
        {
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (userInfo != null)
                {
                    if (data.UserCancel == userInfo.ExternalId)
                    {
                        var retVal = _lotificacionService.CancelarLote(data);
                        return Ok(retVal);
                    }
                    else
                    {
                        return BadRequest("El usuario no es el mismo que se logueo");
                    }
                }
                return BadRequest("Error al obtener el usaurio logueado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al cancelar el lote: " + ex.Message);
            }

        }


        [HttpGet]
        public async Task<IActionResult> ObtenerAnalizadores(int idPlanta, int idParametro)
        {
            try
            {
                var analizadores = await _lotificacionService.GetAnalizadoresporParametroAsync(idPlanta, idParametro);
                return Ok(analizadores);
            }
            catch (Exception ex)
            {
                // Manejar errores y registrar
                Console.WriteLine($"Error al obtener analizadores: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductosPorPlanta(int plantaId)
        {
            var productos = await _lotificacionService.GetProductosAsync(plantaId);

            return Json(new { Result = "Ok", Data = productos });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTanquesPorPlantaProducto(int plantaId, int productoId)
        {
            var tanques = await _lotificacionService.GetTanquesPlantaProducto(plantaId, productoId);

            return Json(new { Result = "Ok", Data = tanques });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEspecificacionProducto(int plantaId, int productoId)
        {
            //1 = plantaId - producto
            var lstEspecificProduct = await _lotificacionService.GetEspecificacionProductsAsync(plantaId, productoId, 1);

            return Json(new { Result = "Ok", Data = lstEspecificProduct });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ObtenerAnalisisTanque(ListadoLotificacionData data)
        {
            try
            {
                var datos = await _lotificacionService.ObtenerAnalisisTanque(data);

                if (datos != null && datos.Count() > 0)
                {
                    var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                    if (userInfo != null)
                    {
                        // Se tiene que ir a buscar el permiso para este usuario (api, consulta a base , etc)
                        //Obtener el permiso para este usuario , puede ser 1 = permiso concedido, 0 = No tiene permiso
                        var permisosUsuario = await _lotificacionService.GetPermisosUsuario(userInfo.ExternalId);

                        bool tienePermisoGetUserAdmin = permisosUsuario.Any(p => p.nombrePermiso.Equals("GetUserAdmin") && p.tienePermiso);

                        //bool GetUserAdmin = true;
                        //if (GetUserAdmin)
                        if (tienePermisoGetUserAdmin)
                        {
                            // Actualizar el campo PermisoUserAdmin a true donde GetUserAdmin 
                            datos.ForEach(item =>
                            {
                                //if (item.GetUseralta == userInfo.ExternalId)
                                //{
                                    item.PermisoUserAdmin = true;
                                //}                                   

                            });
                        }

                        // Se tiene que ir a buscar el permiso para este usuario (api, consulta a base , etc)
                        //Obtener el permiso para este usuario , puede ser 1 = permiso concedido, 0 = No tiene permiso
                        bool tienePermisoGetPermisoUser = permisosUsuario.Any(p => p.nombrePermiso.Equals("GetPermisoUser") && p.tienePermiso);

                        //bool GetPermisoUser = false;
                        //if (GetPermisoUser)
                        if (tienePermisoGetPermisoUser)
                        {
                            // Actualizar el campo PermisoUser a true donde GetUseralta coincida con el criterio
                            datos.ForEach(item =>
                            {
                                if (item.GetUseralta == userInfo.ExternalId)
                                {
                                    item.PermisoUser = true;
                                }
                            });
                        }

                        //Se utiliza para ver los permisos de Aceptar o rechazar el lote generado cuando es producto medicinal
                        //Primero validar si el usuario logueado tiene permiso para entrar al modulo de listado de lotes.
                        bool tienePermisoGetListadoLotificacionUser = permisosUsuario.Any(p => p.nombrePermiso.Equals("GetListadoLotificacionUser") && p.tienePermiso);
                        if(tienePermisoGetListadoLotificacionUser)
                        {
                            // Actualizar el campo PermisoRevisionLote a true donde GetListadoLotificacionUser coincida con el criterio
                            datos.ForEach(item =>
                            {
                                if (item.GetUseralta != userInfo.ExternalId)
                                {
                                    item.PermisoRevisionLote = true;
                                }
                            });
                        }

                    }
                }

                return Ok(datos);
            }
            catch (Exception ex)
            {
                // Manejar errores y registrar
                Console.WriteLine($"Error al obtener analizadores: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EditarLoteEstatusRevision(EditarEstatusRevisionData data)
        {
            try
            {                
                var retVal = _lotificacionService.EditarLoteEstatusRevision(data);
                return Ok(retVal);                  
            }
            catch (Exception ex)
            {
                return BadRequest("Error al cancelar el lote: " + ex.Message);
            }

        }
    }
}
