using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Manage.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace Manage.Model.Models
{
    [Table("hu_salary_records")]
    [Index(nameof(ContractId), Name = "IX_hu_salary_records_contract_id")]
    [Index(nameof(ContractAllowanceId), Name = "IX_hu_salary_records_contract_allowance_id")]
    [Index(nameof(ContractWelfareId), Name = "IX_hu_salary_records_contract_welfare_id")]
    [Index(nameof(EmployeeId), Name = "IX_hu_salary_records_employee_id")]
    public partial class HuSalaryRecord : IEntityBase
    {
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
        public int? EmployeeId { get; set; }
        [Column("contract_id")]
        public int? ContractId { get; set; }
        [Column("contract_allowance_id")]
        public int? ContractAllowanceId { get; set; }
        [Column("contract_welfare_id")]
        public int? ContractWelfareId { get; set; }
        [Column("money")]
        public double? Money { get; set; }

        [ForeignKey(nameof(ContractId))]
        [InverseProperty(nameof(HuContract.HuSalaryRecords))]
        public virtual HuContract Contract { get; set; }
        [ForeignKey(nameof(ContractAllowanceId))]
        [InverseProperty(nameof(HuContractAllowance.HuSalaryRecords))]
        public virtual HuContractAllowance ContractAllowance { get; set; }
        [ForeignKey(nameof(ContractWelfareId))]
        [InverseProperty(nameof(HuWelfare.HuSalaryRecords))]
        public virtual HuWelfare ContractWelfare { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(HuEmployee.HuSalaryRecords))]
        public virtual HuEmployee Employee { get; set; }
    }
}
