﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Diplom.DOMAIN.NewModels
{
    public partial class DiplomDBContext : DbContext
    {
        public DiplomDBContext()
        {
        }

        public DiplomDBContext(DbContextOptions<DiplomDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audios> Audios { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<KindOfActivities> KindOfActivities { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Texts> Texts { get; set; }
        public virtual DbSet<UserActions> UserActions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Videos> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audios>(entity =>
            {
                entity.HasIndex(e => e.PersonId, "IX_Audios_PersonId");

                entity.Property(e => e.FileExtension).HasMaxLength(10);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Audios)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.HasIndex(e => e.PersonId, "IX_Images_PersonId");

                entity.Property(e => e.FileExtension).HasMaxLength(10);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<KindOfActivities>(entity =>
            {
                entity.ToTable("Kind of activities");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.HasIndex(e => e.KindOfActivityId, "IX_Persons_KindOfActivityId");

                entity.HasIndex(e => e.LocationId, "IX_Persons_LocationId")
                    .IsUnique();

                entity.Property(e => e.BornDate).HasColumnType("date");

                entity.Property(e => e.DeathDate).HasColumnType("date");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Patronymic).IsRequired();

                entity.Property(e => e.Surname).IsRequired();

                entity.HasOne(d => d.KindOfActivity)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.KindOfActivityId);

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.Persons)
                    .HasForeignKey<Persons>(d => d.LocationId);
            });

            modelBuilder.Entity<Texts>(entity =>
            {
                entity.HasIndex(e => e.PersonId, "IX_Texts_PersonId");

                entity.Property(e => e.FileExtension).HasMaxLength(10);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Texts)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<UserActions>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.UserActions)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_UserActions_To_Persons");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserActions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserActions_To_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Videos>(entity =>
            {
                entity.HasIndex(e => e.PersonId, "IX_Videos_PersonId");

                entity.Property(e => e.FileExtension).HasMaxLength(10);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.PersonId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}