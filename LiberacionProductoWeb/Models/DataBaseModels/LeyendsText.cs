using LiberacionProductoWeb.Models.DataBaseModels.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class LeyendsText : Entity, ICloneable
    {
        [Column(TypeName = "varchar(200)")]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "varchar(200)")]
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        [Column(TypeName = "varchar(200)")]
        public string Title { get; set; }
        [Column(TypeName = "varchar(max)")]
        public String Text { get; set; }
        public int Step { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ProductId { get; set; }
        public bool Active { get; set; }
        public LeyendsText(int id, string createdBy, DateTime createdDate, string modifyBy, DateTime modifyDate,
            string title, String text, int step, string type, string productId)
        {
            this.Id = id;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.ModifyBy = modifyBy;
            this.ModifyDate = modifyDate;
            this.Title = title;
            this.Text = text;
            this.Step = step;
            this.Type = type;
            this.ProductId = productId;
        }
        public LeyendsText()
        { }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as LeyendsText;
            var current = objectToCompare as LeyendsText;

            var source = string.Empty;
            if (current.Type.Equals("OA"))
                source = "Orden de acodicionamiento";
            else if (current.Type.Equals("OP"))
                source = "Orden de producción";
            else
                source = "Checklist";
            if (old.Text != current.Text)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "LayoutText",
                    Date = DateTime.Now,
                    Detail = $"Cambios a los textos de {source} - Texto",
                    Funcionality = $"Cambios a los textos de {source}",
                    PreviousValue = old.Text,
                    NewValue = current.Text,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = current.ModifyBy,
                }); ;
            }
            if (old.Title != current.Title)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "LayoutText",
                    Date = DateTime.Now,
                    Detail = $"Cambios a los textos de {source} - Titulo",
                    Funcionality = $"Cambios a los textos de {source}",
                    PreviousValue = old.Title,
                    NewValue = current.Title,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = current.ModifyBy,
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }
        public object Clone()
        {
            return new LeyendsText(this.Id, this.CreatedBy, this.CreatedDate, this.ModifyBy, this.ModifyDate,
                this.Title, this.Text, this.Step, this.Type, this.ProductId);
        }
    }
}
