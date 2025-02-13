﻿using DevExpress.XtraReports.UI;
using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Reports;
using LiberacionProductoWeb.Reports.ConditioningOrder;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface IExportPDFService
    {
        public XtraReport GetReport(string json, string nombre, string valorMaximo, string valorMinimo, string rango, string lowlimit, string toplimit);
        public Task<ProductionOrderViewModel> GetGraphicReport(ProductionOrderViewModel model);
        public Task<ProductionOrderViewModel> GetOrderProductionModelById(int id, int preview = 0);
        public Task<ConditioningOrderViewModel> GetModel(int IdOP, int Id, ConditioningOrder conditioningOrder, bool style = false, int preview = 0);
        public Task<RptChkList> GetRptChkList(CheckListGeneralViewModel chkGeneral, ConditioningOrder conditioningOrder, int preview = 0);
        public Task<CheckListVM> GetRptCheckListVMs(CheckListVM model, ConditioningOrder conditioningOrder, int preview = 0);
        public Task<List<SelectListItem>> GetRptUsersItemsAsync(int id = -1);
        //public Task<Report1> GetRptCertificate(int IdOP, int Id, string tourNumber, string pipeNumber, string tank, int IdCustomer, ConditioningOrderViewModel model, string distributionBatch);
        public Task<Report1> GetRptCertificate(int IdOP, int Id, string tourNumber, string pipeNumber, string tank, ConditioningOrderViewModel model, string distributionBatch, int CertificateId, int FooterCertificateId);
        public Task<ConditioningOrderViewModel> getDatasource(int IdOP, int Id);
        public string GetDateFromNumberProduction(string NumberProduction);
        public Task<String> GetAnalizador(string IdLotePipa, string Param);
        public Task<String> GetTankClient(string IdLotePipa, string TourNumber, string TankPraxiar);
        public Task<RptChkList> GetRptChkListQuestionsOne(CheckListGeneralViewModel chkGeneral, ConditioningOrder conditioningOrder);
        public Task<Step4> BuildTableAnalisys(PipeFillingViewModel pipeFillingViewModel, List<ConditioningOrderViewModel> rptDataSource);
    }
}
