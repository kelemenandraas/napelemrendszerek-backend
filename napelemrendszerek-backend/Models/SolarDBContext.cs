using System;
using dotenv.net.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using dotenv.net;
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class SolarDBContext : DbContext
    {
        public SolarDBContext()
        {
            DotEnv.Load();
        }

        public SolarDBContext(DbContextOptions<SolarDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compartment> Compartment { get; set; }
        public virtual DbSet<Part> Part { get; set; }
        public virtual DbSet<PartProjectConnection> PartProjectConnection { get; set; }
        public virtual DbSet<PartStates> PartStates { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectHistory> ProjectHistory { get; set; }
        public virtual DbSet<ProjectStates> ProjectStates { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var envVars = DotEnv.Read();
                optionsBuilder.UseSqlServer($"Server={envVars["DB_URL"]},{envVars["DB_PORT"]};Database={envVars["DB"]};User Id={envVars["DB_USER"]};Password={envVars["DB_PASS"]};");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compartment>(entity =>
            {
                entity.Property(e => e.CompartmentId).HasColumnName("compartmentID");

                entity.Property(e => e.CompartmentCell).HasColumnName("compartmentCell");

                entity.Property(e => e.CompartmentCulomn).HasColumnName("compartmentCulomn");

                entity.Property(e => e.CompartmentRow).HasColumnName("compartmentRow");

                entity.Property(e => e.StoredAmount).HasColumnName("storedAmount");

                entity.Property(e => e.StoredPartName)
                    .IsRequired()
                    .HasColumnName("storedPartName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.StoredPartNameNavigation)
                    .WithMany(p => p.Compartment)
                    .HasForeignKey(d => d.StoredPartName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compartme__store__59063A47");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => e.PartName)
                    .HasName("PK__Part__D5399EC82A7BF5B4");

                entity.Property(e => e.PartName)
                    .HasColumnName("partName")
                    .HasMaxLength(50);

                entity.Property(e => e.MaxNumberInBox).HasColumnName("maxNumberInBox");

                entity.Property(e => e.NumberAvailable).HasColumnName("numberAvailable");

                entity.Property(e => e.SellPrice).HasColumnName("sellPrice");
            });

            modelBuilder.Entity<PartProjectConnection>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.NumberReserved).HasColumnName("numberReserved");

                entity.Property(e => e.PartName)
                    .IsRequired()
                    .HasColumnName("partName")
                    .HasMaxLength(50);

                entity.Property(e => e.PartStateId).HasColumnName("partStateID");

                entity.Property(e => e.ProjectId).HasColumnName("projectID");

                entity.HasOne(d => d.PartNameNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.PartName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PartProje__partN__6383C8BA");

                entity.HasOne(d => d.PartState)
                    .WithMany()
                    .HasForeignKey(d => d.PartStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PartProje__partS__6477ECF3");

                entity.HasOne(d => d.Project)
                    .WithMany()
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PartProje__proje__628FA481");
            });

            modelBuilder.Entity<PartStates>(entity =>
            {
                entity.HasKey(e => e.PartStateId)
                    .HasName("PK__PartStat__73EFE6A2048528C5");

                entity.Property(e => e.PartStateId).HasColumnName("partStateID");

                entity.Property(e => e.PartStateName)
                    .IsRequired()
                    .HasColumnName("partStateName")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ProjectId).HasColumnName("projectID");

                entity.Property(e => e.ClosedDate)
                    .HasColumnName("closedDate")
                    .HasColumnType("date");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasMaxLength(20);

                entity.Property(e => e.CustomerAddress)
                    .IsRequired()
                    .HasColumnName("customerAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasColumnName("customerEmail")
                    .HasMaxLength(30);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customerName")
                    .HasMaxLength(30);

                entity.Property(e => e.CustomerPhone)
                    .IsRequired()
                    .HasColumnName("customerPhone")
                    .HasMaxLength(20);

                entity.Property(e => e.EstimatedTimeInDays).HasColumnName("estimatedTimeInDays");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnName("lastModifiedDate")
                    .HasColumnType("date");

                entity.Property(e => e.ProjectDescription)
                    .IsRequired()
                    .HasColumnName("projectDescription")
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectLocation)
                    .IsRequired()
                    .HasColumnName("projectLocation")
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectStateId).HasColumnName("projectStateID");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("date");

                entity.Property(e => e.WorkFee).HasColumnName("workFee");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project__created__5EBF139D");

                entity.HasOne(d => d.ProjectState)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.ProjectStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project__project__5DCAEF64");
            });

            modelBuilder.Entity<ProjectHistory>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DateOfChange)
                    .HasColumnName("dateOfChange")
                    .HasColumnType("date");

                entity.Property(e => e.NewProjectStateId).HasColumnName("newProjectStateID");

                entity.Property(e => e.OldProjectStateId).HasColumnName("oldProjectStateID");

                entity.Property(e => e.ProjectId).HasColumnName("projectID");

                entity.HasOne(d => d.NewProjectState)
                    .WithMany()
                    .HasForeignKey(d => d.NewProjectStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectHi__newPr__68487DD7");

                entity.HasOne(d => d.OldProjectState)
                    .WithMany()
                    .HasForeignKey(d => d.OldProjectStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectHi__oldPr__6754599E");

                entity.HasOne(d => d.Project)
                    .WithMany()
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectHi__proje__66603565");
            });

            modelBuilder.Entity<ProjectStates>(entity =>
            {
                entity.HasKey(e => e.ProjectStateId)
                    .HasName("PK__ProjectS__8C4589B5F44E8975");

                entity.Property(e => e.ProjectStateId).HasColumnName("projectStateID");

                entity.Property(e => e.ProjectStateName)
                    .IsRequired()
                    .HasColumnName("projectStateName")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__Roles__CD98460A14C22343");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("roleName")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__F3DBC5738C27FE8A");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(20);

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("userPassword")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__roleID__5441852A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
