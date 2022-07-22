using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Manage.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace Manage.Model.Models
{
    [Table("hu_type_of_contract")]
    [Index(nameof(ContractId), Name = "IX_hu_type_of_contract_contract_id")]
    public partial class HuTypeOfContract : IEntityBase
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
        [Column("contract_id")]
        public int? ContractId { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        [Column("activeflg")]
        public string Activeflg { get; set; }
        [Column("number_of_month")]
        public int? NumberOfMonth { get; set; }

        [ForeignKey(nameof(ContractId))]
        [InverseProperty(nameof(HuContract.HuTypeOfContracts))]
        public virtual HuContract Contract { get; set; }
    }
}
