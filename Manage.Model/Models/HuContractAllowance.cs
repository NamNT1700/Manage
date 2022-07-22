using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Manage.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace Manage.Model.Models
{
    [Table("hu_Contract_allowance")]
    [Index(nameof(AllowanceId), Name = "IX_hu_Contract_allowance_allowance_id")]
    [Index(nameof(ContractId), Name = "IX_hu_Contract_allowance_contract_id")]
    public partial class HuContractAllowance : IEntityBase
    {
        public HuContractAllowance()
        {
            HuSalaryRecords = new HashSet<HuSalaryRecord>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("code")]
        [StringLength(255)]
        public string Code { get; set; }
        [Column("created_by")]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column("created_time", TypeName = "datetime")]
        public DateTime? CreatedTime { get; set; }
        [Column("last_updated_by")]
        [StringLength(50)]
        public string LastUpdatedBy { get; set; }
        [Column("last_update_time", TypeName = "datetime")]
        public DateTime? LastUpdateTime { get; set; }
        [Column("contract_id")]
        public int? ContractId { get; set; }
        [Column("allowance_id")]
        public int? AllowanceId { get; set; }
        [Column("money")]
        public double? Money { get; set; }
        [Column("activeflg")]
        public string Activeflg { get; set; }

        [ForeignKey(nameof(AllowanceId))]
        [InverseProperty(nameof(HuAllowance.HuContractAllowances))]
        public virtual HuAllowance Allowance { get; set; }
        [ForeignKey(nameof(ContractId))]
        [InverseProperty(nameof(HuContract.HuContractAllowances))]
        public virtual HuContract Contract { get; set; }
        [InverseProperty(nameof(HuSalaryRecord.ContractAllowance))]
        public virtual ICollection<HuSalaryRecord> HuSalaryRecords { get; set; }
    }
}
