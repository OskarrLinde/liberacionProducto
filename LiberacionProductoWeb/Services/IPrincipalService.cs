﻿using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.Principal;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface IPrincipalService
    {
        Task<List<SelectListItem>> GetPlants();
        Task<List<SelectListItem>> GetProducts();
        Task<List<SelectListItem>> GetTanks(string product, string[] plantsByUser = null, int function = 0);
        Task<List<TankViewModel>> GetTanksViewModel(string product, string[] plantsByUser = null, int function = 0);
        Task<List<SelectListItem>> GetPlantsXuser(string[] plantsId, List<SelectListItem> Plants);
        Task<List<SelectListItem>> GetStates();
        Task<List<PenddingTaskModel>> GetPenddingTasks(string userCurrent, List<string> filterStatus);
        Task<List<SelectListItem>> GetActivities();
        Task<List<QueryFilesModel>> GetQueryFiles();
        Task<DetailOP> GetDetailOP(int id);
        Task<List<DetailOA>> GetDetailOA(int idOP);
        Task<List<DetailDistributionBatch>> GetDetailDB(int idOP, ConditioningOrder conditioningOrder, ProductionOrder productionOrder);
        Task<List<QueryGeneralModel>> GetQueryGeneral();
        Task<List<SelectListItem>> GetProductsXPlants(string[] plantsByUser, List<SelectListItem> PlantsFilter, int function = 0);
        Task<List<SelectListItem>> GetStatesCheckList();
        Task<List<SelectListItem>> GetActions();
        Task<List<CheckListVM>> GenerateCheckListCat(int preview = 0, int conditioningOrder = 0, ConditioningOrder conditioning = null);
        Task<List<CheckListPipeRecordAnswer>> CheckListRecord(int NumOA);
        Task<CheckListVM> CheckListRecordLabels(int NumOA, int checkListId, int productionOrderId);
        Task<List<HistoryNotes>> HistoryNotestRecord(int NumOA, int option);
        Task<List<HistoryNotes>> GetHistoryDetail(int Id, int option);
        Task<List<SechToolDistributionBatchViewModel>> GetDistributions(string DistributionBatch);
        Task<List<GeneralCatalog>> GetLimits();
        Task<List<SechToolDistributionBatchViewModel>> GetCheckListDictiumAnswer(int ConditioningOrderId, int option);
        Task<string> GetIdentificadorPlant(string plantId);
        Task<List<CheckListVM>> CatalogLeyendsforConditioningOrder(int conditioningOrder, Guid groupCheckListId);
    }
}
