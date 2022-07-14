﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Manage.Model.Models
{
    [Table("hu_bank")]
    public partial class HuBank
    {
        public HuBank()
        {
            HuBankBranches = new HashSet<HuBankBranch>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("code")]
        [StringLength(255)]
        public string Code { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        [Column("activeflg")]
        public bool? Activeflg { get; set; }
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

        [InverseProperty(nameof(HuBankBranch.Bank))]
        public virtual ICollection<HuBankBranch> HuBankBranches { get; set; }
    }
}
