using LiberacionProductoWeb.Models.DataBaseModels.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class CheckListText: Entity, ICloneable
    {
        [Column(TypeName = "varchar(200)")]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "varchar(200)")]
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;

        [Column(TypeName = "varchar(max)")]
        public String Text { get; set; }
        public int Step { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }
        public CheckListText(string createdBy, DateTime createdDate, string modifyBy, DateTime modifyDate, string text, int step, string type)
        {
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.ModifyBy = modifyBy;
            this.ModifyDate = modifyDate;
            this.Text = text;
            this.Step = step;
            this.Type = type;
        }
        public CheckListText()
        { }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as CheckListText;
            var current = objectToCompare as CheckListText;
            if (old.ModifyBy != current.ModifyBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "LayoutText",
                    Date = DateTime.Now,
                    Detail = "Cambios a los textos de checklist - modificado por",
                    Funcionality = "Cambios a los textos de checklist",
                    PreviousValue = old.ModifyBy,
                    NewValue = current.ModifyBy,
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
            return new CheckListText(this.CreatedBy, this.CreatedDate, this.ModifyBy, this.ModifyDate, this.Text, this.Step, this.Type);   
        }
    }
}
