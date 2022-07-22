using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Manage.Model.Models;

#nullable disable

namespace Manage.Model.Context
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HuAllowance> HuAllowances { get; set; }
        public virtual DbSet<HuBank> HuBanks { get; set; }
        public virtual DbSet<HuBankBranch> HuBankBranches { get; set; }
        public virtual DbSet<HuContract> HuContracts { get; set; }
        public virtual DbSet<HuContractAllowance> HuContractAllowances { get; set; }
        public virtual DbSet<HuContractualBenefit> HuContractualBenefits { get; set; }
        public virtual DbSet<HuDistrict> HuDistricts { get; set; }
        public virtual DbSet<HuEmployee> HuEmployees { get; set; }
        public virtual DbSet<HuEmployeeCv> HuEmployeeCvs { get; set; }
        public virtual DbSet<HuEmployeeEducation> HuEmployeeEducations { get; set; }
        public virtual DbSet<HuFamily> HuFamilies { get; set; }
        public virtual DbSet<HuHospital> HuHospitals { get; set; }
        public virtual DbSet<HuNation> HuNations { get; set; }
        public virtual DbSet<HuOrgTitle> HuOrgTitles { get; set; }
        public virtual DbSet<HuOrganization> HuOrganizations { get; set; }
        public virtual DbSet<HuProvince> HuProvinces { get; set; }
        public virtual DbSet<HuSalaryRecord> HuSalaryRecords { get; set; }
        public virtual DbSet<HuSchool> HuShools { get; set; }
        public virtual DbSet<HuTitle> HuTitles { get; set; }
        public virtual DbSet<HuTypeOfContract> HuTypeOfContracts { get; set; }
        public virtual DbSet<HuWard> HuWards { get; set; }
        public virtual DbSet<HuWelfare> HuWelfaces { get; set; }
        public virtual DbSet<OtherList> OtherLists { get; set; }
        public virtual DbSet<OtherListType> OtherListTypes { get; set; }
        public virtual DbSet<SeUser> SeUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-HJ37BIM;Initial Catalog=Employee_manager_VMO_lan2;User ID=sa;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<HuAllowance>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<HuBank>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<HuContract>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<HuEmployee>(entity =>
            {
                entity.HasOne(d => d.OrgNavigation)
                    .WithMany(p => p.HuEmployees)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_hu_employee_hu_organization");
            });

            modelBuilder.Entity<HuEmployeeCv>(entity =>
            {
                entity.HasOne(d => d.BankBrank)
                    .WithMany(p => p.HuEmployeeCvs)
                    .HasForeignKey(d => d.BankBrankId)
                    .HasConstraintName("FK_hu_employee_cv_hu_bank_branch");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.HuEmployeeCvs)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("FK_hu_employee_cv_hu_bank");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.HuEmployeeCvs)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_hu_employee_cv_hu_employee");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.HuEmployeeCvs)
                    .HasForeignKey(d => d.HospitalId)
                    .HasConstraintName("FK_hu_employee_cv_hu_hospital");

                entity.HasOne(d => d.Nation)
                    .WithMany(p => p.HuEmployeeCvs)
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK_hu_employee_cv_hu_nation");
            });

            modelBuilder.Entity<HuHospital>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<HuNation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<HuOrgTitle>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<HuSchool>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.HuShools)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_hu_shools_hu_employee");
            });

            modelBuilder.Entity<HuTitle>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<OtherList>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
