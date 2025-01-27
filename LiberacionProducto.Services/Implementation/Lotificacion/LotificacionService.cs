

using LiberacionProducto.Bus.Services.Lotificacion;
using LiberacionProducto.Data.Repositories.Lotificacion;
using LiberacionProducto.Entities.Entities.Lotificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LiberacionProducto.Entities.Entities.Lotificacion.ListadoLotificacion;

namespace LiberacionProducto.Services.Implementation.Lotificacion
{
    public class LotificacionService
    {
        private readonly LotificacionRepository _lotificacionRepository;
        private readonly LoteService _loteService;

        public LotificacionService(LotificacionRepository lotificacionRepository,
               LoteService loteService)
        {
            _lotificacionRepository = lotificacionRepository;
            _loteService = loteService;
        }

        public async Task<CAT_TR_PlantaUsuario> GetPlantasUsuario(string idUsuario)
        {
            return await _lotificacionRepository.GetPlantasUsuario(idUsuario);
        }

        public async Task<List<CatPlanta>> GetPlantasAsync(string plantasArray)
        {
            return await _lotificacionRepository.GetPlantasAsync(plantasArray);
        }

        public async Task<List<CatProducto>> GetProductosAsync(int idPlanta)
        {
            return await _lotificacionRepository.GetProductosAsync(idPlanta);
        }

        public async Task<List<CatTanques>> GetTanquesPlantaProducto(int idPlanta, int idProducto)
        {
            return await _lotificacionRepository.GetTanquesPlantaProducto(idPlanta, idProducto);
        }

        public async Task<List<CAB_EspecificacionProducto>> GetEspecificacionProductsAsync(int IdPlanta, int IdProducto, int IdTipoEspecificacion/*int IdEspecificacionProducto*/)
        //public async Task<List<CAB_EspecificacionProducto>> GetEspecificacionProductsAsync(int IdEspecificacionProducto)
        {

            return await _lotificacionRepository.GetEspecificacionProductsAsync(IdPlanta, IdProducto, IdTipoEspecificacion);
        }

        public async Task<List<CAB_TC_Analizadores>> GetAnalizadoresporParametroAsync(int idPlanta, int idParametro)
        {
            return await _lotificacionRepository.GetAnalizadoresporParametroAsync(idPlanta, idParametro);
        }

        public async Task<CatPlanta> GetDescripcionPlanta(int idPlanta)
        {
            return await _lotificacionRepository.GetDescripcionPlanta(idPlanta);
        }
        public async Task<CatProducto> GetDescripcionProducto(int idProducto)
        {
            return await _lotificacionRepository.GetDescripcionProducto(idProducto);
        }

        public string GeneraIdLote(string idProducto, string descProducto, int idPlanta, string descPlanta, string idTanque)
        {
            return _loteService.GeneraNuevoLote(idProducto, descProducto, idPlanta, descPlanta, idTanque);
        }

        public string GuardarDatos(LotificacionData data)
        {
            return _lotificacionRepository.GuardarDatos(data);
        }

        public string EditarDatosLote(LotificacionData data)
        {
            return _lotificacionRepository.EditarDatosLote(data);
        }

        public async Task<List<AnalisisTanque>> ObtenerAnalisisTanque(ListadoLotificacionData dataBusqueda)
        {
            return await _lotificacionRepository.ObtenerAnalisisTanque(dataBusqueda);
        }

        public string CancelarLote(CancelarLoteData data)
        {
            return _lotificacionRepository.CancelarLote(data);
        }
        public async Task<List<PermisosUsuarioData>> GetPermisosUsuario(int idUsuario)
        {
            return await _lotificacionRepository.GetPermisosUsuario(idUsuario);
        }
    }
}
