using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class ProductionOrderCustomersFiles : Entity
    {
        public string ReviewedBy { get; set; }
        public DateTime ReviewedDate { get; set; }
        public int ProductionOrderId { get; set; }
        public bool? State { get; set; }
        public string FileName { get; set; }
        public string FileNameOrigin { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }
        public ProductionOrder ProductionOrder { get; set; }

        public static ProductionOrderCustomersFiles Create(
        int id,
        String reviewedBy,
        DateTime reviewedDate,
        int productionOrderId,
        bool? state,
        string fileName,
        string fileNameOrigin,
        string extension,
        string type
        )
        {
            var entity = new ProductionOrderCustomersFiles
            {
                Id = id,
                ReviewedBy = reviewedBy,
                ReviewedDate = reviewedDate,
                ProductionOrderId = productionOrderId,
                State = state,
                FileName = fileName,
                FileNameOrigin = fileNameOrigin,
                Extension = extension,
                Type = type
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as ProductionOrderCustomersFiles;
            var current = objectToCompare as ProductionOrderCustomersFiles;
            if (old.FileName != this.FileName)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Campo - archivo eliminado",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.FileName,
                    NewValue = FileName,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = current.ProductionOrder.DelegateUser,
                });
            }

            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }
    }
}
