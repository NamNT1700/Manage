using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Manage.Model.Base;

namespace Manage.Model.Models
{
    [Table("Se_User")]
    public partial class SeUser : IEntityBase
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
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ActiveFlg { get; set; }
    }
}
