﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Manage.Model.Models
{
    [Table("hu_org_title")]
    public partial class HuOrgTitle
    {
        public HuOrgTitle()
        {
            HuEmployees = new HashSet<HuEmployee>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("org_id")]
        public int? OrgId { get; set; }
        [Column("title_id")]
        public int? TitleId { get; set; }
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

        [InverseProperty(nameof(HuEmployee.Org))]
        public virtual ICollection<HuEmployee> HuEmployees { get; set; }
    }
}