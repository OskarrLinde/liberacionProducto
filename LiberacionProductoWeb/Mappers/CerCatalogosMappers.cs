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
                    iD_GRADO = item.IdGrado,
                    descripcion = item.Descripcion,
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
                IdGrado = Convert.ToInt32(data.iD_GRADO),
                IdProducto = Convert.ToInt32(data.iD_PRODUCTO),
                IdPlanta = Convert.ToInt32(data.iD_PLANTA),
                Descripcion = data.descripcion,
                ClavePals = data.clavE_PALS,
                IdStatus = data.iD_STATUS == "true" ? (short)1 : (short)0
            };
        }
    }
}