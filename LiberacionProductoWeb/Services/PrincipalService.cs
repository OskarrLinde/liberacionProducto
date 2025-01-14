using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Data.Repository.Base.External;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.LayoutLeyendsViewModels;
using LiberacionProductoWeb.Models.Principal;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public class PrincipalService : IPrincipalService
    {
        private readonly IPlantasRepository _plantasRepository;
        private readonly ITanquesRepository _tanquesRepository;
        private readonly IConfiguration _config;
        private readonly IProductoRepository _productoRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IActivitieRepository _activitieRepository;
        private readonly IAnalyticsCertsService _analyticsCertsService;
        private readonly IGeneralCatalogRepository _generalCatalogRepository;
        private readonly ICheckListPipeRecordAnswerRepository _checkListPipeRecordAnswerRepository;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPipelineClearanceRepository _pipelineClearanceRepository;
        private readonly IBatchDetailsRepository _batchDetailsRepository;
        private readonly IConditioningOrderRepository _conditioningOrderRepository;
        private readonly IPipeFillingControlRepository _pipeFillingControlRepository;
        private readonly IPipeFillingRepository _pipeFillingRepository;
        private readonly IEquipmentProcessConditioningRepository _equipmentProcessConditioningRepository;
        private readonly IPipeFillingCustomerRepository _pipeFillingCustomerRepository;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly ICheckListPipeDictiumAnswerRepository _checkListPipeDictiumAnswerRepository;
        private readonly IProductCatalogRepository _productCatalogRepository;
        private readonly IHistoryNotesRepository _historyNotesRepository;
        private readonly IActivitiesReportAuditRepository _activitiesReportAuditRepository;
        private readonly IPerformanceProcessConditioningRepository _performanceProcessConditioningRepository;
        private readonly IConditioningOrderService _conditioningOrderService;
        private readonly IPipeFillingCustomersFilesRepository _pipeFillingCustomersFilesRepository;
        private readonly ILeyendsPreviewTextRepository _leyendsPreviewTextRepository;
        private readonly ILeyendsTextHistoryGroupRepository _leyendsTextHistoryGroupRepository;
        private readonly ILeyendsTextHistoryRepository _leyendsTextHistoryRepository;
        private readonly ILogger<PrincipalService> _logger;
        public bool WSMexeFuncionalidad;

        public PrincipalService(IPlantasRepository plantasRepository,
            ITanquesRepository tanquesRepository,
            IProductoRepository productoRepository,
            IConfiguration config,
            IStateRepository stateRepository,
            IActivitieRepository activitieRepository,
            IAnalyticsCertsService analyticsCertsService,
            IGeneralCatalogRepository generalCatalogRepository,
            ICheckListPipeRecordAnswerRepository checkListPipeRecordAnswerRepository,
            IProductionOrderRepository productionOrderRepository,
            UserManager<ApplicationUser> userManager,
            IPipelineClearanceRepository pipelineClearanceRepository,
            IBatchDetailsRepository batchDetailsRepository,
            IConditioningOrderRepository conditioningOrderRepository,
            IPipeFillingControlRepository pipeFillingControlRepository,
            IPipeFillingRepository pipeFillingRepository,
            IEquipmentProcessConditioningRepository equipmentProcessConditioningRepository,
            IStringLocalizer<Resource> resource,
            IPipeFillingCustomerRepository pipeFillingCustomerRepository,
            ICheckListPipeDictiumAnswerRepository checkListPipeDictiumAnswerRepository,
            IProductCatalogRepository productCatalogRepository,
            IHistoryNotesRepository historyNotesRepository,
            IActivitiesReportAuditRepository activitiesReportAuditRepository,
            IPerformanceProcessConditioningRepository performanceProcessConditioningRepository,
            IConditioningOrderService conditioningOrderService,
            IPipeFillingCustomersFilesRepository pipeFillingCustomersFilesRepository,
            ILeyendsPreviewTextRepository leyendsPreviewTextRepository,
            ILeyendsTextHistoryGroupRepository leyendsTextHistoryGroupRepository,
            ILeyendsTextHistoryRepository leyendsTextHistoryRepository,
            ILogger<PrincipalService> logger)
        {
            _plantasRepository = plantasRepository;
            _tanquesRepository = tanquesRepository;
            _productoRepository = productoRepository;
            _config = config;
            _stateRepository = stateRepository;
            _activitieRepository = activitieRepository;
            bool.TryParse(_config["FlagWSMexe:ServiceApiKey"], out WSMexeFuncionalidad);
            _analyticsCertsService = analyticsCertsService;
            _generalCatalogRepository = generalCatalogRepository;
            _checkListPipeRecordAnswerRepository = checkListPipeRecordAnswerRepository;
            _productionOrderRepository = productionOrderRepository;
            _userManager = userManager;
            _pipelineClearanceRepository = pipelineClearanceRepository;
            _batchDetailsRepository = batchDetailsRepository;
            _conditioningOrderRepository = conditioningOrderRepository;
            _pipeFillingControlRepository = pipeFillingControlRepository;
            _pipeFillingRepository = pipeFillingRepository;
            _equipmentProcessConditioningRepository = equipmentProcessConditioningRepository;
            _resource = resource;
            _pipeFillingCustomerRepository = pipeFillingCustomerRepository;
            _checkListPipeDictiumAnswerRepository = checkListPipeDictiumAnswerRepository;
            _productCatalogRepository = productCatalogRepository;
            _historyNotesRepository = historyNotesRepository;
            _activitiesReportAuditRepository = activitiesReportAuditRepository;
            _performanceProcessConditioningRepository = performanceProcessConditioningRepository;
            _conditioningOrderService = conditioningOrderService;
            _pipeFillingCustomersFilesRepository = pipeFillingCustomersFilesRepository;
            _leyendsPreviewTextRepository = leyendsPreviewTextRepository;
            _leyendsTextHistoryGroupRepository = leyendsTextHistoryGroupRepository;
            _leyendsTextHistoryRepository = leyendsTextHistoryRepository;
            _logger = logger;
        }
        public Task<List<SelectListItem>> GetPlants()
        {
            List<SelectListItem> plants = new List<SelectListItem>();
            if (WSMexeFuncionalidad)
            {
                var plantas = _plantasRepository.GetAll();
                foreach (var item in plantas)
                {
                    plants.Add(new SelectListItem { Value = item.Id_Planta.ToString(), Text = item.Descripcion });
                }
            }
            else
            {

                plants.Add(new SelectListItem { Value = "1", Text = "Planta 1" });
                plants.Add(new SelectListItem { Value = "2", Text = "Planta 2" });
                plants.Add(new SelectListItem { Value = "3", Text = "Planta 3" });
                plants.Add(new SelectListItem { Value = "4", Text = "Planta 4" });
                plants.Add(new SelectListItem { Value = "5", Text = "Planta 5" });

            }
            return Task.FromResult(plants);
        }

        public Task<List<SelectListItem>> GetProducts()
        {
            List<SelectListItem> products = new List<SelectListItem>();
            if (WSMexeFuncionalidad)
            {
                var productos = _productoRepository.GetAll();
                foreach (var item in productos)
                {
                    products.Add(new SelectListItem { Value = item.Product_Id, Text = item.Product_Name });
                }
            }
            else
            {
                products.Add(new SelectListItem { Value = "OX", Text = _resource.GetString("Oxygen") });
                products.Add(new SelectListItem { Value = "NI", Text = _resource.GetString("Nitrogen") });

            }
            return Task.FromResult(products);
        }

        public Task<List<SelectListItem>> GetTanks(string product, string[] plantsByUser = null, int function = 0)
        {
            List<SelectListItem> tanks = new List<SelectListItem>();
            if (WSMexeFuncionalidad)
            {
                var productos = from tanq in _tanquesRepository.GetAll().Where(x => x.Product_Id == product)
                                group tanq.Descripcion
                                by tanq.Descripcion
                                   into tbl
                                select new VwTanques
                                {
                                    Descripcion = tbl.Key

                                };
                foreach (var item in productos)
                {
                    tanks.Add(new SelectListItem { Value = item.Descripcion, Text = item.Descripcion });
                }
            }
            else
            {
                List<VwTanques> ListTanks = new List<VwTanques>();
                ListTanks.Add(new VwTanques { Product_Id = "OX", Id_Tanque = 1, Id_Planta = 1, Descripcion = "LOX-01" });
                ListTanks.Add(new VwTanques { Product_Id = "OX", Id_Tanque = 1, Id_Planta = 32, Descripcion = "LOX-02" });
                ListTanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 2, Id_Planta = 1, Descripcion = "LNI-01" });
                ListTanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 3, Id_Planta = 32, Descripcion = "LNI-04" });
                ListTanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 3, Id_Planta = 2, Descripcion = "LNI-02" });
                ListTanks.Add(new VwTanques { Product_Id = "OX", Id_Tanque = 1, Id_Planta = 5, Descripcion = "LOX-01" });
                ListTanks.Add(new VwTanques { Product_Id = "CD", Id_Tanque = 1, Id_Planta = 13, Descripcion = "LCD-03" });
                foreach (var item in ListTanks.Where(x => x.Product_Id == product))
                {
                    if (plantsByUser == null)
                    {
                        tanks.Add(new SelectListItem { Value = item.Descripcion, Text = item.Descripcion });
                    }
                    else
                    {
                        if (plantsByUser != null)
                        {
                            foreach (var itemx in plantsByUser.ToList().Where(x => x.ToString() != ""))
                            {
                                if (itemx.ToString() == item.Id_Planta.ToString())
                                {
                                    tanks.Add(new SelectListItem { Value = item.Id_Tanque.ToString(), Text = item.Descripcion });
                                }
                            }
                        }

                    }
                }
            }
            return Task.FromResult(tanks);
        }

        public Task<List<TankViewModel>> GetTanksViewModel(string product, string[] plantsByUser = null, int function = 0)
        {
            List<TankViewModel> tanks = new List<TankViewModel>();
            if (WSMexeFuncionalidad)
            {
                var productos = _tanquesRepository.GetAll();
                foreach (var item in productos)
                {
                    tanks.Add(new TankViewModel
                    {
                        PlantId = item.Id_Planta,
                        ProductId = item.Product_Id,
                        TankId = item.Id_Tanque.ToString(),
                        TankGeneratedId = item.Id_Tanque + item.Descripcion + item.Id_Planta + item.Product_Id,
                        Descripcion = item.Descripcion
                    });
                }
            }
            else
            {
                List<VwTanques> ListTanks = new List<VwTanques>();
                ListTanks.Add(new VwTanques { Product_Id = "OX", Id_Tanque = 1, Id_Planta = 1, Descripcion = "LOX-01" });
                ListTanks.Add(new VwTanques { Product_Id = "OX", Id_Tanque = 1, Id_Planta = 32, Descripcion = "LOX-02" });
                ListTanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 2, Id_Planta = 1, Descripcion = "LNI-01" });
                ListTanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 3, Id_Planta = 32, Descripcion = "LNI-04" });
                ListTanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 3, Id_Planta = 2, Descripcion = "LNI-02" });

                foreach (var item in ListTanks.Where(x => x.Product_Id == product))
                {
                    if (plantsByUser == null)
                    {
                        tanks.Add(new TankViewModel
                        {
                            PlantId = item.Id_Planta,
                            ProductId = item.Product_Id,
                            TankId = item.Id_Tanque.ToString(),
                            TankGeneratedId = item.Id_Tanque + item.Descripcion + item.Id_Planta + item.Product_Id,
                            Descripcion = item.Descripcion
                        });
                    }
                    else
                    {
                        if (plantsByUser != null)
                        {
                            foreach (var itemx in plantsByUser.ToList().Where(x => x.ToString() != ""))
                            {
                                if (itemx.ToString() == item.Id_Planta.ToString())
                                {
                                    tanks.Add(new TankViewModel
                                    {
                                        PlantId = item.Id_Planta,
                                        ProductId = item.Product_Id,
                                        TankId = item.Id_Tanque.ToString(),
                                        TankGeneratedId = item.Id_Tanque + item.Descripcion + item.Id_Planta + item.Product_Id,
                                        Descripcion = item.Descripcion
                                    });
                                }
                            }
                        }

                    }
                }
            }
            return Task.FromResult(tanks);
        }

        public Task<List<SelectListItem>> GetPlantsXuser(string[] plantsId, List<SelectListItem> Plants)
        {
            List<SelectListItem> PlantsResult = new List<SelectListItem>();
            if (plantsId != null)
            {
                foreach (var item in Plants)
                {
                    foreach (var itemUsr in plantsId)
                    {
                        if (itemUsr == item.Value.ToString())
                        {
                            PlantsResult.Add(new SelectListItem() { Text = item.Text, Value = item.Value });
                        }
                    }
                }

            }
            return Task.FromResult(PlantsResult);
        }

        public async Task<List<SelectListItem>> GetStates()
        {
            List<SelectListItem> States = new List<SelectListItem>();
            var states = await _stateRepository.GetAsync(x => x.Active == true);
            foreach (var item in states)
            {
                States.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Description });
            }
            return States;
        }

        public async Task<List<PenddingTaskModel>> GetPenddingTasks(string userCurrent, List<string> filterStatus)
        {
            var plants = await GetPlants();
            var products = await GetProducts();
            var opList = await _productionOrderRepository.GetAsync(x => (x.CreatedBy == userCurrent || x.DelegateUser == userCurrent));
            var oaList = await _conditioningOrderRepository.GetAsync(x => (x.CreatedBy == userCurrent || x.DelegateUser == userCurrent) && !filterStatus.Contains(x.State));
            var opIds = opList.Select(x => x.Id).ToList();
            var opIdsOA = oaList.Select(x => x.ProductionOrderId).ToList();
            var batchDetails = await _batchDetailsRepository.GetAsync(x => opIds.Contains(x.ProductionOrderId));
            var batchDetailsOA = await _batchDetailsRepository.GetAsync(x => opIdsOA.Contains(x.ProductionOrderId));
            var penddingTasks = opList.Where(x => !filterStatus.Contains(x.State)).Select(x => new PenddingTaskModel
            {
                Id = x.Id,
                FechaDeCreacion = (DateTime)x.CreatedDate,
                NumeroLoteProduccion = batchDetails.FirstOrDefault(y => y.ProductionOrderId == x.Id)?.Number ?? _resource.GetString("ForLotify"),
                Planta = plants.Where(pl => pl.Value == x.PlantId).SingleOrDefault().Text,
                Producto = products.Where(pr => pr.Value == x.ProductId).SingleOrDefault().Text,
                Estado = !string.IsNullOrEmpty(x.State) ? x.State : "Nueva",
                ActividadACompletar = x.StepSavedDescription,
                MotivoCancelacion = !string.IsNullOrEmpty(x.ReasonReject) ? x.ReasonReject : "NA",
                Tanque = x.TankId,
                ProductionOrderId = x.Id
            }).ToList();
            var tempTask = oaList.Select(x => new PenddingTaskModel
            {
                Id = x.Id,
                FechaDeCreacion = (DateTime)x.CreatedDate,
                NumeroLoteProduccion = batchDetailsOA.FirstOrDefault(y => y.ProductionOrderId == x.ProductionOrderId)?.Number ?? _resource.GetString("ForLotify"),
                Planta = plants.Where(pl => pl.Value == x.PlantId).SingleOrDefault().Text,
                Producto = products.Where(pr => pr.Value == x.ProductId).SingleOrDefault().Text,
                Estado = !string.IsNullOrEmpty(x.State) ? x.State : "Nueva",
                ActividadACompletar = x.StepSavedDescription,
                MotivoCancelacion = !string.IsNullOrEmpty(x.ReasonReject) ? x.ReasonReject : "NA",
                Tanque = opList.Where(y => y.Id == x.ProductionOrderId).Any() ? opList.FirstOrDefault(y => y.Id == x.ProductionOrderId)?.TankId : (this.GetTankIdPenddingTask(x.ProductionOrderId).Result),
                ProductionOrderId = x.ProductionOrderId
            }).ToList();
            penddingTasks.AddRange(tempTask);
            return penddingTasks;
        }

        public async Task<List<SelectListItem>> GetActivities()
        {
            List<SelectListItem> Activities = new List<SelectListItem>();
            var states = await _activitieRepository.GetAsync(x => x.Active == true);
            foreach (var item in states)
            {
                Activities.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Description });
            }
            return Activities;
        }

        //sin llamadas a nivel superior de OA
        public async Task<List<QueryFilesModel>> GetQueryFiles()
        {
            List<QueryFilesModel> queryFilesModels = new List<QueryFilesModel>();

            var plants = await GetPlants();
            var products = await GetProducts();

            var opList = await _productionOrderRepository.GetAllAsync();

            foreach (var p in opList)
            {
                var batch = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == p.Id)).FirstOrDefault();
                var pipelineClearance = (await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId == p.Id)).FirstOrDefault();
                //var conditioningOrderDb = (await _conditioningOrderRepository.GetAsync(x => x.ProductionOrderId == p.Id)).FirstOrDefault();
                var conditioningOrderDb = await _conditioningOrderRepository.FirstOrDefaultAsync(x => x.ProductionOrderId == p.Id);

                var customerFiles = new List<PipeFillingCustomersFiles>();

                customerFiles = conditioningOrderDb != null ?
                    (await _pipeFillingCustomersFilesRepository.GetAsync(x => x.ConditioningOrderId == conditioningOrderDb.Id)).ToList() : new List<PipeFillingCustomersFiles>();
                queryFilesModels.Add(new QueryFilesModel
                {
                    Id = p.Id,
                    Plant = plants.Where(pl => pl.Value == p.PlantId).SingleOrDefault().Text,
                    PlantId = p.PlantId,
                    Product = products.Where(pr => pr.Value == p.ProductId).SingleOrDefault().Text,
                    ProductId = p.ProductId,
                    State = p.State == ProductionOrderStatus.Released.Value ? conditioningOrderDb.State : p.State,
                    NoLotProd = batch?.Number ?? _resource.GetString("ForLotify"),
                    Tank = p.TankId,
                    TankId = p.TankId,
                    StartDateExp = Convert.ToDateTime(p.CreatedDate).Date,
                    StartDateProd = pipelineClearance?.ReviewedDate.Value,
                    StartTimeProd = pipelineClearance?.ReviewedDate.Value.ToString("HH:mm") ?? _resource.GetString("NoInformation"),
                    EndDateProd = p.EndDate != null ? p.EndDate.Value.Date : null,
                    EndTimeProd = p.EndDate != null ? p.EndDate.Value.ToString("HH:mm") : _resource.GetString("NoInformation"),
                    SizeLot = batch?.Size.ToString() ?? _resource.GetString("NoInformation"),
                    Dictum = p.IsReleased == true ? "Liberado" : "No liberado",
                    Comments = p.ReasonReject != null ? p.ReasonReject : _resource.GetString("CommentReasonReject"),
                    Certified = customerFiles.Any() ? _resource.GetString("Certified").Value : _resource.GetString("NA").Value
                });
            }

            return queryFilesModels;
        }

        public async Task<DetailOP> GetDetailOP(int id)
        {
            var detailOP = new DetailOP();

            var entity = await _productionOrderRepository.GetByIdAsync(id);

            if (entity != null)
            {
                detailOP = new DetailOP
                {
                    State = entity.State,
                    ReasonCancellation = entity.State == ConditioningOrderStatus.Cancelled.Value ?
                                         entity.ReleasedNotes != null ? entity.ReleasedNotes : "NA" : "NA",
                    CalibrationStatus = entity.StepSaved > 1 ? "Concluido" : "Por concluir",
                    LineBreak = entity.StepSaved > 2 ? "Concluido" : "Por concluir",
                    StageMonitoring = entity.StepSaved > 3 ? "Concluido" : "Por concluir",
                    Chromotogram = entity.StepSaved > 3 && !string.IsNullOrEmpty(entity.ProductId) && entity.ProductId == "NI" ? "Disponible" : string.Empty, //AHF
                    Lotification = entity.StepSaved > 5 ? "Concluido" : "Por concluir",
                    ProductionOrder = entity.Id,
                    ProductId = entity.ProductId
                };

            }

            return detailOP;
        }

        //sin llamadas a nivel superior de OA
        public async Task<List<DetailOA>> GetDetailOA(int idOP)
        {
            var detailOA = new List<DetailOA>();

            var entityList = await _conditioningOrderRepository.GetAsync(x => x.ProductionOrderId == idOP);

            if (entityList?.Any() ?? false)
            {
                var ProcessDB = await _performanceProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == entityList.First().Id);
                detailOA = entityList.Select(x => new DetailOA
                {
                    State = x.State,
                    CalibrationAnalyticalEquipment = x.StepSaved > 1 ? "Concluido" : "Por concluir",
                    CalibrationScalelEquipment = x.StepSaved > 2 ? "Concluido" : "Por concluir",
                    LineBreak = x.StepSaved > 2 ? "Concluido" : "Por concluir",
                    Accessories = x.StepSaved > 2 ? "Concluido" : "Por concluir",
                    TotalProduct = (ProcessDB?.Any() ?? false) ? "Concluido" : _resource.GetString("Pendding"),
                    ConditioningOrder = x.Id,
                    FinalChromotogram = x.StepSaved >= 3 && !string.IsNullOrEmpty(x.ProductId) && x.ProductId == "NI" ? "Disponible" : string.Empty, //AHF
                    InitialChromotogram = x.StepSaved >= 3 && !string.IsNullOrEmpty(x.ProductId) && x.ProductId == "NI" ? "Disponible" : string.Empty, //AHF,
                    ProductId = x.ProductId
                }).ToList();
            }

            return detailOA;
        }

        public async Task<List<DetailDistributionBatch>> GetDetailDB(int idOP, ConditioningOrder conditioningOrder, ProductionOrder productionOrder)
        {
            var distributionBatchList = new List<DetailDistributionBatch>();
            List<PipeFillingControlViewModel> listpipeFillingControls = new List<PipeFillingControlViewModel>();
            //var productionOrder = await _productionOrderRepository.GetByIdAsync(idOP);
            //if (productionOrder == null)
            //{
            //    return null;
            //}

            //var oaList = (await _conditioningOrderRepository.GetAsync(x => x.ProductionOrderId == idOP)).FirstOrDefault();
            //var conditioningOrder = await _conditioningOrderRepository.FirstOrDefaultAsync(x => x.ProductionOrderId == idOP);
            if (conditioningOrder == null)
            {
                return null;
            }

            var checkList = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == conditioningOrder.Id);
            var epc = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == conditioningOrder.Id);
            if (epc == null || !epc.Any())
            {
                return null;
            }

            var tournumberList = epc.Select(x => x.TourNumber).Distinct();

            var ldetalledist = new List<VwLotesDistribuicionDetalle>();
            var pipeFillins = await _conditioningOrderService.GetAnalisisCliente(conditioningOrder.Id, productionOrder.ProductId, productionOrder.PlantId, conditioningOrder.CreatedDate, tournumberList.ToList());
            foreach (var tournumber in tournumberList)
            {
                var pipeDetailsList = pipeFillins.Where(x => x.TourNumber == tournumber).ToList();
                foreach (var item in pipeDetailsList)
                {
                    if (WSMexeFuncionalidad)
                    {
                        distributionBatchList.Add(new DetailDistributionBatch
                        {
                            ProductionOrderId = productionOrder.Id,
                            ConditioningOrderId = conditioningOrder.Id,
                            DistributionBatch = item.DistributionBatch,
                            ProductId = productionOrder.ProductId,
                            Items = (await _pipeFillingCustomerRepository.GetAsync(x => x.DistributionBatch == item.DistributionBatch))
                                                    .Select(x => new DetailDistributionBatchItem
                                                    {
                                                        TourNumber = x.TourNumber,
                                                        OrderNumber = x.DeliveryNumber,
                                                        PipeNumber = x.DistributionBatch?.Split('-')[3],
                                                        ChecklistStatus = checkList.Where(y => y.TourNumber == x.TourNumber).Any()
                                                            ? checkList.Where(y => y.TourNumber == x.TourNumber).Select(x => x.Status).LastOrDefault() : _resource.GetString("Pendding"),
                                                        CheckListId = checkList.Where(y => y.TourNumber == x.TourNumber).Any()
                                                            ? checkList.Where(y => y.TourNumber == x.TourNumber).Select(x => x.Id).LastOrDefault().ToString() : null,
                                                        InitialAnalysis = item.InitialAnalysis.Any() ? "Concluido" : _resource.GetString("Pendding"),
                                                        InitialChromatogram = conditioningOrder.StepSaved >= 3 && !string.IsNullOrEmpty(x.ProductId) && x.ProductId == "NI" ? "Disponible" : string.Empty, //AHF,
                                                        FinalAnalysis = item.FinalAnalysis.Any() ? "Concluido" : _resource.GetString("Pendding"),
                                                        FinalChromatogram = conditioningOrder.StepSaved >= 3 && !string.IsNullOrEmpty(x.ProductId) && x.ProductId == "NI" ? "Disponible" : string.Empty, //AHF,
                                                        CustomerTank = x.Tank,
                                                        CustomerName = x.Name,
                                                        AnalysisReport = item.IsReleased.HasValue && item.IsReleased == true ? x.AnalysisReport != null ? x.AnalysisReport : _resource.GetString("Pendding") : "NA",
                                                        ReviewedDate = x.ReviewedDate != null ? x.ReviewedDate : null,
                                                        DistributionBatch = x.DistributionBatch,
                                                        Id = x.Id
                                                    }).ToList()

                        });
                    }
                    else
                    {
                        distributionBatchList = new List<DetailDistributionBatch>
                        {
                            new DetailDistributionBatch
                            {
                                ProductionOrderId = conditioningOrder.Id,
                                DistributionBatch = item.DistributionBatch,
                                Items = new List<DetailDistributionBatchItem> {
                                    new DetailDistributionBatchItem {
                                        TourNumber = "123459",
                                        OrderNumber = "",
                                        PipeNumber = item.DistributionBatch?.Split('-')[3],
                                        ChecklistStatus = "",
                                        InitialAnalysis = "",
                                        InitialChromatogram = "",
                                        FinalAnalysis = "",
                                        FinalChromatogram = "",
                                        CustomerTank = "M31-LOX-01",
                                        CustomerName = "Instituto Mexicano del Seguro Social",
                                        AnalysisReport = ""
                                    }
                                }
                            }
                        };
                    }
                }
            }


            return distributionBatchList;
        }

        public async Task<List<QueryGeneralModel>> GetQueryGeneral()
        {
            List<QueryGeneralModel> queryGeneralModels = new List<QueryGeneralModel>();

            var plants = await GetPlants();
            var products = await GetProducts();

            var oaList = await _conditioningOrderRepository.GetAllAsync();

            foreach (var oa in oaList)
            {
                var op = (await _productionOrderRepository.GetAsync(x => x.Id == oa.ProductionOrderId)).FirstOrDefault();
                var batch = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == oa.ProductionOrderId)).FirstOrDefault();
                var pipeFillingList = await _pipeFillingControlRepository.GetAsync(x => x.TourNumber != null && x.ConditioningOrderId != 0 && x.ConditioningOrderId == oa.Id);

                foreach (var con in pipeFillingList)
                {
                    var fill = (await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == con.Id)).ToList();
                    if (fill.Any())
                    {
                        foreach (var pipefilling in fill)
                        {
                            var checklists = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == oa.Id && x.DistributionBatch == pipefilling.DistributionBatch);

                            var customers = await _pipeFillingCustomerRepository.GetAsync(x => x.ConditioningOrderId == oa.Id && x.DistributionBatch == pipefilling.DistributionBatch && x.TourNumber == con.TourNumber);

                            foreach (var customer in customers)
                            {
                                queryGeneralModels.Add(new QueryGeneralModel
                                {
                                    Id = op?.Id ?? 0,
                                    StartDateOP = op?.CreatedDate,
                                    NoLotProd = batch?.Number ?? _resource.GetString("NoInformation"),
                                    Tank = op?.TankId ?? _resource.GetString("NoInformation"),
                                    TankId = op?.TankId,
                                    StartDateOA = oa.CreatedDate,
                                    NoLotDist = pipefilling?.DistributionBatch ?? _resource.GetString("NoInformation"),
                                    NoTankClient = customer.Tank ?? _resource.GetString("NoInformation"),
                                    NameClient = customer.Name ?? _resource.GetString("NoInformation"),
                                    AnalysisReport = customer.AnalysisReport ?? _resource.GetString("NoInformation"),
                                    Comments = oa.ReasonReject ?? _resource.GetString("CommentReasonReject"),
                                    Plant = plants.Where(p => p.Value == op?.PlantId).SingleOrDefault().Text ?? _resource.GetString("NoInformation"),
                                    PlantId = op?.PlantId,
                                    Product = products.Where(p => p.Value == op?.ProductId).SingleOrDefault().Text ?? _resource.GetString("NoInformation"),
                                    ProductId = op?.ProductId,
                                    CheckList = checklists.LastOrDefault()?.Status ?? _resource.GetString("NoInformation"),
                                    State = op?.State ?? _resource.GetString("NoInformation"),
                                    ProductionOrderId = op.Id,
                                    ConditioningOrderId = oa.Id,
                                    TourNumber = customer.TourNumber ?? _resource.GetString("NoInformation"),
                                    PipeNumber = !string.IsNullOrEmpty(customer.DistributionBatch) ? customer.DistributionBatch.Split('-')?[3] : _resource.GetString("NoInformation"),
                                    distributionBatch = customer.DistributionBatch ?? _resource.GetString("NoInformation"),
                                });
                            }
                        }
                    }
                }
            }

            return queryGeneralModels.Distinct().ToList();
        }

        public async Task<List<SelectListItem>> GetProductsXPlants(string[] plantsByUser, List<SelectListItem> PlantsFilter, int function = 0)
        {
            List<SelectListItem> response = new List<SelectListItem>();
            Dictionary<int, string> productsFiltered = new Dictionary<int, string>();
            GeneralViewModel model = new GeneralViewModel();
            if (WSMexeFuncionalidad)
            {
                //fill filters
                var general = await _generalCatalogRepository.GetAllAsync();
                if (general.Count == 0)
                {
                    var productos = _productoRepository.GetAll();
                    foreach (var item in productos)
                    {
                        response.Add(new SelectListItem { Value = item.Product_Id, Text = item.Product_Name });
                    }
                }
                else
                {

                    model.PlantsFilter = PlantsFilter;
                    model.ProductsFilter = await GetProdutsItemsAsync(plantsByUser);
                    List<SelectListItem> replace = new List<SelectListItem>();
                    foreach (var item in model.ProductsFilter)
                    {
                        if (item.Text == _resource.GetString("Oxygen"))
                        {
                            replace.Add(new SelectListItem { Text = _resource.GetString("Oxygen"), Value = "OX" });
                        }
                        if (item.Text == _resource.GetString("Nitrogen"))
                        {
                            replace.Add(new SelectListItem { Text = _resource.GetString("Nitrogen"), Value = "NI" });
                        }
                        if (item.Text == _resource.GetString("Argon"))
                        {
                            replace.Add(new SelectListItem { Text = _resource.GetString("Argon"), Value = "AR" });
                        }
                        if (item.Text == _resource.GetString("Hydrogen"))
                        {
                            replace.Add(new SelectListItem { Text = _resource.GetString("Hydrogen"), Value = "HIDRÓGENO" });
                        }
                        if (item.Text == _resource.GetString("CO2"))
                        {
                            replace.Add(new SelectListItem { Text = _resource.GetString("CO2"), Value = "CD" });
                        }
                    }
                    response = replace;

                }
            }
            else
            {

                //fill filters
                model.PlantsFilter = PlantsFilter;
                model.ProductsFilter = await GetProdutsItemsAsync(plantsByUser);
                model.GeneralList = new List<General>();
                var general = await _generalCatalogRepository.GetAllAsync();
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<General>>(general);
                if (mapped != null)
                {
                    foreach (var item in mapped)
                    {
                        //fill external catalogs

                        var plantText = model.PlantsFilter.Find(p => p.Value == Convert.ToString(item.PlantId));
                        var productText = model.ProductsFilter.Find(p => p.Value == Convert.ToString(item.ProductId));
                        item.PlantId = plantText?.Text;
                        item.ProductId = productText?.Text;
                    }
                }
                model.GeneralList = (List<General>)mapped;

                foreach (var item in model.GeneralList)
                {
                    if (item.ProductId.Contains(_resource.GetString("Oxygen")))
                    {
                        response.Add(new SelectListItem { Value = "OX", Text = item.ProductId });
                    }
                    if (item.ProductId.Contains(_resource.GetString("Nitrogen")))
                    {
                        response.Add(new SelectListItem { Value = "NI", Text = item.ProductId });
                    }
                    if (item.ProductId.Contains(_resource.GetString("Argon")))
                    {
                        response.Add(new SelectListItem { Value = "AR", Text = item.ProductId });
                    }
                    if (item.ProductId.Contains(_resource.GetString("Hydrogen")))
                    {
                        response.Add(new SelectListItem { Value = "HY", Text = item.ProductId });
                    }
                    if (item.ProductId.Contains(_resource.GetString("CO2")))
                    {
                        response.Add(new SelectListItem { Value = "CD", Text = item.ProductId });
                    }
                }



            }

            var group = response.GroupBy(x => new { x.Text, x.Value });
            response = new List<SelectListItem>();
            foreach (var item in group)
            {
                response.Add(new SelectListItem { Value = item.Key.Value, Text = item.Key.Text });
            }

            return response;
        }

        private async Task<List<SelectListItem>> GetProdutsItemsAsync(string[] plantsByUser)
        {
            List<SelectListItem> response = new List<SelectListItem>();
            Dictionary<string, string> productsFiltered = new Dictionary<string, string>();
            //gets products
            if (plantsByUser != null)
            {
                foreach (var item in plantsByUser.ToList().Where(x => x.ToString() != ""))
                {
                    var products = await _analyticsCertsService.GetProducts(int.Parse(item));
                    if (products.Count() != 0)
                    {
                        foreach (var p in products)
                        {
                            if (!productsFiltered.ContainsKey(p.Key))
                                productsFiltered.Add(p.Key, p.Value);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, string> entry in productsFiltered)
            {
                response.Add(new SelectListItem
                {
                    Text = entry.Value,
                    Value = Convert.ToString(entry.Key)

                });
            }


            return response;
        }
        public Task<List<SelectListItem>> GetStatesCheckList()
        {
            List<SelectListItem> StatesList = new List<SelectListItem>();
            StatesList.Add(new SelectListItem { Text = "CL - En proceso", Value = "1" });
            StatesList.Add(new SelectListItem { Text = "CL - En cancelación", Value = "2" });
            StatesList.Add(new SelectListItem { Text = "CL - Cancelado", Value = "3" });
            StatesList.Add(new SelectListItem { Text = "CL - Liberado", Value = "4" });
            return Task.FromResult(StatesList);
        }

        public async Task<List<SelectListItem>> GetActions()
        {
            List<SelectListItem> ActionsList = new List<SelectListItem>();
            var ActionsDb = await _activitiesReportAuditRepository.GetAllAsync();
            foreach (var item in ActionsDb)
            {
                ActionsList.Add(new SelectListItem { Text = item.Description, Value = item.Value });
            }
            return ActionsList;
        }



        public async Task<List<CheckListVM>> GenerateCheckListCat(int preview = 0, int conditioningOrder = 0, ConditioningOrder conditioning = null)
        {
            var CheckListPipeAnswerCatalog = new List<CheckListVM>();

            if (preview == 0 && conditioningOrder == 0)
            {
                _logger.LogError($"Parametros inválidos {preview} - {conditioningOrder}");
                return new List<CheckListVM>();
            }

            if (preview != 0)
            {
                var data = from tbl in await _leyendsPreviewTextRepository.GetAsync(x => x.ProductId.Equals("CheckList"))
                           select new CheckListVM
                           {
                               Id = tbl.Id,
                               Requirement = tbl.Text,
                               Verification = "",
                               Description = "",
                               Notify = "",
                               Action = "",
                               Alias = tbl.Title,
                               Group = tbl.Type.Equals(LayoutLeyendsType.CheckListIV.Value) ? "IV" : "FP",
                               Step = tbl.Step,
                           };
                CheckListPipeAnswerCatalog = data.OrderByDescending(x => x.Group).ThenBy(x => x.Step).ToList();
            }
            else
            {
                var data = await CatalogLeyendsforConditioningOrder(conditioningOrder, conditioning.GroupIdChecklist);
                CheckListPipeAnswerCatalog = data.OrderByDescending(x => x.Group).ThenBy(x => x.Step).ToList();
            }
            return CheckListPipeAnswerCatalog;
        }

        public Task<List<CheckListPipeRecordAnswer>> CheckListRecord(int NumOA)
        {
            List<CheckListPipeRecordAnswer> record = new List<CheckListPipeRecordAnswer>();
            var DbObject = _checkListPipeRecordAnswerRepository.GetByIdAsync(NumOA);

            return Task.FromResult(record);

        }

        public async Task<CheckListVM> CheckListRecordLabels(int NumOA, int checkListId, int productionOrderId)
        {
            CheckListVM record = new CheckListVM();
            if (WSMexeFuncionalidad)
            {
                //var OAdb = await _conditioningOrderRepository.GetByIdAsync(NumOA);
                //var OAdb = await _conditioningOrderRepository.FirstOrDefaultAsync(NumOA);
                //var OPdb = await _productionOrderRepository.GetByIdAsync(OAdb.ProductionOrderId);
                var OPdb = await _productionOrderRepository.GetByIdAsync(productionOrderId);
                var Productdb = _productoRepository.GetAsync(x => x.Product_Id == OPdb.ProductId).FirstOrDefault();
                var Plantdb = _plantasRepository.GetAsync(x => x.Id_Planta == int.Parse(OPdb.PlantId)).FirstOrDefault();
                var PipeDb = await _checkListPipeDictiumAnswerRepository.GetByIdAsync(checkListId);
                if (PipeDb.DistributionBatch != "")
                    record.Pipe = PipeDb.DistributionBatch.Split('-')[3];
                record.Localizate = Plantdb.Descripcion;
                record.Product = Productdb.Product_Name;
                record.Alias = PipeDb.Alias;
            }
            else
            {
                //var OAdb = await _conditioningOrderRepository.GetByIdAsync(NumOA);
                //var OAdb = await _conditioningOrderRepository.FirstOrDefaultAsync(NumOA);
                //var OPdb = await _productionOrderRepository.GetByIdAsync(OAdb.ProductionOrderId);
                var OPdb = await _productionOrderRepository.GetByIdAsync(productionOrderId);
                var Productdb = (await _productCatalogRepository.GetAsync(x => x.ProductId == OPdb.ProductId)).FirstOrDefault();
                var Plantdb = (await GetPlants()).Where(x => x.Value == OPdb.PlantId).FirstOrDefault();
                var PipeDb = await _checkListPipeDictiumAnswerRepository.GetByIdAsync(checkListId);
                record.Pipe = PipeDb.DistributionBatch.Split('-')[3];
                record.Localizate = Plantdb.Text;
                record.Product = Productdb.ProductId;
            }

            return record;

        }

        public async Task<List<HistoryNotes>> HistoryNotestRecord(int NumOA, int option)
        {
            List<HistoryNotes> record = new List<HistoryNotes>();
            if (option == 1)
            {
                var DbObject = await _historyNotesRepository.GetAsync(x => x.ProductionOrderId == NumOA);
                record = DbObject.ToList();
            }

            return record;

        }

        public async Task<List<HistoryNotes>> GetHistoryDetail(int Id, int option)
        {
            List<HistoryNotes> record = new List<HistoryNotes>();
            var userDb = _userManager.Users;
            if (option == 1)
            {
                var historyNotes = from hist in await _historyNotesRepository.GetAsync(x => x.ProductionOrderId == Id && x.Type == HistoryNotesType.OrdenProduccion.Value)
                                   select new HistoryNotes
                                   {
                                       ProductionOrderId = hist.ProductionOrderId,
                                       Source = hist.Source,
                                       Note = hist.Note,
                                       User = userDb.Where(x => x.Id == hist.User).FirstOrDefault().NombreUsuario,
                                       Date = hist.Date,
                                       Type = hist.Type
                                   };
                record = historyNotes.ToList();
            }
            else
            {
                var historyNotes = from hist in await _historyNotesRepository.GetAsync(x => x.ProductionOrderId == Id && x.Type == HistoryNotesType.OrdenAcondicionamiento.Value)
                                   select new HistoryNotes
                                   {
                                       ProductionOrderId = hist.ProductionOrderId,
                                       Source = hist.Source,
                                       Note = hist.Note,
                                       User = userDb.Where(x => x.Id == hist.User).FirstOrDefault().NombreUsuario,
                                       Date = hist.Date,
                                       Type = hist.Type
                                   };
                record = historyNotes.ToList();
            }
            return record;
        }

        public async Task<List<SechToolDistributionBatchViewModel>> GetDistributions(string DistributionBatch)
        {
            var union = new List<SechToolDistributionBatchViewModel>();
            try
            {
                var batchDB = (from batch in await _batchDetailsRepository.GetAsync(x => x.Number.Contains(DistributionBatch.Trim()))
                               select new SechToolDistributionBatchViewModel
                               {
                                   DistributionBatch = batch.Number,
                                   Source = "ProductionOrder",
                                   ProductionOrderId = batch.ProductionOrderId
                               });


                var fillDB = (from fill in await _pipeFillingRepository.GetAsync(x => x.DistributionBatch.Contains(DistributionBatch.Trim()))
                              select new SechToolDistributionBatchViewModel
                              {
                                  DistributionBatch = fill.DistributionBatch.Trim(),
                                  Source = "ConditioningOrder",
                                  ConditioningOrderId = fill.ConditioningOrderId
                              });

                union = batchDB.ToList().Union(fillDB.ToList()).ToList();
            }
            catch (Exception ex)
            {

            }

            return union.ToList();
        }

        public async Task<List<GeneralCatalog>> GetLimits()
        {
            List<GeneralCatalog> generals = new List<GeneralCatalog>();
            var limits = (await _generalCatalogRepository.GetAllAsync());
            generals = limits.ToList();
            return generals;
        }
        public async Task<List<SechToolDistributionBatchViewModel>> GetCheckListDictiumAnswer(int ConditioningOrderId, int option)
        {
            var ListCheckList = new List<SechToolDistributionBatchViewModel>();
            try
            {
                if (option == 1)
                {
                    var ListDB = (from checkList in await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == ConditioningOrderId)
                                  select new SechToolDistributionBatchViewModel
                                  {
                                      Id = checkList.Id,
                                      DistributionBatch = checkList.DistributionBatch,
                                      Source = checkList.Source.ToString(),
                                      ConditioningOrderId = checkList.NumOA,
                                      TourNumber = checkList.TourNumber,
                                      PipeNumber = checkList.PipeNumber,
                                      RelationShip = checkList.RelationShip,
                                      File = checkList.File,
                                      Alias = checkList.Alias,
                                      Status = string.IsNullOrEmpty(checkList.Status) ? _resource.GetString("NoInformation").Value : checkList.Status
                                  });
                    ListCheckList = ListDB.ToList();
                }
                else
                {
                    var ListDB = (await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == ConditioningOrderId))
                                  .Where(x => x.Status != CheckListType.CloseNo.Value).ToList();

                    var filtered = (from checkList in ListDB
                                    where checkList.Status != CheckListType.Inprogress.Value
                                    select new SechToolDistributionBatchViewModel
                                    {
                                        Id = checkList.Id,
                                        DistributionBatch = checkList.DistributionBatch,
                                        Source = checkList.Source.ToString(),
                                        ConditioningOrderId = checkList.NumOA,
                                        TourNumber = checkList.TourNumber,
                                        PipeNumber = checkList.PipeNumber,
                                        RelationShip = checkList.RelationShip,
                                        File = checkList.File,
                                        Alias = checkList.Alias,
                                        Status = string.IsNullOrEmpty(checkList.StatusTwo) ? _resource.GetString("NoInformation").Value : checkList.StatusTwo
                                    });

                    ListCheckList = filtered.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
            }

            return ListCheckList;
        }


        private async Task<string> GetTankIdPenddingTask(int productionOrderId)
        {
            var tankId = string.Empty;
            var tankDB = await _productionOrderRepository.GetByIdAsync(productionOrderId);
            tankId = tankDB.TankId;
            return tankId;
        }


        public async Task<string> GetIdentificadorPlant(string plantId)
        {
            var planta = string.Empty;
            plantId = _plantasRepository.GetAsync(x => x.Id_Planta.Equals(int.Parse(plantId))).FirstOrDefault().Identificador;
            return plantId;
        }


        public async Task<List<CheckListVM>> CatalogLeyendsforConditioningOrder(int conditioningOrder, Guid groupCheckListId)
        {
            var checkList = new List<CheckListVM>();

            //var entity = await _conditioningOrderRepository.GetByIdAsync(conditioningOrder);
            //var entity = await _conditioningOrderRepository.FirstOrDefaultAsync(conditioningOrder);
            var historyGroup = await _leyendsTextHistoryGroupRepository.GetAsync(x => x.GroupId == groupCheckListId);
            //GetAsync(x => x.GroupId.Equals(entity.GroupIdChecklist)));

            var history = await _leyendsTextHistoryRepository.GetAsync(x =>
                       historyGroup.Select(x => x.Id).Contains(x.LeyendsTextHistoryGroupId));

            var data = from tbl1 in historyGroup
                       join tbl2 in history
                      on tbl1.Id equals tbl2.LeyendsTextHistoryGroupId
                       select new CheckListVM
                       {
                           Id = tbl1.Id,
                           Requirement = tbl2.Text,
                           Verification = "",
                           Description = "",
                           Notify = "",
                           Action = "",
                           Alias = tbl2.Title,
                           Group = tbl1.Type.Equals(LayoutLeyendsType.CheckListIV.Value) ? "IV" : "FP",
                           Step = tbl1.Step,
                       };
            checkList = data.ToList();
            return checkList;
        }

    }
}

