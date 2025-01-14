namespace LiberacionProductoWeb.Models.LayoutLeyendsViewModels
{
    public class LayoutLeyendsConditioningOrderSteps
    {
        private LayoutLeyendsConditioningOrderSteps(int value) { Value = value; }
        public int Value { get; set; }
        public static LayoutLeyendsConditioningOrderSteps StepZero { get { return new LayoutLeyendsConditioningOrderSteps(0); } }
        public static LayoutLeyendsConditioningOrderSteps StepOne { get { return new LayoutLeyendsConditioningOrderSteps(1); } }
        public static LayoutLeyendsConditioningOrderSteps StepTwo { get { return new LayoutLeyendsConditioningOrderSteps(2); } }
        public static LayoutLeyendsConditioningOrderSteps StepThree { get { return new LayoutLeyendsConditioningOrderSteps(3); } }
        public static LayoutLeyendsConditioningOrderSteps StepFor { get { return new LayoutLeyendsConditioningOrderSteps(4); } }
        public static LayoutLeyendsConditioningOrderSteps StepFive { get { return new LayoutLeyendsConditioningOrderSteps(5); } }
        public static LayoutLeyendsConditioningOrderSteps StepSix { get { return new LayoutLeyendsConditioningOrderSteps(6); } }
        public static LayoutLeyendsConditioningOrderSteps StepSeven { get { return new LayoutLeyendsConditioningOrderSteps(7); } }
        public static LayoutLeyendsConditioningOrderSteps StepEight { get { return new LayoutLeyendsConditioningOrderSteps(8); } }
    }
}
