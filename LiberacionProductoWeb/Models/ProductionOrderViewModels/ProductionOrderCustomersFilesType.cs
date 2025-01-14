namespace LiberacionProductoWeb.Models.ProductionOrderViewModels
{
    public class ProductionOrderCustomersFilesType
    {
        private ProductionOrderCustomersFilesType(string value) { Value = value; }
        public string Value { get; set; }
        public static ProductionOrderCustomersFilesType TableFive { get { return new ProductionOrderCustomersFilesType("TableFive"); } }
        public static ProductionOrderCustomersFilesType TableSix { get { return new ProductionOrderCustomersFilesType("TableSix"); } }
        public static ProductionOrderCustomersFilesType TableSeven { get { return new ProductionOrderCustomersFilesType("TableSeven"); } }

    }
}
