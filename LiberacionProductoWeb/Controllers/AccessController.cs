using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using LiberacionProductoWeb.Models.ConfigViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Localize;
using Microsoft.AspNetCore.Identity;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Helpers;

namespace LiberacionProductoWeb.Controllers
{
    public class AccessController : Controller
    {
        private readonly ILogger<AccessController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        IStringLocalizer<Resource> _resource;
        private readonly AccessOptions _accessOptions;

        public AccessController(ILogger<AccessController> logger, UserManager<ApplicationUser> userManager, IStringLocalizer<Resource> resource, AccessOptions accessOptions)
        {
            _logger = logger;
            _userManager = userManager;
            _resource = resource;
            _accessOptions = accessOptions;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Access()
        {
            ConfiguracionUsuarioVM configuracionUsuario = new ConfiguracionUsuarioVM();

            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                configuracionUsuario.PathECB = _accessOptions.PathECB;
                configuracionUsuario.PathCAB = _accessOptions.PathCAB;
                configuracionUsuario.PathCPM = _accessOptions.PathCPM;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorPrincipal"));
                _logger.LogError(ex, "Error.");
            }

            return View(configuracionUsuario);
        }
    }
}
