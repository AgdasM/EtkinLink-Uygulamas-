using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EtkinLink;

public partial class EtkinLinkContext : DbContext
{
    public EtkinLinkContext()
    {
    }

    public EtkinLinkContext(DbContextOptions<EtkinLinkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityTbl> ActivityTbls { get; set; }

    public virtual DbSet<AuthorizationTbl> AuthorizationTbls { get; set; }

    public virtual DbSet<CategoryTbl> CategoryTbls { get; set; }

    public virtual DbSet<CityTbl> CityTbls { get; set; }

    public virtual DbSet<DeleteMessageTbl> DeleteMessageTbls { get; set; }

    public virtual DbSet<ParticipantsTbl> ParticipantsTbls { get; set; }

    public virtual DbSet<UserTbl> UserTbls { get; set; }

    public virtual DbSet<DevTBL> DevTBLs  { get; set; }

    public virtual DbSet<EtkinlikGet_tbl> EtkinlikGet_Tbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=EtkinLink;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityTbl>(entity =>
        {
            entity.HasKey(e => e.ActivityId);

            entity.ToTable("Activity_tbl");

            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.ActivityDate).HasColumnType("date");
            entity.Property(e => e.ActivityDeadLine).HasColumnType("date");
            entity.Property(e => e.ActivityName).HasMaxLength(75);
            entity.Property(e => e.Address).HasMaxLength(75);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.Explanation).HasMaxLength(150);
            entity.Property(e => e.TicketPrice).HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.ActivityTbls)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_tbl_Category_tbl");

            entity.HasOne(d => d.City).WithMany(p => p.ActivityTbls)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_tbl_City_tbl");
        });

        modelBuilder.Entity<AuthorizationTbl>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Authorization_tbl");

            entity.Property(e => e.Gmail).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });
        modelBuilder.Entity<DevTBL>(entity =>
        {
            entity.HasKey(e => e.DevID);
            entity.ToTable("DevTbl");
            entity.Property(e => e.DevID).HasColumnName("DevID");
            entity.Property(e => e.DevMail).HasMaxLength(50);
            entity.Property(e => e.DevPassword).HasMaxLength(50);
            entity.Property(e => e.DevSite).HasMaxLength(100);
        });

        modelBuilder.Entity<CategoryTbl>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("Category_tbl");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(20);
        });

        modelBuilder.Entity<CityTbl>(entity =>
        {
            entity.HasKey(e => e.CityId);

            entity.ToTable("City_tbl");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.City).HasMaxLength(25);
        });
        modelBuilder.Entity<EtkinlikGet_tbl>(entity =>
        {
            entity.HasKey(e => e.ID);

            entity.ToTable("EtkinlikGet_tbl");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.EtkinlikAdı).HasMaxLength(100);
            entity.Property(e => e.EtkinlikFiyatı).HasColumnType("money");
        });

        modelBuilder.Entity<DeleteMessageTbl>(entity =>
        {
            entity.ToTable("DeleteMessage_tbl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.Message).HasMaxLength(150);

            entity.HasOne(d => d.Activity).WithMany(p => p.DeleteMessageTbls)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_DeleteMessage_tbl_Activity_tbl");
        });

        modelBuilder.Entity<ParticipantsTbl>(entity =>
        {
            entity.ToTable("Participants_tbl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Activity).WithMany(p => p.ParticipantsTbls)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_Participants_tbl_Activity_tbl");

            entity.HasOne(d => d.User).WithMany(p => p.ParticipantsTbls)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Participants_tbl_User_TBL1");
        });

        modelBuilder.Entity<UserTbl>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("User_TBL");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserSurname).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
