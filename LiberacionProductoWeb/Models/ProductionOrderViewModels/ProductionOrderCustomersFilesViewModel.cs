namespace LiberacionProductoWeb.Models.ProductionOrderViewModels
{
    public class ProductionOrderCustomersFilesViewModel
    {
        public int Id { get; set; }
        public int ProductionOrderId { get; set; }
        public bool? State { get; set; }
        public string FileName { get; set; }
        public string FileNameOrigin { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }
    }
}
