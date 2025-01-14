using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Data.Repository.Base.External;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.LayoutLeyendsViewModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public class ProductionOrderService : IProductionOrderService
    {
        private readonly ILogger<ProductionOrderService> _logger;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly ILotesProduccionDetalleRepository _lotesProduccionDetalle;
        private readonly IProductionEquipmentRepository _productionEquipmentRepository;
        private readonly IMonitoringEquipmentRepository _monitoringEquipmentRepository;
        private readonly IPipelineClearanceRepository _pipelineClearanceRepository;
        private readonly IProductionOrderAttributeRepository _productionOrderAttributeRepository;
        private readonly IBatchDetailsRepository _batchDetailsRepository;
        private readonly IGeneralCatalogRepository _generalCatalogRepository;
        private readonly IBatchAnalysisRepository _batchAnalysisRepository;
        public bool WSMexeFuncionalidad;
        private readonly IConfiguration _config;
        private readonly IVariablesFileReaderService _variablesFileReaderService;
        private readonly IHistoryStatesRepository _historyStatesRepository;
        private readonly ILeyendsPreviewTextRepository _leyendsPreviewTextRepository;
        private readonly ILeyendsTextHistoryGroupRepository _leyendsTextHistoryGroupRepository;
        private readonly ILeyendsTextHistoryRepository _leyendsTextHistoryRepository;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly IProductionOrderCustomersFilesRepository _productionOrderCustomersFilesRepository;
        private readonly IConditioningOrderRepository _conditioningOrderRepository;
        public ProductionOrderService(ILogger<ProductionOrderService> logger,
            IProductionOrderRepository productionOrderRepository,
            ILotesProduccionDetalleRepository lotesProduccionDetalle,
            IProductionEquipmentRepository productionEquipmentRepository,
            IMonitoringEquipmentRepository monitoringEquipmentRepository,
            IPipelineClearanceRepository pipelineClearanceRepository,
            IProductionOrderAttributeRepository productionOrderAttributeRepository,
            IBatchDetailsRepository batchDetailsRepository,
            IGeneralCatalogRepository generalCatalogRepository,
            IBatchAnalysisRepository batchAnalysisRepository,
            IConfiguration config,
            IVariablesFileReaderService variablesFileReaderService,
            IHistoryStatesRepository historyStatesRepository,
            ILeyendsPreviewTextRepository leyendsPreviewTextRepository,
            ILeyendsTextHistoryGroupRepository leyendsTextHistoryGroupRepository,
            ILeyendsTextHistoryRepository leyendsTextHistoryRepository,
            IStringLocalizer<Resource> resource,
            IProductionOrderCustomersFilesRepository productionOrderCustomersFilesRepository,
            IConditioningOrderRepository conditioningOrderRepository)
        {
            _logger = logger;
            _productionOrderRepository = productionOrderRepository;
            _lotesProduccionDetalle = lotesProduccionDetalle;
            _productionEquipmentRepository = productionEquipmentRepository;
            _monitoringEquipmentRepository = monitoringEquipmentRepository;
            _pipelineClearanceRepository = pipelineClearanceRepository;
            _productionOrderAttributeRepository = productionOrderAttributeRepository;
            _batchDetailsRepository = batchDetailsRepository;
            _generalCatalogRepository = generalCatalogRepository;
            _batchAnalysisRepository = batchAnalysisRepository;
            _config = config;
            bool.TryParse(_config["FlagWSMexe:ServiceApiKey"], out WSMexeFuncionalidad);
            _variablesFileReaderService = variablesFileReaderService;
            _historyStatesRepository = historyStatesRepository;
            _leyendsPreviewTextRepository = leyendsPreviewTextRepository;
            _leyendsTextHistoryGroupRepository = leyendsTextHistoryGroupRepository;
            _leyendsTextHistoryRepository = leyendsTextHistoryRepository;
            _resource = resource;
            _productionOrderCustomersFilesRepository = productionOrderCustomersFilesRepository;
            _conditioningOrderRepository = conditioningOrderRepository;
        }

        public async Task<IList<ProductionOrder>> GetAllAsync()
        {
            var productionOrders = await _productionOrderRepository.GetAllAsync();

            return new List<ProductionOrder>(productionOrders);
        }

        public async Task<IList<VwLotesProduccionDetalle>> GetLot(string plant, string product, string tank, string productoNombre)
        {
            List<VwLotesProduccionDetalle> vwLotes = new List<VwLotesProduccionDetalle>();
            if (WSMexeFuncionalidad)
            {
                var Result = _lotesProduccionDetalle.GetAll(x => x.ID_PLANTA == int.Parse(plant) && x.PRODUCT_ID == product && x.TANQUE == tank);
                var ultimaFecha = Result.Max(x => x.FECHA_ALTA);
                var LotesProduccionDetalle = Result.Where(x => x.ID_PLANTA == int.Parse(plant) && x.PRODUCT_ID == product && x.TANQUE == tank && x.FECHA_ALTA == ultimaFecha);
                LotesProduccionDetalle = LotesProduccionDetalle.Where(x => x.PARAMETRO != null);
                ///parameters
                var filterVariables = await this._variablesFileReaderService.GetFilterParameterByProductKeyAsync(productoNombre);
                foreach (var item in LotesProduccionDetalle)
                {
                    if (filterVariables == null || (filterVariables != null && !filterVariables.Variables.Contains(item.PARAMETRO.Trim())))
                    {
                        vwLotes.Add(new VwLotesProduccionDetalle
                        {
                            ID_LOTE = item.ID_LOTE,
                            TANQUE = item.TANQUE,
                            NIVEL_FINAL = item.NIVEL_FINAL,
                            PARAMETRO = item.PARAMETRO,
                            VALOR_ANALISIS = item.VALOR_ANALISIS,
                            FECHA_ALTA = item.FECHA_ALTA,
                            CREADO_POR = item.CREADO_POR,
                            STATUS = item.STATUS,
                            UNIDAD_DE_MEDIDA = item.UNIDAD_DE_MEDIDA,
                            LIMITE_SUPERIOR = item.LIMITE_SUPERIOR,
                            LIMITE_INFERIOR = item.LIMITE_INFERIOR
                        });
                    }
                }

            }
            else
            {
                return new List<VwLotesProduccionDetalle>
                 {
                     new VwLotesProduccionDetalle
                     {
                         ID_LOTE = "OX - M31 -040820 - M31 - LOX - 01 - 0951",
                         TANQUE = "Almacenamiento TC - 3620 (M31-LOX-01)",
                         NIVEL_FINAL = "85",
                         PARAMETRO = "Pureza",
                         VALOR_ANALISIS = 0.6500M,
                         FECHA_ALTA = DateTime.Now,
                         CREADO_POR = "Martha Elene Herrera",
                         STATUS = "PRODUCTO CONFORME"
                     },
                     new VwLotesProduccionDetalle
                     {
                         ID_LOTE = "OX - M31 -040820 - M31 - LOX - 01 - 0951",
                         TANQUE = "Almacenamiento TC - 3620 (M31-LOX-01)",
                         NIVEL_FINAL = "85",
                         PARAMETRO = "Identidad",
                         VALOR_ANALISIS = 0.6300M,
                         FECHA_ALTA = DateTime.Now,
                         CREADO_POR = "Martha Elene Herrera",
                         STATUS = "PRODUCTO CONFORME"
                     },
                     new VwLotesProduccionDetalle
                     {
                         ID_LOTE = "OX - M31 -040820 - M31 - LOX - 01 - 0951",
                         TANQUE = "Almacenamiento TC - 3620 (M31-LOX-01)",
                         NIVEL_FINAL = "85",
                         PARAMETRO = "Humedad",
                         VALOR_ANALISIS = 0.2200M,
                         FECHA_ALTA = DateTime.Now,
                         CREADO_POR = "Martha Elene Herrera",
                         STATUS = "PRODUCTO CONFORME"
                     },
                     new VwLotesProduccionDetalle
                     {
                         ID_LOTE = "OX - M31 -040820 - M31 - LOX - 01 - 0951",
                         TANQUE = "Almacenamiento TC - 3620 (M31-LOX-01)",
                         NIVEL_FINAL = "85",
                         PARAMETRO = "CO",
                         VALOR_ANALISIS = 0.2500M,
                         FECHA_ALTA = DateTime.Now,
                         CREADO_POR = "Martha Elene Herrera",
                         STATUS = "PRODUCTO CONFORME"
                     },
                     new VwLotesProduccionDetalle
                     {
                         ID_LOTE = "OX - M31 -040820 - M31 - LOX - 01 - 0951",
                         TANQUE = "Almacenamiento TC - 3620 (M31-LOX-01)",
                         NIVEL_FINAL = "85",
                         PARAMETRO = "CO2",
                         VALOR_ANALISIS = 0.1000M,
                         FECHA_ALTA = DateTime.Now,
                         CREADO_POR = "Martha Elene Herrera",
                         STATUS = "PRODUCTO CONFORME"
                     },
                 };
            }
            return new List<VwLotesProduccionDetalle>(vwLotes);
        }

        public Task<IList<VwLotesProduccionDetalle>> GetLot()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductionOrderViewModel> GetByIdAsync(int Id, int preview = 0)
        {
            if (Id <= 0)
            {
                return null;
            }

            var entity = await _productionOrderRepository.GetByIdAsync(Id);

            if (entity == null)
            {
                return null;
            }

            var generalCatalogFilter = await _generalCatalogRepository.GetAsync(x => x.PlantId == entity.PlantId && x.ProductId == entity.ProductId && x.TankId == entity.TankId);

            var model = new ProductionOrderViewModel
            {
                Id = entity.Id,
                StepSaved = entity.StepSaved,
                IsReleased = entity.IsReleased,
                ReleasedBy = entity.ReleasedBy,
                ReleasedDate = entity.ReleasedDate,
                ReleasedNotes = entity.ReleasedNotes,
                CreatedDate = entity.CreatedDate,
                Status = entity.State,
                SelectedPlantFilter = entity.PlantId,
                SelectedProductFilter = entity.ProductId,
                SelectedTankFilter = entity.TankId,
                SelectedPurityFilter = entity.Purity?.ToString(),
                InCompliance = entity.InCompliance,
            };

            var productionEquipment = (await _productionEquipmentRepository.GetAsync(x => x.ProductionOrderId == entity.Id)).FirstOrDefault();
            if (productionEquipment != null)
            {
                model.ProductionEquipment = new ProductionEquipmentViewModel
                {
                    IsAvailable = productionEquipment.IsAvailable,
                    ReviewedBy = productionEquipment.ReviewedBy,
                    ReviewedDate = productionEquipment.ReviewedDate,
                };
            }

            var monitoringEquipmentList = await _monitoringEquipmentRepository.GetAsync(x => x.ProductionOrderId == entity.Id);
            if (monitoringEquipmentList != null)
            {
                model.MonitoringEquipmentList = monitoringEquipmentList.Select(x =>
                    new MonitoringEquipmentViewModel
                    {
                        Code = x.Code,
                        Description = x.Description,
                        IsCalibrated = x.IsCalibrated,
                        Notes = x.Notes,
                        ReviewedBy = x.ReviewedBy,
                        ReviewedDate = x.ReviewedDate,
                    }
                ).ToList();
            }

            var pipelineClearance = (await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId == entity.Id && x.InCompliance.HasValue && x.InCompliance.Value)).FirstOrDefault();
            if (pipelineClearance != null)
            {
                model.PipelineClearance = new PipelineClearanceViewModel
                {
                    InCompliance = pipelineClearance.InCompliance,
                    ReviewedBy = pipelineClearance.ReviewedBy,
                    ReviewedDate = pipelineClearance.ReviewedDate,
                    ProductionStartedDate = pipelineClearance.ProductionStartedDate,
                    ProductionEndDate = pipelineClearance.ProductionEndDate
                };
            }

            var controlVariablesList = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.ControlVariable);
            if (controlVariablesList != null)
            {
                model.ControlVariables = controlVariablesList.Select(x =>
                    new ControlVariableViewModel
                    {
                        Id = x.Id,
                        Area = x.Area,
                        Description = x.Description,
                        Variable = x.Variable + " / " + x.Specification,
                        Specification = x.Specification,
                        ChartPath = x.ChartPath,
                        MaxValue = x.MaxValue,
                        MinValue = x.MinValue,
                        AvgValue = x.AvgValue,
                        InCompliance = x.InCompliance,
                        DeviationReport = new DeviationReportViewModel
                        {
                            Folio = x.DeviationReportFolio,
                            Notes = x.DeviationReportNotes
                        },
                        ReviewedBy = x.ReviewedBy,
                        ReviewedDate = x.ReviewedDate,
                        Notes = x.Notes,
                        Type = ProductionOrderAttributeType.ControlVariable,
                        //MEG find this variable on General Catalog Table TODO - Optimization



                        VariableCode = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.CodeTool, //charpath used on substitution of var code
                        LowLimit = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.LowerLimit,
                        TopLimit = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.UpperLimit
                    }
                ).ToList();
            }

            var criticalParametersList = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.CriticalParameter);
            if (criticalParametersList != null)
            {
                model.CriticalParameters = criticalParametersList.Select(x =>
                    new CriticalParameterViewModel
                    {
                        Id = x.Id,
                        Area = x.Area,
                        Description = x.Description,
                        Parameter = x.Variable + " / " + x.Specification,
                        Specification = x.Specification,
                        ChartPath = x.ChartPath,
                        MaxValue = x.MaxValue,
                        MinValue = x.MinValue,
                        AvgValue = x.AvgValue,
                        InCompliance = x.InCompliance,
                        DeviationReport = new DeviationReportViewModel
                        {
                            Folio = x.DeviationReportFolio,
                            Notes = x.DeviationReportNotes
                        },
                        ReviewedBy = x.ReviewedBy,
                        ReviewedDate = x.ReviewedDate,
                        Notes = x.Notes,
                        Type = ProductionOrderAttributeType.CriticalParameter,
                        //MEG find this variable on General Catalog Table TODO - Optimization
                        VariableCode = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.CodeTool,
                        LowLimit = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.LowerLimit,
                        TopLimit = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.UpperLimit
                    }
                ).ToList();
            }

            var criticalQualityAttributesList = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.CriticalQualityAttribute);
            if (criticalQualityAttributesList != null)
            {
                model.CriticalQualityAttributes = criticalQualityAttributesList.Select(x =>
                    new CriticalQualityAttributeViewModel
                    {
                        Id = x.Id,
                        Area = x.Area,
                        Description = x.Description,
                        Attribute = x.Variable + " / " + x.Specification,
                        Specification = x.Specification,
                        ChartPath = x.ChartPath,
                        MaxValue = x.MaxValue,
                        MinValue = x.MinValue,
                        AvgValue = x.AvgValue,
                        InCompliance = x.InCompliance,
                        DeviationReport = new DeviationReportViewModel
                        {
                            Folio = x.DeviationReportFolio,
                            Notes = x.DeviationReportNotes
                        },
                        ReviewedBy = x.ReviewedBy,
                        ReviewedDate = x.ReviewedDate,
                        Notes = x.Notes,
                        Type = ProductionOrderAttributeType.CriticalQualityAttribute,
                        //MEG find this variable on General Catalog Table TODO - Optimization
                        VariableCode = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.CodeTool,
                        LowLimit = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.LowerLimit,
                        TopLimit = generalCatalogFilter
                                        .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.UpperLimit
                    }
                ).ToList();
            }

            var batchDetails = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == entity.Id)).FirstOrDefault();
            if (batchDetails != null)
            {
                var Analysis = await _batchAnalysisRepository.GetAsync(x => x.BatchDetailsId == batchDetails.Id);
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<BatchAnalysisViewModel>>(Analysis);

                model.BatchDetails = new BatchDetailsViewModel
                {
                    Number = batchDetails.Number,
                    Tank = batchDetails.Tank,
                    Level = batchDetails.Level,
                    Size = batchDetails.Size,
                    AnalyzedBy = batchDetails.AnalyzedBy,
                    AnalyzedDate = batchDetails.AnalyzedDate,
                    InCompliance = batchDetails.InCompliance,
                    NotInComplianceFolio = batchDetails.NotInComplianceFolio,
                    NotInComplianceNotes = batchDetails.NotInComplianceNotes,
                    IsReleased = batchDetails.IsReleased,
                    ReleasedBy = batchDetails.ReleasedBy,
                    ReleasedDate = batchDetails.ReleasedDate,
                    ReleasedNotes = batchDetails.ReleasedNotes,
                    Analysis = mapped.ToList()
                };
            }

            var leyends = new List<LayoutLeyendsProductionOrderVM>();
            if (preview != 0)
            {
                var data = from tbl in await _leyendsPreviewTextRepository.
                                  GetAsync(x => x.Type.Equals(LayoutLeyendsType.ProductionOrder.Value
                                  ) && x.ProductId.Equals(model.SelectedProductFilter))
                           select new LeyendsPreviewText
                           {
                               Type = tbl.Type,
                               Step = tbl.Step,
                               Title = tbl.Title,
                               Text = tbl.Text
                               .Replace("style=\"font-family: 'Arial'; font-size: 9pt;\"", "")
                           };
                leyends = ObjectMapper.Mapper.Map<List<LayoutLeyendsProductionOrderVM>>(data);
            }
            else
            {
                var data = await leyendsHistoryTexts(entity.GroupId);
                leyends = ObjectMapper.Mapper.Map<List<LayoutLeyendsProductionOrderVM>>(data);
            }
            model.LayoutLeyends = leyends;
            var customerFiles = await this.GetCustomersFiles(model.Id);
            model.ProductionOrderCustomersFilesViewModel = customerFiles;
            return model;
        }

        public async Task<Boolean> ForReleasedProductionOrder(int ProductionOrderId)
        {
            var ForReleased = new Boolean();
            ForReleased = (await _historyStatesRepository.
                            GetAsync(x => x.ProductionOrderId == ProductionOrderId)).Where(x => x.State == ProductionOrderStatus.ToBeReleased.Value).Any() ? true : false;
            return ForReleased;
        }

        public async Task<int> GetProductionOrderForProduct(string productId)
        {
            return (await _productionOrderRepository
                         .GetAsync(x => x.State.Equals(ProductionOrderStatus.Released.Value)
                         && x.ProductId.Equals(productId))).LastOrDefault().Id;
        }

        public async Task<List<LeyendsPreviewText>> leyendsHistoryTexts(Guid guid, bool style = false)
        {
            var historyGroup = (await _leyendsTextHistoryGroupRepository.
            GetAsync(x => x.GroupId.Equals(guid)));

            var history = await _leyendsTextHistoryRepository.GetAsync(x =>
                       historyGroup.Select(x => x.Id).Contains(x.LeyendsTextHistoryGroupId));

            var data = from tbl1 in historyGroup
                       join tbl2 in history
                      on tbl1.Id equals tbl2.LeyendsTextHistoryGroupId
                       select new LeyendsPreviewText
                       {
                           Type = tbl1.Type,
                           Step = tbl1.Step,
                           Title = tbl2.Title,
                           Text = style ? tbl2.Text : tbl2.Text.Replace("style=\"font-family: 'Arial'; font-size: 9pt;\"", ""),
                       };
            return data.ToList();
        }

        public async Task<List<LayoutLeyendsProductionOrderVM>> GenerateLeyends(ProductionOrderViewModel model, int preview = 0)
        {
            var leyends = new List<LayoutLeyendsProductionOrderVM>();
            if (preview != 0)
            {
                var data = from tbl in await _leyendsPreviewTextRepository.
                                  GetAsync(x => x.Type.Equals(LayoutLeyendsType.ProductionOrder.Value
                                  ) && x.ProductId.Equals(model.SelectedProductFilter))
                           select new LeyendsPreviewText
                           {
                               Type = tbl.Type,
                               Step = tbl.Step,
                               Title = tbl.Title,
                               Text = tbl.Text
                           };
                leyends = ObjectMapper.Mapper.Map<List<LayoutLeyendsProductionOrderVM>>(data);
            }
            else
            {
                var historyGroup = (await _leyendsTextHistoryGroupRepository.
                    GetAsync(x => x.GroupId.Equals(model.GroupId)));

                var history = await _leyendsTextHistoryRepository.GetAsync(x =>
                           historyGroup.Select(x => x.Id).Contains(x.LeyendsTextHistoryGroupId));

                var data = from tbl1 in historyGroup
                           join tbl2 in history
                          on tbl1.Id equals tbl2.LeyendsTextHistoryGroupId
                           select new LeyendsPreviewText
                           {
                               Type = tbl1.Type,
                               Step = tbl1.Step,
                               Title = tbl2.Title,
                               Text = tbl2.Text
                           };
                leyends = ObjectMapper.Mapper.Map<List<LayoutLeyendsProductionOrderVM>>(data);
            }
            return leyends;
        }

        public async Task<ProductionOrderCustomersFiles> SaveCustomersFiles(ProductionOrderViewModel model, string TableName, IFormFile files)
        {
            var entity = new ProductionOrderCustomersFiles();
            string FileName = Guid.NewGuid().ToString();
            var upload = Path.Combine(_resource.GetString("PathProductionOrderCustomersFiles").Value);
            if (!Directory.Exists(upload))
                Directory.CreateDirectory(upload);
            var ext = Path.GetExtension(files.FileName);
            using (var fileStreams = new FileStream(Path.Combine(upload, FileName + ext), FileMode.Create))
            {
                files.CopyTo(fileStreams);
            }
            entity = ProductionOrderCustomersFiles.Create(
                0,
                model.DelegateUser,
                DateTime.Now,
                model.Id,
                true,
                FileName,
                files.FileName,
                ext,
                TableName);
            System.Threading.Thread.Sleep(1000);
            var counter = (await _productionOrderCustomersFilesRepository.
                           GetAsync(x => x.ProductionOrderId.Equals(model.Id) && x.Type.Equals(TableName))).Count;
            if (counter >= 10)
                return null;

            var insert = await _productionOrderCustomersFilesRepository.AddAsync(entity);
            insert.IsTransient();
            return entity;
        }

        public async Task<bool> DeleteCustomerFile(ProductionOrderViewModel model)
        {
            var resultado = (await _productionOrderCustomersFilesRepository.GetAsync(x => x.FileName == model.FileName)).FirstOrDefault();
            if (resultado != null)
            {
                await _productionOrderCustomersFilesRepository.DeleteAsync(resultado);
                var upload = Path.Combine(_resource.GetString("PathProductionOrderCustomersFiles").Value);
                //delete file
                if ((System.IO.File.Exists(upload + @"\" + resultado.FileName)))
                {
                    System.IO.File.Delete(upload + @"\" + resultado.FileName);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ProductionOrderCustomersFilesViewModel>> GetCustomersFiles(int ProductionOrderId)
        {
            var files = new List<ProductionOrderCustomersFilesViewModel>();
            var data = await _productionOrderCustomersFilesRepository.GetAsync(x => x.ProductionOrderId.Equals(ProductionOrderId));
            if (data != null)
                files = ObjectMapper.Mapper.Map<IEnumerable<ProductionOrderCustomersFilesViewModel>>(data).ToList();
            return files;
        }

        public async Task<int> GetConditioningOrderForProduct(string productId)
        {
            var ListStates = new List<string>();
            ListStates.Add(ConditioningOrderStatus.ReleasedOld.Value);
            ListStates.Add(ConditioningOrderStatus.Released.Value);
            return (await _conditioningOrderRepository
                         .GetAsync(x => ListStates.Contains(x.State)
                         && x.ProductId.Equals(productId))).LastOrDefault().Id;
        }

        public async Task<bool> ValidateExistFile(ProductionOrderViewModel model, string TableName, IFormFileCollection files)
        {
            var exist = new bool();
            var existFile = (await _productionOrderCustomersFilesRepository.
                        GetAsync(x => x.ProductionOrderId.Equals(model.Id)
                        && x.Type.Equals(TableName)
                        && files.Select(x => x.FileName).Contains(x.FileNameOrigin)));
            if (existFile.Any())
            {
                exist = true;
            }
            return exist;
        }
    }
}