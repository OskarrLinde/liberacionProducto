using LiberacionProductoWeb.Models.DataBaseModels.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class LeyendsTextHistory : Entity
    {

        [Column(TypeName = "varchar(200)")]
        public string Title { get; set; }
        [Column(TypeName = "varchar(max)")]
        public String Text { get; set; }
        public int LeyendsTextHistoryGroupId { get; set; }
        public LeyendsTextHistoryGroup LeyendsTextHistoryGroup { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }
    public class LeyendsTextHistoryGroup : Entity
    {
        public Guid GroupId { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int Step { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ProductId { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }
}
