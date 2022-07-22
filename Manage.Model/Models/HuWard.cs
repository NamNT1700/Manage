using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Manage.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace Manage.Model.Models
{
    [Table("hu_ward")]
    [Index(nameof(DistricId), Name = "IX_hu_ward_distric_id")]
    public partial class HuWard : IEntityBase
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
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        [Column("distric_id")]
        public int? DistricId { get; set; }
        [Column("activeflg")]
        public string Activeflg { get; set; }

        [ForeignKey(nameof(DistricId))]
        [InverseProperty(nameof(HuDistrict.HuWards))]
        public virtual HuDistrict Distric { get; set; }
    }
}
