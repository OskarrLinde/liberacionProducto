namespace LiberacionProductoWeb.Models.LayoutLeyendsViewModels
{
    public class LayoutLeyendsType
    {
        private LayoutLeyendsType(string value) { Value = value; }
        public string Value { get; set; }
        public static LayoutLeyendsType ProductionOrder { get { return new LayoutLeyendsType("OP"); } }
        public static LayoutLeyendsType ConditioningOrder { get { return new LayoutLeyendsType("OA"); } }
        public static LayoutLeyendsType CheckListIV { get { return new LayoutLeyendsType("CheckListIV"); } }
        public static LayoutLeyendsType CheckListFP { get { return new LayoutLeyendsType("CheckListFP"); } }
    }
}
