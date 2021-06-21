using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReadBook.Models
{
    public partial class ReadBookContext : DbContext
    {
        public ReadBookContext()
        {
        }

        public ReadBookContext(DbContextOptions<ReadBookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<BookTypes> BookTypes { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<SpecialTags> SpecialTags { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Works> Works { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DELL-LETAI;Database=ReadBook;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>(entity =>
            {
                entity.Property(e => e.Dob).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BookTypes>(entity =>
            {
                entity.Property(e => e.BookType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Substance).IsRequired();

                entity.HasOne(d => d.BookType)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_BookTypes");

                entity.HasOne(d => d.SpecialTag)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.SpecialTagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_SpecialTags");
            });

            modelBuilder.Entity<SpecialTags>(entity =>
            {
                entity.Property(e => e.SpecialTag)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Works>(entity =>
            {
                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_Authors");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_Books");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
