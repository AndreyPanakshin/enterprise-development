using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Data;

/// <summary>
/// Набор данных для тестирования
/// </summary>
public static class DataSeed
{
    private static readonly DateOnly _currentDay = DateOnly.FromDateTime(DateTime.Today);

    /// <summary>
    /// Специализации тренеров
    /// </summary>
    public static List<Specialization> Specializations { get; } =
    [
        new Specialization { Id = 1, Name = "Силовой тренинг" },
        new Specialization { Id = 2, Name = "Йога" },
        new Specialization { Id = 3, Name = "Кроссфит" },
        new Specialization { Id = 4, Name = "Кардио" },
        new Specialization { Id = 5, Name = "Пилатес" },
        new Specialization { Id = 6, Name = "Бокс" },
        new Specialization { Id = 7, Name = "Тяжелая атлетика" },
        new Specialization { Id = 8, Name = "ЛФК" },
        new Specialization { Id = 9, Name = "Аэробика" },
        new Specialization { Id = 10, Name = "Танцы" }
    ];

    /// <summary>
    /// Список клиентов
    /// </summary>
    public static List<Member> Members { get; } =
    [
        new Member
        {
            Id = 1,
            PassportNumber = "4512 881023",
            LastName = "Непряхин",
            FirstName = "Александр",
            Patronymic = "Григорьевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2002, 3, 14),
            PhoneNumber = "+79161112233",
            MembershipStartDate = _currentDay.AddMonths(-10),
            MembershipEndDate = _currentDay.AddMonths(-1) 
        },
        new Member
        {
            Id = 2,
            PassportNumber = "4515 992104",
            LastName = "Савельева",
            FirstName = "Дарья",
            Patronymic = "Алексеевна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(2001, 7, 22),
            PhoneNumber = "+79162223344",
            MembershipStartDate = _currentDay.AddMonths(-6),
            MembershipEndDate = _currentDay.AddDays(-12) 
        },
        new Member
        {
            Id = 3,
            PassportNumber = "4518 330192",
            LastName = "Хисамов",
            FirstName = "Артур",
            Patronymic = null,
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2005, 9, 24),
            PhoneNumber = "+79163334455",
            MembershipStartDate = _currentDay.AddMonths(-3),
            MembershipEndDate = _currentDay.AddDays(-2) 
        },
        new Member
        {
            Id = 4,
            PassportNumber = "4520 774109",
            LastName = "Синячко",
            FirstName = "Баронесса",
            Patronymic = "Игоревна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1999, 1, 18),
            PhoneNumber = "+79164445566",
            MembershipStartDate = _currentDay.AddMonths(-5),
            MembershipEndDate = _currentDay.AddMonths(2)
        },
        new Member
        {
            Id = 5,
            PassportNumber = "4522 665011",
            LastName = "Дзюба",
            FirstName = "Артем",
            Patronymic = "Павлович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1990, 9, 30),
            PhoneNumber = "+79165556677",
            MembershipStartDate = _currentDay.AddMonths(-1),
            MembershipEndDate = _currentDay.AddMonths(5)
        },
        new Member
        {
            Id = 6,
            PassportNumber = "4523 112044",
            LastName = "Кузнецова",
            FirstName = "Наталья",
            Patronymic = "Сергеевна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1986, 4, 12),
            PhoneNumber = "+79166667788",
            MembershipStartDate = _currentDay.AddMonths(-2),
            MembershipEndDate = _currentDay.AddMonths(4)
        },
        new Member
        {
            Id = 7,
            PassportNumber = "4524 554321",
            LastName = "Кочевников",
            FirstName = "Евлампий",
            Patronymic = "Андреевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1992, 12, 25),
            PhoneNumber = "+79167778899",
            MembershipStartDate = _currentDay.AddMonths(-4),
            MembershipEndDate = _currentDay.AddMonths(1)
        },
        new Member
        {
            Id = 8,
            PassportNumber = "4525 998877",
            LastName = "Зайцев",
            FirstName = "Прохор",
            Patronymic = null,
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2000, 8, 8),
            PhoneNumber = "+79168889900",
            MembershipStartDate = _currentDay.AddMonths(-7),
            MembershipEndDate = _currentDay.AddMonths(3)
        },
        new Member
        {
            Id = 9,
            PassportNumber = "4526 332211",
            LastName = "Морозова",
            FirstName = "Фёкла",
            Patronymic = "Романовна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1998, 5, 15),
            PhoneNumber = "+79169990011",
            MembershipStartDate = _currentDay.AddMonths(-2),
            MembershipEndDate = _currentDay.AddMonths(6)
        },
        new Member
        {
            Id = 10,
            PassportNumber = "4527 445566",
            LastName = "Кобыльсков",
            FirstName = "Никита",
            Patronymic = "Сергеевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1995, 10, 2),
            PhoneNumber = "+79160001122",
            MembershipStartDate = _currentDay,
            MembershipEndDate = _currentDay.AddMonths(12)
        }
    ];

    /// <summary>
    /// Список тренеров
    /// </summary>
    public static List<Trainer> Trainers { get; } =
    [
        new Trainer
        {
            Id = 1,
            PassportNumber = "5011 102938",
            LastName = "Панакшин",
            FirstName = "Андрей",
            Patronymic = "Валерьевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1987, 2, 10),
            Specialization = Specializations[0],
            WorkExperienceYears = 6
        },
        new Trainer
        {
            Id = 2,
            PassportNumber = "5012 293847",
            LastName = "Архипова",
            FirstName = "Алина",
            Patronymic = "Игоревна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1993, 6, 14),
            Specialization = Specializations[1],
            WorkExperienceYears = 3
        },
        new Trainer
        {
            Id = 3,
            PassportNumber = "5013 384756",
            LastName = "Макеев",
            FirstName = "Сергей",
            Patronymic = "Петрович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1989, 11, 28),
            Specialization = Specializations[2],
            WorkExperienceYears = 8
        },
        new Trainer
        {
            Id = 4,
            PassportNumber = "5014 475665",
            LastName = "Слуцкий",
            FirstName = "Леонид",
            Patronymic = "Викторович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1975, 4, 3),
            Specialization = Specializations[3],
            WorkExperienceYears = 11
        },
        new Trainer
        {
            Id = 5,
            PassportNumber = "5015 566574",
            LastName = "Моуриньо",
            FirstName = "Жозе",
            Patronymic = "Глебович",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1991, 8, 19),
            Specialization = Specializations[4],
            WorkExperienceYears = 2
        },
        new Trainer
        {
            Id = 6,
            PassportNumber = "5016 657483",
            LastName = "Карпин",
            FirstName = "Валерий",
            Patronymic = "Георгиевич",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1986, 12, 1),
            Specialization = Specializations[5],
            WorkExperienceYears = 9
        },
        new Trainer
        {
            Id = 7,
            PassportNumber = "5017 748392",
            LastName = "Смирнова",
            FirstName = "Ксения",
            Patronymic = "Денисовна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(2002, 3, 27),
            Specialization = Specializations[6],
            WorkExperienceYears = 4
        },
        new Trainer
        {
            Id = 8,
            PassportNumber = "5018 839201",
            LastName = "Тарасов",
            FirstName = "Дмитрий",
            Patronymic = "Валентинович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1968, 7, 11),
            Specialization = Specializations[7],
            WorkExperienceYears = 7
        },
        new Trainer
        {
            Id = 9,
            PassportNumber = "5019 920110",
            LastName = "Ушаков",
            FirstName = "Федор",
            Patronymic = "Максимович",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(1984, 9, 9),
            Specialization = Specializations[8],
            WorkExperienceYears = 14
        },
        new Trainer
        {
            Id = 10,
            PassportNumber = "5020 011029",
            LastName = "Филиппова",
            FirstName = "Наталья",
            Patronymic = "Борисовна",
            Gender = Gender.Female,
            DateOfBirth = new DateOnly(1990, 5, 23),
            Specialization = Specializations[9],
            WorkExperienceYears = 5
        }
    ];

    /// <summary>
    /// Список персональных занятий
    /// </summary>
    public static List<TrainingSession> TrainingSession { get; } =
    [
        new() { Id = 1, Member = Members[0], Trainer = Trainers[0], DateTime = CreateDate(0, 5, 10), HallName = "Зал A", IsTrial = true },
        new() { Id = 2, Member = Members[1], Trainer = Trainers[0], DateTime = CreateDate(0, 7, 12), HallName = "Зал A", IsTrial = false },
        new() { Id = 3, Member = Members[2], Trainer = Trainers[0], DateTime = CreateDate(0, 9, 14), HallName = "Зал B", IsTrial = false },

        new() { Id = 4, Member = Members[3], Trainer = Trainers[2], DateTime = CreateDate(0, 10, 11), HallName = "Зал A", IsTrial = false },
        new() { Id = 5, Member = Members[4], Trainer = Trainers[2], DateTime = CreateDate(0, 11, 15), HallName = "Зал C", IsTrial = false },
        new() { Id = 6, Member = Members[5], Trainer = Trainers[2], DateTime = CreateDate(0, 12, 17), HallName = "Зал A", IsTrial = true },
        new() { Id = 7, Member = Members[6], Trainer = Trainers[2], DateTime = CreateDate(-1, 15, 12), HallName = "Зал B", IsTrial = false },

        new() { Id = 8, Member = Members[7], Trainer = Trainers[3], DateTime = CreateDate(0, 14, 9), HallName = "Зал B", IsTrial = false },
        new() { Id = 9, Member = Members[8], Trainer = Trainers[3], DateTime = CreateDate(0, 16, 13), HallName = "Зал A", IsTrial = false },
        new() { Id = 10, Member = Members[9], Trainer = Trainers[3], DateTime = CreateDate(0, 18, 16), HallName = "Зал C", IsTrial = true },

        new() { Id = 11, Member = Members[0], Trainer = Trainers[5], DateTime = CreateDate(0, 20, 10), HallName = "Зал C", IsTrial = false },
        new() { Id = 12, Member = Members[1], Trainer = Trainers[5], DateTime = CreateDate(0, 21, 14), HallName = "Зал A", IsTrial = false },

        new() { Id = 13, Member = Members[2], Trainer = Trainers[7], DateTime = CreateDate(0, 22, 11), HallName = "Зал B", IsTrial = false },
        new() { Id = 14, Member = Members[3], Trainer = Trainers[7], DateTime = CreateDate(1, 5, 15), HallName = "Зал A", IsTrial = false }

    ];

    private static DateTime CreateDate(int monthOffset, int day, int hour)
    {
        var targetMonth = DateTime.Today.AddMonths(monthOffset);
        return new DateTime(targetMonth.Year, targetMonth.Month, day, hour, 0, 0);
    }
}