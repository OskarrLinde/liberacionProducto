using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.LayoutLeyendsViewModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class LayoutLeyendsController : Controller
    {
        private readonly ILogger<LayoutLeyendsController> _logger;
        private readonly ILayoutLeyendTextService _layoutLeyendTextService;
        private readonly ICheckListService _checkListService;
        public LayoutLeyendsController(ILogger<LayoutLeyendsController> logger, ILayoutLeyendTextService layoutLeyendTextService, ICheckListService checkListService)
        {
            this._logger = logger;
            this._layoutLeyendTextService = layoutLeyendTextService;
            this._checkListService = checkListService;
        }

        [Authorize(SecurityConstants.CONSULTAR_LAYOUT_ORDEN_PRODUCCION_CONDITIONING)]
        [HttpGet]
        public async Task<IActionResult> IndexProductionOrder(string productId = null)
        {
            var model = new LayoutLeyendsVM();
            model.LayoutLeyendsProductionOrders = new List<LayoutLeyendsProductionOrderVM>();
            try
            {
                model = (await _layoutLeyendTextService.ProductionOrderText(productId));
            }
            catch (Exception ex)
            {
                //this._logger.LogError(ex.Message);
                this._logger.LogError(ex, "Error.");
                return Json(new { Result = "Error", Message = "Surgio un error al cargar LayoutLeyendsController IndexProductionOrder." });
            }
            return View(model);
        }
        [Authorize(SecurityConstants.CONSULTAR_LAYOUT_ORDEN_PRODUCCION_CONDITIONING)]
        [HttpGet]
        public async Task<IActionResult> PreviewProductionOrder(string productionId = "OX")
        {
            var model = new ProductionOrderViewModel();
            try
            {
                model.Id = await _layoutLeyendTextService.GetProductionOrderForProduct(productionId);
            }
            catch (Exception ex)
            {
                //this._logger.LogError(ex.Message);
                this._logger.LogError(ex, "Error.");
                var modelError = new ConfiguracionUsuarioVM();
                modelError.MensajeError = "Surgio un error al cargar la vista previa de orden de producción.";
                return RedirectToAction("Errors", "Home", modelError);
            }
            return View(model);
        }
        [Authorize(SecurityConstants.EDITAR_LAYOUT_ORDEN_PRODUCCION_CONDITIONING)]
        [HttpPost]
        public async Task<IActionResult> CrearEditar([FromForm] LayoutLeyendsVM model, int option = 0, bool active = false)
        {
            try
            {
                if (option == 0)
                    await _layoutLeyendTextService.AddLayoutProductionOrder(model, active);
                else
                    await _layoutLeyendTextService.AddLayoutConditioningOrder(model, active);
                return Json(new { Result = "Ok", Message = "Guardado con éxito" });
            }

            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en CrearEditar " + ex);
                _logger.LogError(ex, "Error.");
                //return Json(new { Result = "fail", Message = ex });
                return Json(new { Result = "fail", Message = "Surgio un error." });
            }
        }

        [Authorize(SecurityConstants.EDITAR_LAYOUT_ORDEN_PRODUCCION_CONDITIONING)]
        [HttpPost]
        public async Task<IActionResult> CrearEditarPreview([FromForm] LayoutLeyendsVM model, int option = 0)
        {
            try
            {
                if (option == 0)
                    await _layoutLeyendTextService.AddLayoutPreviewProductionOrder(model);
                else
                    await _layoutLeyendTextService.AddLayoutPreviewConditioningOrder(model);

                return Json(new { Result = "Ok", Message = "Guardado con éxito" });
            }

            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en CrearEditar " + ex);
                _logger.LogError(ex, "Error.");
                //return Json(new { Result = "fail", Message = ex });
                return Json(new { Result = "fail", Message = "Surgio un error." });
            }
        }
        [Authorize(SecurityConstants.CONSULTAR_LAYOUT_ORDEN_PRODUCCION_CONDITIONING)]
        [HttpGet]
        public async Task<IActionResult> IndexConditioningOrder(string productId = null)
        {
            var model = new LayoutLeyendsVM();
            model.LayoutLeyendsConditioningOrders = new List<LayoutLeyendsConditioningOrderVM>();
            try
            {
                model = (await _layoutLeyendTextService.ConditioningOrderText(productId));

            }
            catch (Exception ex)
            {
                //this._logger.LogError(ex.Message);
                this._logger.LogError(ex, "Error.");
                return Json(new { Result = "Error", Message = "Surgio un error al cargar LayoutLeyendsController IndexConditioningOrder." });
            }
            return View(model);
        }

        [Authorize(SecurityConstants.CONSULTAR_LAYOUT_ORDEN_PRODUCCION_CONDITIONING)]
        [HttpGet]
        public async Task<IActionResult> PreviewCondinioningOrder(string productionId = "OX")
        {
            var model = new ProductionOrderViewModel();
            try
            {
                model.Id = await _layoutLeyendTextService.GetConditioningOrderForProduct(productionId);
            }
            catch (Exception ex)
            {
                //this._logger.LogError(ex.Message);
                _logger.LogError(ex, "Error.");
                var modelError = new ConfiguracionUsuarioVM();
                modelError.MensajeError = "Surgio un error al cargar la vista previa de orden de acondicionamiento.";
                return RedirectToAction("Errors", "Home", modelError);
                //return Json(new { Result = "Error", Message = "Surgio un error al cargar la vista previa de orden de acondicionamiento." });
            }
            return View(model);
        }

        [Authorize(SecurityConstants.CONSULTAR_LAYOUT_CHECKLIST)]
        [HttpGet]
        public async Task<IActionResult> IndexChecklist()
        {
            var model = new LayoutLeyendsVM();
            model.LayoutLeyendsProductionOrders = new List<LayoutLeyendsProductionOrderVM>();
            try
            {
                model = (await _layoutLeyendTextService.CheckListText());
            }
            catch (Exception ex)
            {
                //this._logger.LogError(ex.Message);
                this._logger.LogError(ex, "Error.");
                return Json(new { Result = "Error", Message = "Surgio un error al cargar LayoutLeyendsController IndexConditioningOrder." });
            }
            return View(model);
        }

        [Authorize(SecurityConstants.EDITAR_LAYOUT_CHECKLIST)]
        [HttpPost]
        public async Task<IActionResult> CrearEditarChecklist([FromForm] LayoutLeyendsVM model, bool active = false)
        {
            try
            {
                await _layoutLeyendTextService.AddLayoutCheckList(model, active);
                return Json(new { Result = "Ok", Message = "Guardado con éxito" });
            }

            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en CrearEditar " + ex);
                _logger.LogError(ex, "Error.");
                //return Json(new { Result = "fail", Message = ex });
                return Json(new { Result = "fail", Message = "Surgio un error." });
            }
        }

        [Authorize(SecurityConstants.EDITAR_LAYOUT_CHECKLIST)]
        [HttpPost]
        public async Task<IActionResult> CrearEditarPreviewCheckList([FromForm] LayoutLeyendsVM model)
        {
            try
            {
                await _layoutLeyendTextService.AddLayoutPreviewCheckList(model);
                return Json(new { Result = "Ok", Message = "Guardado con éxito" });
            }

            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en CrearEditar " + ex);
                _logger.LogError(ex, "Surgio un error.");
                //return Json(new { Result = "fail", Message = ex });
                return Json(new { Result = "fail", Message = "Surgio un error." });
            }
        }

        [Authorize(SecurityConstants.CONSULTAR_LAYOUT_CHECKLIST)]
        [HttpGet]
        public async Task<IActionResult> PreviewCheckList()
        {
            var model = new CheckListVM();
            try
            {
                var data = await _checkListService.GetCheckListCloseOk();
                model.NumberOrder = data.NumOA;
                model.TourNumber = data.TourNumber;
                model.DistributionBatch = data.DistributionBatch;
                model.checkListId = data.Id;
                ///model.Id = await _layoutLeyendTextService.GetProductionOrderForProduct(productionId);
            }
            catch (Exception ex)
            {
                //this._logger.LogError(ex.Message);
                _logger.LogError(ex, "Error.");
                return Json(new { Result = "Error", Message = "Surgio un error al cargar LayoutLeyendsController PreviewProductionOrder." });
            }
            return View(model);
        }
        public async Task<string> GetUserId(List<System.Security.Claims.Claim> claims)
        {
            string Id = string.Empty;
            foreach (var item in claims)
            {
                if (item.Type == "Id")
                {
                    Id = item.Value;
                }
            }
            return Id;
        }
    }
}
