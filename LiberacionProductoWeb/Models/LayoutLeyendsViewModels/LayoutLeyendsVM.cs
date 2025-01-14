using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.LayoutLeyendsViewModels
{
    public class LayoutLeyendsVM : SechToolDistributionBatchVM
    {
        public List<LayoutLeyendsProductionOrderVM> LayoutLeyendsProductionOrders { get; set; }
        public List<LayoutLeyendsConditioningOrderVM> LayoutLeyendsConditioningOrders { get; set; }
        public List<LayoutLeyendsCheckListVM> LayoutLeyendsCheckLists { get; set; }
        # region helper controls
        public IEnumerable<SelectListItem> SelectedProductFilter { get; set; }
        public string StepZero { get; set; }
        public string StepOne { get; set; }
        public string StepTwo { get; set; }
        public string StepThree { get; set; }
        public string StepFor { get; set; }
        public string StepFive { get; set; }
        public string StepSix { get; set; }
        public string StepSeven { get; set; }
        public string StepEight { get; set; }
        public string StepNine { get; set; }
        public string StepTen { get; set; }
        public string StepEleven { get; set; }
        public string ProductId { get; set; }

        public string TitleZero { get; set; }
        public string TitleOne { get; set; }
        public string TitleTwo { get; set; }
        public string TitleThree { get; set; }
        public string TitleFor { get; set; }
        public string TitleFive { get; set; }
        public string TitleSix { get; set; }
        public string TitleSeven { get; set; }
        public string TitleEight { get; set; }

        public string LeyendOne { get; set; }
        public string LeyendTwo { get; set; }
        public List<String> SelectedProduct { get; set; }
        public List<SelectListItem> ListProducts { get; set; }
        #endregion

    }
    public class LayoutLeyendsProductionOrderVM
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public String Text { get; set; }
        public int Step { get; set; }
        public string Type { get; set; }
        public string ProductId { get; set; }
        public bool Active { get; set; }

    }

    public class LayoutLeyendsConditioningOrderVM
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public String Text { get; set; }
        public int Step { get; set; }
        public string Type { get; set; }
        public string ProductId { get; set; }
        public bool Active { get; set; }

    }

    public class LayoutLeyendsCheckListVM
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public String Text { get; set; }
        public int Step { get; set; }
        public string Type { get; set; }
        public string ProductId { get; set; }
        public bool Active { get; set; }
    }
}
