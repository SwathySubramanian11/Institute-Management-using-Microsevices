using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentLibrary.Models;

public partial class EYStudentDBContext : DbContext
{
    public EYStudentDBContext()
    {
    }

    public EYStudentDBContext(DbContextOptions<EYStudentDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=EYStudentDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.BatchCode).HasName("PK__Batch__B22ADA8FEB6A48C3");

            entity.ToTable("Batch");

            entity.Property(e => e.BatchCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.RollNo).HasName("PK__Student__7886D5A01BBFB62D");

            entity.ToTable("Student");

            entity.Property(e => e.RollNo)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BatchCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StudentAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.BatchCodeNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.BatchCode)
                .HasConstraintName("FK__Student__BatchCo__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
