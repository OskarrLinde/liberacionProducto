using Newtonsoft.Json;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.CertCatalogosViewModels
{
    public class RetrieveMultipleResponse
    {
        public List<Attribute> Attributes { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class Value
    {
        [JsonProperty("Value")]
        public string value { get; set; }
        public List<string> Values { get; set; }
    }

    public class Attribute
    {
        public string Key { get; set; }
        public Value Value { get; set; }
    }
}
