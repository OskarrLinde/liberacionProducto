using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;

namespace LiberacionProductoWeb.Models.LayoutLeyendsViewModels
{
    public class LayoutLeyendsProductionOrderSteps
    {
        private LayoutLeyendsProductionOrderSteps(int value) { Value = value; }
        public int Value { get; set; }
        public static LayoutLeyendsProductionOrderSteps StepOne { get { return new LayoutLeyendsProductionOrderSteps(1); } }
        public static LayoutLeyendsProductionOrderSteps StepTwo { get { return new LayoutLeyendsProductionOrderSteps(2); } }
        public static LayoutLeyendsProductionOrderSteps StepThree { get { return new LayoutLeyendsProductionOrderSteps(3); } }
        public static LayoutLeyendsProductionOrderSteps StepFor { get { return new LayoutLeyendsProductionOrderSteps(4); } }
        public static LayoutLeyendsProductionOrderSteps StepFive { get { return new LayoutLeyendsProductionOrderSteps(5); } }
        public static LayoutLeyendsProductionOrderSteps StepSix { get { return new LayoutLeyendsProductionOrderSteps(6); } }
        public static LayoutLeyendsProductionOrderSteps StepSeven { get { return new LayoutLeyendsProductionOrderSteps(7); } }
    }
}
