using DevExpress.Web.Internal;
using LiberacionProducto.Entities.CertCatalogos;
using LiberacionProductoWeb.Models.CertCatalogosViewModels;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Mappers
{
    public class CerCatalogosMappers
    {
        public static List<FuenteSuministro> MapFuenteSuministroDataToFuenteSuministro(List<FuenteSuministroData> result)
        {
            List<FuenteSuministro> lstFuenteSuministros = new List<FuenteSuministro>();
            foreach (var item in result)
            {
                lstFuenteSuministros.Add(new FuenteSuministro
                {
                    ID_FUENTE_SUMINISTRO = item.IdFuenteSuministro,
                    descripcion = item.Descripcion,
                    iD_STATUS = item.IdStatus
                });
            }

            return lstFuenteSuministros;
        }

        public static List<PlantaAprobada> MapPlantaAprobadaDataToPlantaAprobada(List<PlantaAprobadaData> result)
        {
            List<PlantaAprobada> lstPlantaAprobada = new List<PlantaAprobada>();
            foreach (var item in result)
            {
                lstPlantaAprobada.Add(new PlantaAprobada
                {
                    ID_PLANTA_APROBADA = item.IdPlantaAprobada,
                    descripcion = item.Descripcion,
                    iD_STATUS = item.IdStatus
                });
            }

            return lstPlantaAprobada;
        }

        public static List<Tanque> MapTanqueDataToTanque(List<TanqueData> result)
        {
            List<Tanque> lstTanque = new List<Tanque>();
            foreach (var item in result)
            {
                lstTanque.Add(new Tanque
                {
                    iD_TANQUE = item.IdTanque,
                    iD_PLANTA = item.IdPlanta,
                    iD_PRODUCTO = item.IdProducto,
                    descripcion = item.Descripcion,
                    grados = item.Grados,
                    iD_STATUS = item.IdStatus,
                    clavE_PALS = item.ClavePals
                });
            }

            return lstTanque;
        }

        public static TanqueData MapDtoTanqueToTanqueData(DtoTanque data)
        {
            return new TanqueData()
            {
                IdProducto = Convert.ToInt32(data.iD_PRODUCTO),
                IdPlanta = Convert.ToInt32(data.iD_PLANTA),
                Descripcion = data.descripcion,
                ClavePals = data.clavE_PALS,
                IdStatus = data.iD_STATUS == "true" ? (short)1 : (short)0
            };
        }

		public static PlantaParametroAnalizadorData MapAnalizadorParametrosToPlantaParametroAnalizadorData(DtoAnalizadorParametros data)
		{
            return new PlantaParametroAnalizadorData()
            {
                IdAnalizador = Convert.ToInt32(data.iD_ANALIZADOR),
                IdPlanta = data.iD_PLANTA == "" ? 0 : Convert.ToInt32(data.iD_PLANTA),
                IdParametro = Convert.ToInt32(data.iD_PARAMETRO),
                IdMetodo = Convert.ToInt32(data.iD_METODO),
                LimiteInferior = Convert.ToDecimal(data.limitE_INFERIOR),
                LeyendaReporte = data.leyendA_REPORTE,
                ClavePals = data.clave_Pals,
				IdStatus = data.iD_STATUS == "true" ? (short)1 : (short)0
			};
		}
	}
}