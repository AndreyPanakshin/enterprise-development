using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Repositories.Ef;

/// <summary>
/// Контекст базы данных фитнес-клуба (MySQL)
/// </summary>
public class FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options) : DbContext(options)
{
    /// <summary>Клиенты клуба</summary>
    public DbSet<Member> Members => Set<Member>();

    /// <summary>Тренеры клуба</summary>
    public DbSet<Trainer> Trainers => Set<Trainer>();

    /// <summary>Специализации тренеров</summary>
    public DbSet<Specialization> Specializations => Set<Specialization>();

    /// <summary>Персональные тренировки</summary>
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

    /// <summary>Настройка модели данных</summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(m => m.Id).HasName("pk_members");
            entity.Property(m => m.Id).ValueGeneratedOnAdd();
            entity.Property(m => m.PassportNumber).HasMaxLength(20);
            entity.Property(m => m.LastName).HasMaxLength(50);
            entity.Property(m => m.FirstName).HasMaxLength(50);
            entity.Property(m => m.Patronymic).HasMaxLength(50);
            entity.Property(m => m.PhoneNumber).HasMaxLength(30);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(s => s.Id).HasName("pk_specializations");
            entity.Property(s => s.Id).ValueGeneratedOnAdd();
            entity.Property(s => s.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(t => t.Id).HasName("pk_trainers");
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.PassportNumber).HasMaxLength(20);
            entity.Property(t => t.LastName).HasMaxLength(50);
            entity.Property(t => t.FirstName).HasMaxLength(50);
            entity.Property(t => t.Patronymic).HasMaxLength(50);
            entity.Property<int>("SpecializationId").HasColumnName("specialization_id");
            entity.HasOne(t => t.Specialization)
                .WithMany()
                .HasForeignKey("SpecializationId")
                .HasConstraintName("fk_trainers_specializations")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex("SpecializationId").HasDatabaseName("ix_trainers_specialization_id");
        });

        modelBuilder.Entity<TrainingSession>(entity =>
        {
            entity.HasKey(s => s.Id).HasName("pk_training_sessions");
            entity.Property(s => s.Id).ValueGeneratedOnAdd();
            entity.Property(s => s.HallName).HasMaxLength(100);
            entity.Property<int>("MemberId").HasColumnName("member_id");
            entity.Property<int>("TrainerId").HasColumnName("trainer_id");
            entity.HasOne(s => s.Member)
                .WithMany()
                .HasForeignKey("MemberId")
                .HasConstraintName("fk_training_sessions_members")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(s => s.Trainer)
                .WithMany()
                .HasForeignKey("TrainerId")
                .HasConstraintName("fk_training_sessions_trainers")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex("MemberId").HasDatabaseName("ix_training_sessions_member_id");
            entity.HasIndex("TrainerId").HasDatabaseName("ix_training_sessions_trainer_id");
        });
    }
}