using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BatchLibrary.Models;

public partial class EYBatchDBContext : DbContext
{
    public EYBatchDBContext()
    {
    }

    public EYBatchDBContext(DbContextOptions<EYBatchDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=EYBatchDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.BatchCode).HasName("PK__Batch__B22ADA8FC52304FD");

            entity.ToTable("Batch");

            entity.Property(e => e.BatchCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CourseCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.CourseCodeNavigation).WithMany(p => p.Batches)
                .HasForeignKey(d => d.CourseCode)
                .HasConstraintName("FK__Batch__CourseCod__38996AB5");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseCode).HasName("PK__Course__FC00E001E0F7CCF8");

            entity.ToTable("Course");

            entity.Property(e => e.CourseCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
