using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Models;

public partial class EYCourseDBContext : DbContext
{
    public EYCourseDBContext()
    {
    }

    public EYCourseDBContext(DbContextOptions<EYCourseDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=EYCourseDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseCode).HasName("PK__Course__FC00E001AFD3DB0E");

            entity.ToTable("Course");

            entity.Property(e => e.CourseCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CourseFee).HasColumnType("money");
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
