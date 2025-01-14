using AutoMapper;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.LayoutLeyendsViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LiberacionProductoWeb.Services
{
    public class LayoutLeyendTextService : ILayoutLeyendTextService
    {
        private readonly ILogger<LayoutLeyendTextService> _logger;
        private readonly ILeyendsTextRepository _leyendsTextRepository;
        private readonly IMapper _mapper;
        private readonly ILeyendsPreviewTextRepository _leyendsPreviewTextRepository;
        private readonly IProductionOrderService _productionOrderService;
        private readonly ILeyendsTextHistoryRepository _leyendsTextHistoryRepository;
        private readonly ILeyendsTextHistoryGroupRepository _leyendsTextHistoryGroupRepository;
        private readonly IProductCatalogRepository _productCatalogRepository;
        private readonly AppDbExternalContext _external;
        public LayoutLeyendTextService(ILogger<LayoutLeyendTextService> logger,
            ILeyendsTextRepository leyendsTextRepository,
            IMapper mapper,
            ILeyendsPreviewTextRepository leyendsPreviewTextRepository,
            IProductionOrderService productionOrderService,
            ILeyendsTextHistoryRepository leyendsTextHistoryRepository,
            ILeyendsTextHistoryGroupRepository leyendsTextHistoryGroupRepository,
            IProductCatalogRepository productCatalogRepository,
            AppDbExternalContext external)
        {
            this._logger = logger;
            this._leyendsTextRepository = leyendsTextRepository;
            this._mapper = mapper;
            this._leyendsPreviewTextRepository = leyendsPreviewTextRepository;
            this._productionOrderService = productionOrderService;
            this._leyendsTextHistoryRepository = leyendsTextHistoryRepository;
            this._leyendsTextHistoryGroupRepository = leyendsTextHistoryGroupRepository;
            this._productCatalogRepository = productCatalogRepository;
            this._external = external;
        }

        public async Task AddLayoutProductionOrder(LayoutLeyendsVM leyendsCertificateVM, bool active = false)
        {
            var insert = new List<LeyendsText>();
            var update = new List<LeyendsText>();
            var updateOld = new List<LeyendsText>();
            var history = new List<LeyendsTextHistory>();
            var groupId = Guid.NewGuid();
            try
            {
                foreach (var item in leyendsCertificateVM.LayoutLeyendsProductionOrders)
                {
                    var products = item.ProductId.Trim().Replace(" ", "").Split(",").ToList();
                    foreach (var itemx in products)
                    {
                        var Leyends = (await _leyendsTextRepository.
                            GetAsync(x => x.Type.Equals(item.Type)
                            && x.ProductId.Equals(itemx)
                            && x.Step.Equals(item.Step))).FirstOrDefault();
                        if (Leyends == null)
                        {
                            item.ProductId = itemx;
                            item.Active = active;
                            insert.Add(ObjectMapper.Mapper.Map<LeyendsText>(item));
                            history.Add(new LeyendsTextHistory
                            {
                                Title = item.Title,
                                Text = item.Text,
                                LeyendsTextHistoryGroup = new LeyendsTextHistoryGroup
                                {
                                    CreatedBy = item.CreatedBy,
                                    CreatedDate = item.CreatedDate,
                                    Step = item.Step,
                                    Type = item.Type,
                                    ProductId = itemx,
                                    GroupId = groupId
                                },
                            });
                        }
                        else
                        {
                            var entityClone = new LeyendsText();
                            entityClone = (LeyendsText)Leyends.Clone();
                            Leyends.ProductId = itemx;
                            Leyends.ModifyBy = item.ModifyBy;
                            Leyends.ModifyDate = item.ModifyDate;
                            Leyends.Title = item.Title;
                            Leyends.Text = item.Text;
                            Leyends.Active = active;
                            update.Add(ObjectMapper.Mapper.Map<LeyendsText>(Leyends));
                            updateOld.Add(entityClone);
                            history.Add(new LeyendsTextHistory
                            {
                                Title = item.Title,
                                Text = item.Text,
                                LeyendsTextHistoryGroup = new LeyendsTextHistoryGroup
                                {
                                    CreatedBy = item.CreatedBy,
                                    CreatedDate = item.CreatedDate,
                                    Step = item.Step,
                                    Type = item.Type,
                                    ProductId = itemx,
                                    GroupId = groupId
                                },
                            });
                        }
                    }
                }
                if (insert.Any())
                    await _leyendsTextRepository.AddAsync(insert);
                if (update.Any())
                    await _leyendsTextRepository.UpdateAsync(update, updateOld);
                if (active)
                    await _leyendsTextHistoryRepository.AddAsync(history);
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService AddLayoutProductionOrder " + ex);
                _logger.LogError(ex, "Error.");
            }
        }
        public async Task<LayoutLeyendsVM> ProductionOrderText(string productId = "OX")
        {
            var model = new LayoutLeyendsVM();
            var entity = new List<LeyendsText>();
            try
            {
                entity = (await _leyendsTextRepository.
                    GetAsync(x =>
                     x.Type.Equals(LayoutLeyendsType.ProductionOrder.Value)
                     && x.ProductId.Equals(productId))).ToList();
                model.TitleOne = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepOne.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepOne = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepOne.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.TitleTwo = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepTwo.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepTwo = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepTwo.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleThree = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepThree.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepThree = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepThree.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleFor = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepFor.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepFor = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepFor.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleFive = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepFive.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepFive = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepFive.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleSix = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepSix.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepSix = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepSix.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleSeven = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepSeven.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepSeven = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsProductionOrderSteps.StepSeven.Value)).
                    Select(x => x.Text).FirstOrDefault();

                var products = from tbl1 in (await _productCatalogRepository.GetAllAsync()).Where(x => x.Status).ToList()
                               join tbl2 in _external.LPM_VW_PRODUCTOS.Where(x => x.Product_Id != null).ToList()
                               on tbl1.ProductId equals tbl2.Product_Id
                               select new SelectListItem
                               {
                                   Text = tbl2.Product_Name,
                                   Value = tbl1.ProductId
                               };

                model.ListProducts = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                model.ListProducts = products.ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService ProductionOrderText " + ex);
                _logger.LogError(ex, "Error.");
            }
            return model;
        }
        public async Task AddLayoutPreviewProductionOrder(LayoutLeyendsVM leyendsCertificateVM)
        {
            var insert = new List<LeyendsPreviewText>();
            var update = new List<LeyendsPreviewText>();
            var updateOld = new List<LeyendsPreviewText>();
            try
            {
                foreach (var item in leyendsCertificateVM.LayoutLeyendsProductionOrders)
                {
                    var products = item.ProductId.Trim().Replace(" ", "").Split(",").ToList();
                    foreach (var itemx in products)
                    {
                        var Leyends = (await _leyendsPreviewTextRepository.
                            GetAsync(x => x.Type.Equals(item.Type)
                            && x.ProductId.Equals(itemx)
                            && x.Step.Equals(item.Step))).FirstOrDefault();
                        if (Leyends == null)
                        {
                            item.ProductId = itemx;
                            insert.Add(ObjectMapper.Mapper.Map<LeyendsPreviewText>(item));

                        }
                        else
                        {
                            var entityClone = new LeyendsPreviewText();
                            entityClone = (LeyendsPreviewText)Leyends.Clone();
                            Leyends.ProductId = itemx;
                            Leyends.ModifyBy = item.ModifyBy;
                            Leyends.ModifyDate = item.ModifyDate;
                            Leyends.Title = item.Title;
                            Leyends.Text = item.Text;
                            update.Add(ObjectMapper.Mapper.Map<LeyendsPreviewText>(Leyends));
                            updateOld.Add(entityClone);
                        }
                    }
                }
                if (insert.Any())
                    await _leyendsPreviewTextRepository.AddAsync(insert);
                if (update.Any())
                    await _leyendsPreviewTextRepository.UpdateAsync(update);
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService AddLayoutProductionOrder " + ex);
                _logger.LogError(ex, "Error.");
            }
        }
        public async Task<int> GetProductionOrderForProduct(string productId)
        {
            return await _productionOrderService.GetProductionOrderForProduct(productId);
        }
        public async Task AddLayoutConditioningOrder(LayoutLeyendsVM leyendsCertificateVM, bool active = false)
        {
            var insert = new List<LeyendsText>();
            var update = new List<LeyendsText>();
            var updateOld = new List<LeyendsText>();
            var history = new List<LeyendsTextHistory>();
            var groupId = Guid.NewGuid();
            try
            {
                foreach (var item in leyendsCertificateVM.LayoutLeyendsConditioningOrders)
                {
                    var products = item.ProductId.Trim().Replace(" ", "").Split(",").ToList();
                    foreach (var itemx in products)
                    {
                        var Leyends = (await _leyendsTextRepository.
                            GetAsync(x => x.Type.Equals(item.Type)
                            && x.ProductId.Equals(itemx)
                            && x.Step.Equals(item.Step))).FirstOrDefault();
                        if (Leyends == null)
                        {
                            item.ProductId = itemx;
                            item.Active = active;
                            insert.Add(ObjectMapper.Mapper.Map<LeyendsText>(item));
                            history.Add(new LeyendsTextHistory
                            {
                                Title = item.Title,
                                Text = item.Text,
                                LeyendsTextHistoryGroup = new LeyendsTextHistoryGroup
                                {
                                    CreatedBy = item.CreatedBy,
                                    CreatedDate = item.CreatedDate,
                                    Step = item.Step,
                                    Type = item.Type,
                                    ProductId = item.ProductId,
                                    GroupId = groupId
                                },
                            });
                        }
                        else
                        {
                            var entityClone = new LeyendsText();
                            entityClone = (LeyendsText)Leyends.Clone();
                            Leyends.ProductId = item.ProductId;
                            Leyends.ModifyBy = item.ModifyBy;
                            Leyends.ModifyDate = item.ModifyDate;
                            Leyends.Title = item.Title;
                            Leyends.Text = item.Text;
                            Leyends.Active = active;
                            update.Add(ObjectMapper.Mapper.Map<LeyendsText>(Leyends));
                            updateOld.Add(entityClone);
                            history.Add(new LeyendsTextHistory
                            {
                                Title = item.Title,
                                Text = item.Text,
                                LeyendsTextHistoryGroup = new LeyendsTextHistoryGroup
                                {
                                    CreatedBy = item.CreatedBy,
                                    CreatedDate = item.CreatedDate,
                                    Step = item.Step,
                                    Type = item.Type,
                                    ProductId = item.ProductId,
                                    GroupId = groupId
                                },
                            });
                        }
                    }
                }
                if (insert.Any())
                    await _leyendsTextRepository.AddAsync(insert);
                if (update.Any())
                    await _leyendsTextRepository.UpdateAsync(update, updateOld);
                if (active)
                    await _leyendsTextHistoryRepository.AddAsync(history);
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService AddLayoutConditioningOrder " + ex);
                _logger.LogError(ex, "Error.");
            }
        }
        public async Task AddLayoutPreviewConditioningOrder(LayoutLeyendsVM leyendsCertificateVM)
        {
            var insert = new List<LeyendsPreviewText>();
            var update = new List<LeyendsPreviewText>();
            var updateOld = new List<LeyendsPreviewText>();
            try
            {
                foreach (var item in leyendsCertificateVM.LayoutLeyendsConditioningOrders)
                {
                    var products = item.ProductId.Trim().Replace(" ", "").Split(",").ToList();
                    foreach (var itemx in products)
                    {
                        var Leyends = (await _leyendsPreviewTextRepository.
                            GetAsync(x => x.Type.Equals(item.Type)
                            && x.ProductId.Equals(itemx)
                            && x.Step.Equals(item.Step))).FirstOrDefault();
                        if (Leyends == null)
                        {
                            item.ProductId = itemx;
                            insert.Add(ObjectMapper.Mapper.Map<LeyendsPreviewText>(item));
                        }
                        else
                        {
                            var entityClone = new LeyendsPreviewText();
                            entityClone = (LeyendsPreviewText)Leyends.Clone();
                            Leyends.ProductId = item.ProductId;
                            Leyends.ModifyBy = item.ModifyBy;
                            Leyends.ModifyDate = item.ModifyDate;
                            Leyends.Title = item.Title;
                            Leyends.Text = item.Text;
                            update.Add(ObjectMapper.Mapper.Map<LeyendsPreviewText>(Leyends));
                            updateOld.Add(entityClone);
                        }
                    }
                }
                if (insert.Any())
                    await _leyendsPreviewTextRepository.AddAsync(insert);
                if (update.Any())
                    await _leyendsPreviewTextRepository.UpdateAsync(update);
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService AddLayoutPreviewConditioningOrder " + ex);
                _logger.LogError(ex, "Error.");
            }
        }
        public async Task<LayoutLeyendsVM> ConditioningOrderText(string productId = "OX")
        {
            var model = new LayoutLeyendsVM();
            var entity = new List<LeyendsText>();
            try
            {
                entity = (await _leyendsTextRepository.
                    GetAsync(x =>
                     x.Type.Equals(LayoutLeyendsType.ConditioningOrder.Value)
                     && x.ProductId.Equals(productId))).ToList();
                model.TitleZero = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepZero.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepZero = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepZero.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleOne = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepOne.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepOne = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepOne.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.TitleTwo = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepTwo.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepTwo = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepTwo.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleThree = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepThree.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepThree = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepThree.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleFor = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepFor.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepFor = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepFor.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleFive = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepFive.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepFive = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepFive.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleSix = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepSix.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepSix = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepSix.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleSeven = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepSeven.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepSeven = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepSeven.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleEight = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepEight.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.StepEight = entity.
                    Where(x => x.Step.Equals(LayoutLeyendsConditioningOrderSteps.StepEight.Value)).
                    Select(x => x.Text).FirstOrDefault();

                var products = from tbl1 in (await _productCatalogRepository.GetAllAsync()).Where(x => x.Status).ToList()
                               join tbl2 in _external.LPM_VW_PRODUCTOS.Where(x => x.Product_Id != null).ToList()
                               on tbl1.ProductId equals tbl2.Product_Id
                               select new SelectListItem
                               {
                                   Text = tbl2.Product_Name,
                                   Value = tbl1.ProductId
                               };

                model.ListProducts = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                model.ListProducts = products.ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService ProductionOrderText " + ex);
                _logger.LogError(ex, "Error.");
            }
            return model;
        }
        public async Task<Guid> GetlastGroupProduct(string productId, string type)
        {
            var leyends = await _leyendsTextHistoryGroupRepository.
                 GetAsync(x => x.ProductId == productId && x.Type == type);
            if (!(leyends?.Any() ?? false))
                throw new Exception($"No se encontraron leyendas para el ProductId: {productId} y Type: {type}");
            return leyends
            .GroupBy(guid => new { guid.GroupId, guid.CreatedDate })
            .OrderByDescending(Group => Group.Key.CreatedDate).FirstOrDefault().Key.GroupId;
            //.OrderByDescending(Group => Group.Max(gpo => gpo.CreatedDate)).FirstOrDefault().Key.GroupId;
        }

        public async Task<LayoutLeyendsVM> CheckListText()
        {
            var model = new LayoutLeyendsVM();
            var entity = new List<LeyendsText>();
            try
            {
                entity = (await _leyendsTextRepository.
                    GetAsync(x => x.ProductId.Equals("CheckList"))).ToList();


                model.TitleOne = entity.
                    Where(x => x.Step.Equals(CheckListSteps.LeyendOne.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.LeyendOne = entity.
                    Where(x => x.Step.Equals(CheckListSteps.LeyendOne.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.TitleTwo = entity.
                    Where(x => x.Step.Equals(CheckListSteps.LeyendTwo.Value)).
                    Select(x => x.Title).FirstOrDefault();

                model.LeyendTwo = entity.
                    Where(x => x.Step.Equals(CheckListSteps.LeyendTwo.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.StepOne = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepOne.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.StepTwo = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepTwo.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.StepThree = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepThree.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.StepFor = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepFor.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.StepFive = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepFive.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.StepSix = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepSix.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.StepSeven = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepSeven.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.StepEight = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepEight.Value)).
                    Select(x => x.Text).FirstOrDefault();


                model.StepNine = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepNine.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.StepTen = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepTen.Value)).
                    Select(x => x.Text).FirstOrDefault();

                model.StepEleven = entity.
                    Where(x => x.Step.Equals(CheckListSteps.StepEleven.Value)).
                    Select(x => x.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService ProductionOrderText " + ex);
                _logger.LogError(ex, "Error.");
            }
            return model;
        }

        public async Task AddLayoutCheckList(LayoutLeyendsVM leyendsCertificateVM, bool active = false)
        {
            var insert = new List<LeyendsText>();
            var update = new List<LeyendsText>();
            var updateOld = new List<LeyendsText>();
            var history = new List<LeyendsTextHistory>();
            var groupId = Guid.NewGuid();
            var checklistString = "CheckList";
            try
            {
                foreach (var item in leyendsCertificateVM.LayoutLeyendsCheckLists)
                {
                    var Leyends = (await _leyendsTextRepository.
                        GetAsync(x => x.Type.Equals(item.Type)
                        && x.ProductId.Equals(checklistString)
                        && x.Step.Equals(item.Step))).FirstOrDefault();
                    if (Leyends == null)
                    {
                        item.ProductId = checklistString;
                        item.Active = active;
                        insert.Add(ObjectMapper.Mapper.Map<LeyendsText>(item));
                        history.Add(new LeyendsTextHistory
                        {
                            Title = item.Title,
                            Text = item.Text,
                            LeyendsTextHistoryGroup = new LeyendsTextHistoryGroup
                            {
                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                Step = item.Step,
                                Type = item.Type,
                                ProductId = checklistString,
                                GroupId = groupId
                            },
                        });
                    }
                    else
                    {
                        var entityClone = new LeyendsText();
                        entityClone = (LeyendsText)Leyends.Clone();
                        Leyends.ProductId = checklistString;
                        Leyends.ModifyBy = item.ModifyBy;
                        Leyends.ModifyDate = item.ModifyDate;
                        Leyends.Title = item.Title;
                        Leyends.Text = item.Text;
                        Leyends.Active = active;
                        update.Add(ObjectMapper.Mapper.Map<LeyendsText>(Leyends));
                        updateOld.Add(entityClone);
                        history.Add(new LeyendsTextHistory
                        {
                            Title = item.Title,
                            Text = item.Text,
                            LeyendsTextHistoryGroup = new LeyendsTextHistoryGroup
                            {
                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                Step = item.Step,
                                Type = item.Type,
                                ProductId = checklistString,
                                GroupId = groupId
                            },
                        });
                    }

                }
                if (insert.Any())
                {
                    await _leyendsTextRepository.AddAsync(insert);
                }
                if (update.Any())
                {
                    await _leyendsTextRepository.UpdateAsync(update, updateOld);
                }
                if (active)
                    await _leyendsTextHistoryRepository.AddAsync(history);
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService AddLayoutCheckList " + ex);
                _logger.LogError(ex, "Error.");
            }
        }

        public async Task AddLayoutPreviewCheckList(LayoutLeyendsVM leyendsCertificateVM)
        {
            var insert = new List<LeyendsPreviewText>();
            var update = new List<LeyendsPreviewText>();
            var updateOld = new List<LeyendsPreviewText>();
            var checklistString = "CheckList";
            try
            {
                foreach (var item in leyendsCertificateVM.LayoutLeyendsCheckLists)
                {

                    var Leyends = (await _leyendsPreviewTextRepository.
                        GetAsync(x => x.Type.Equals(item.Type)
                        && x.ProductId.Equals(checklistString)
                        && x.Step.Equals(item.Step))).FirstOrDefault();
                    if (Leyends == null)
                    {
                        item.ProductId = checklistString;
                        insert.Add(ObjectMapper.Mapper.Map<LeyendsPreviewText>(item));

                    }
                    else
                    {
                        var entityClone = new LeyendsPreviewText();
                        entityClone = (LeyendsPreviewText)Leyends.Clone();
                        Leyends.ProductId = checklistString;
                        Leyends.ModifyBy = item.ModifyBy;
                        Leyends.ModifyDate = item.ModifyDate;
                        Leyends.Title = item.Title;
                        Leyends.Text = item.Text;
                        update.Add(ObjectMapper.Mapper.Map<LeyendsPreviewText>(Leyends));
                        updateOld.Add(entityClone);
                    }

                }
                if (insert.Any())
                {
                    await _leyendsPreviewTextRepository.AddAsync(insert.OrderByDescending(x => x.Type).ThenBy(x => x.Step));
                }
                if (update.Any())
                {
                    //update.AddRange(ObjectMapper.Mapper.Map<List<LeyendsPreviewText>>(GenerateLeyendsChecklist(leyendsCertificateVM)));
                    await _leyendsPreviewTextRepository.UpdateAsync(update);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un error en LayoutLeyendTextService AddLayoutPreviewCheckList " + ex);
                _logger.LogError(ex, "Error.");
            }
        }

        public async Task<Guid> GetlastGroupCheckList(string productId)
        {
            //return (await _leyendsTextHistoryGroupRepository.
            //   GetAsync(x => x.ProductId.Equals(productId)))
            //    .GroupBy(guid => new { guid.GroupId, guid.CreatedDate })
            //    .OrderByDescending(Group => Group.Key.CreatedDate).FirstOrDefault().Key.GroupId;
            //.OrderByDescending(Group => Group.Max(gpo => gpo.CreatedDate)).FirstOrDefault().Key.GroupId;
            var leyends = await _leyendsTextHistoryGroupRepository.
                GetAsync(x => x.ProductId == productId);
            if (!(leyends?.Any() ?? false))
                throw new Exception($"No se encontraron leyendas el checklist con ProductId: {productId}");
            return leyends
                .GroupBy(guid => new { guid.GroupId, guid.CreatedDate })
                .OrderByDescending(Group => Group.Key.CreatedDate).FirstOrDefault().Key.GroupId;
        }

        public async Task<int> GetConditioningOrderForProduct(string productId)
        {
            return await _productionOrderService.GetConditioningOrderForProduct(productId);
        }
    }
}
