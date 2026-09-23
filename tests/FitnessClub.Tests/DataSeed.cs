namespace FitnessClub.Tests;

using FitnessClub.Domain;

/// <summary>
/// Класс для генерации тестовых данных 
/// </summary>
public static class DataSeed
{
    /// <summary>
    /// Возвращает сгенерированный набор данных для тестов
    /// </summary>
    public static (List<Specialization> Specs, List<Trainer> Trainers, List<Client> Clients, List<TrainingSession> Sessions) GetSeedData()
    {
        var specs = new List<Specialization>
        {
            new() { Id = Guid.NewGuid(), Name = "Силовой тренинг" },
            new() { Id = Guid.NewGuid(), Name = "Йога" },
            new() { Id = Guid.NewGuid(), Name = "Кроссфит" },
            new() { Id = Guid.NewGuid(), Name = "Кардио" },
            new() { Id = Guid.NewGuid(), Name = "Пилатес" },
            new() { Id = Guid.NewGuid(), Name = "Бокс" },
            new() { Id = Guid.NewGuid(), Name = "Тяжелая атлетика" },
            new() { Id = Guid.NewGuid(), Name = "ЛФК" },
            new() { Id = Guid.NewGuid(), Name = "Аэробика" },
            new() { Id = Guid.NewGuid(), Name = "Танцы" }
        };

        var trainers = Enumerable.Range(1, 10).Select(i => new Trainer
        {
            Id = Guid.NewGuid(),
            PassportNumber = $"4510 {100000 + i}",
            LastName = $"Тренеров_{i}",
            FirstName = $"Имя_{i}",
            Patronymic = $"Отчество_{i}",
            Gender = i % 2 == 0 ? Gender.Female : Gender.Male,
            DateOfBirth = new DateOnly(1985, 1, 1).AddYears(i),
            SpecializationId = specs[i - 1].Id,
            Specialization = specs[i - 1],
            WorkExperienceYears = i % 3 == 0 ? 2 : 3 + i 
        }).ToList();

        var clients = Enumerable.Range(1, 10).Select(i => new Client
        {
            Id = Guid.NewGuid(),
            PassportNumber = $"4000 {200000 + i}",
            LastName = $"Клиентов_{10 - i}", 
            FirstName = $"Имя_{i}",
            Patronymic = $"Отчество_{i}",
            Gender = i % 2 == 0 ? Gender.Male : Gender.Female,
            DateOfBirth = new DateOnly(1995, 5, 10),
            PhoneNumber = $"+7999000000{i}",
            MembershipStartDate = DateTime.UtcNow.AddMonths(-i),
            MembershipEndDate = i % 2 == 0 ? DateTime.UtcNow.AddDays(-i * 5) : DateTime.UtcNow.AddMonths(i)
        }).ToList();

        var now = DateTime.UtcNow;
        var sessions = new List<TrainingSession>();

        for (int i = 0; i < 15; i++)
        {
            sessions.Add(new TrainingSession
            {
                Id = Guid.NewGuid(),
                ClientId = clients[i % clients.Count].Id,
                Client = clients[i % clients.Count],
                TrainerId = trainers[i % 5].Id, 
                Trainer = trainers[i % 5],
                DateTime = now.AddDays(i - 5), 
                Duration = TimeSpan.FromHours(1),
                HallName = i % 2 == 0 ? "Зал №1" : "Зал №2",
                IsTrial = i % 4 == 0
            });
        }

        return (specs, trainers, clients, sessions);
    }
}