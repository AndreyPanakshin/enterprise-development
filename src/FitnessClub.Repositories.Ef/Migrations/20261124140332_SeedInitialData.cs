using FitnessClub.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessClub.Repositories.Ef.Migrations;

/// <summary>
/// Первичное заполнение базы данных тестовыми данными
/// </summary>
public partial class SeedInitialData : Migration
{
    /// <summary>Вставляет начальные данные</summary>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "specializations",
            columns: new[] { "id", "name" },
            values: new object[,]
            {
                { 1, "Силовой тренинг" },
                { 2, "Йога" },
                { 3, "Кроссфит" },
                { 4, "Кардио" },
                { 5, "Пилатес" },
                { 6, "Бокс" },
                { 7, "Тяжелая атлетика" },
                { 8, "ЛФК" },
                { 9, "Аэробика" },
                { 10, "Танцы" }
            });

        migrationBuilder.InsertData(
            table: "members",
            columns: new[] { "id", "passport_number", "last_name", "first_name", "patronymic", "gender", "date_of_birth", "phone_number", "membership_start_date", "membership_end_date" },
            values: new object[,]
            {
                { 1, "4512 881023", "Непряхин", "Александр", "Григорьевич", (int)Gender.Male, new DateOnly(2002, 3, 14), "+79161112233", new DateOnly(2026, 1, 21), new DateOnly(2027, 1, 21) },
                { 2, "4515 992104", "Савельева", "Дарья", "Алексеевна", (int)Gender.Female, new DateOnly(2001, 7, 22), "+79162223344", new DateOnly(2026, 5, 21), new DateOnly(2026, 11, 9) },
                { 3, "4518 330192", "Хисамов", "Артур", null, (int)Gender.Male, new DateOnly(2005, 9, 24), "+79163334455", new DateOnly(2026, 8, 21), new DateOnly(2026, 12, 21) },
                { 4, "4520 774109", "Синячко", "Баронесса", "Игоревна", (int)Gender.Female, new DateOnly(1999, 1, 18), "+79164445566", new DateOnly(2026, 6, 21), new DateOnly(2027, 1, 21) },
                { 5, "4522 665011", "Дзюба", "Артем", "Павлович", (int)Gender.Male, new DateOnly(1990, 9, 30), "+79165556677", new DateOnly(2026, 10, 21), new DateOnly(2026, 11, 18) },
                { 6, "4523 112044", "Кузнецова", "Наталья", "Сергеевна", (int)Gender.Female, new DateOnly(1986, 4, 12), "+79166667788", new DateOnly(2026, 9, 21), new DateOnly(2027, 3, 21) },
                { 7, "4524 554321", "Кочевников", "Евлампий", "Андреевич", (int)Gender.Male, new DateOnly(1992, 12, 25), "+79167778899", new DateOnly(2026, 7, 21), new DateOnly(2026, 12, 21) },
                { 8, "4525 998877", "Зайцев", "Прохор", null, (int)Gender.Male, new DateOnly(2000, 8, 8), "+79168889900", new DateOnly(2026, 4, 21), new DateOnly(2026, 11, 1) },
                { 9, "4526 332211", "Морозова", "Фёкла", "Романовна", (int)Gender.Female, new DateOnly(1998, 5, 15), "+79169990011", new DateOnly(2026, 9, 21), new DateOnly(2027, 5, 21) },
                { 10, "4527 445566", "Кобыльсков", "Никита", "Сергеевич", (int)Gender.Male, new DateOnly(1995, 10, 2), "+79160001122", new DateOnly(2026, 11, 21), new DateOnly(2027, 11, 21) }
            });

        migrationBuilder.InsertData(
            table: "trainers",
            columns: new[] { "id", "passport_number", "last_name", "first_name", "patronymic", "gender", "date_of_birth", "specialization_id", "work_experience_years" },
            values: new object[,]
            {
                { 1, "5011 102938", "Панакшин", "Андрей", "Валерьевич", (int)Gender.Male, new DateOnly(1987, 2, 10), 1, 6 },
                { 2, "5012 293847", "Архипова", "Алина", "Игоревна", (int)Gender.Female, new DateOnly(1993, 6, 14), 2, 3 },
                { 3, "5013 384756", "Макеев", "Сергей", "Петрович", (int)Gender.Male, new DateOnly(1989, 11, 28), 3, 8 },
                { 4, "5014 475665", "Слуцкий", "Леонид", "Викторович", (int)Gender.Male, new DateOnly(1975, 4, 3), 4, 11 },
                { 5, "5015 566574", "Моуриньо", "Жозе", "Глебович", (int)Gender.Male, new DateOnly(1991, 8, 19), 5, 2 },
                { 6, "5016 657483", "Карпин", "Валерий", "Георгиевич", (int)Gender.Male, new DateOnly(1986, 12, 1), 6, 9 },
                { 7, "5017 748392", "Смирнова", "Ксения", "Денисовна", (int)Gender.Female, new DateOnly(2002, 3, 27), 7, 4 },
                { 8, "5018 839201", "Тарасов", "Дмитрий", "Валентинович", (int)Gender.Male, new DateOnly(1968, 7, 11), 8, 7 },
                { 9, "5019 920110", "Ушаков", "Федор", "Максимович", (int)Gender.Male, new DateOnly(1984, 9, 9), 9, 14 },
                { 10, "5020 011029", "Филиппова", "Наталья", "Борисовна", (int)Gender.Female, new DateOnly(1990, 5, 23), 10, 5 }
            });

        migrationBuilder.InsertData(
            table: "training_sessions",
            columns: new[] { "id", "member_id", "trainer_id", "date_time", "duration", "hall_name", "is_trial" },
            values: new object[,]
            {
                { 1, 1, 1, new DateTime(2026, 11, 5, 10, 0, 0), new TimeSpan(1, 0, 0), "Зал A", true },
                { 2, 2, 1, new DateTime(2026, 11, 7, 12, 0, 0), new TimeSpan(1, 0, 0), "Зал A", false },
                { 3, 3, 1, new DateTime(2026, 11, 9, 14, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 4, 4, 3, new DateTime(2026, 11, 10, 11, 0, 0), new TimeSpan(1, 0, 0), "Зал A", false },
                { 5, 5, 3, new DateTime(2026, 11, 11, 15, 0, 0), new TimeSpan(1, 0, 0), "Зал C", false },
                { 6, 6, 3, new DateTime(2026, 11, 12, 17, 0, 0), new TimeSpan(1, 0, 0), "Зал A", true },
                { 7, 7, 3, new DateTime(2026, 10, 15, 12, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 8, 8, 4, new DateTime(2026, 11, 14, 9, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 9, 9, 4, new DateTime(2026, 11, 16, 13, 0, 0), new TimeSpan(1, 0, 0), "Зал A", false },
                { 10, 10, 4, new DateTime(2026, 11, 18, 16, 0, 0), new TimeSpan(1, 0, 0), "Зал C", true },
                { 11, 1, 6, new DateTime(2026, 11, 20, 10, 0, 0), new TimeSpan(1, 0, 0), "Зал C", false },
                { 12, 2, 6, new DateTime(2026, 11, 21, 14, 0, 0), new TimeSpan(1, 0, 0), "Зал A", false },
                { 13, 3, 8, new DateTime(2026, 11, 22, 11, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 14, 4, 8, new DateTime(2026, 12, 5, 15, 0, 0), new TimeSpan(1, 0, 0), "Зал A", false },
                { 15, 5, 5, new DateTime(2026, 11, 6, 10, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 16, 6, 5, new DateTime(2026, 11, 8, 12, 0, 0), new TimeSpan(1, 0, 0), "Зал C", false },
                { 17, 7, 5, new DateTime(2026, 11, 13, 14, 0, 0), new TimeSpan(1, 0, 0), "Зал B", true },
                { 18, 8, 5, new DateTime(2026, 11, 17, 16, 0, 0), new TimeSpan(1, 0, 0), "Зал C", false },
                { 19, 9, 5, new DateTime(2026, 11, 19, 18, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 20, 10, 9, new DateTime(2026, 11, 9, 10, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 21, 1, 9, new DateTime(2026, 11, 15, 12, 0, 0), new TimeSpan(1, 0, 0), "Зал C", false },
                { 22, 2, 9, new DateTime(2026, 11, 21, 14, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 23, 3, 2, new DateTime(2026, 11, 10, 15, 0, 0), new TimeSpan(1, 0, 0), "Зал C", true },
                { 24, 4, 7, new DateTime(2026, 11, 11, 11, 0, 0), new TimeSpan(1, 0, 0), "Зал C", false },
                { 25, 5, 7, new DateTime(2026, 11, 18, 13, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false },
                { 26, 6, 10, new DateTime(2026, 11, 20, 17, 0, 0), new TimeSpan(1, 0, 0), "Зал B", false }
            });
    }

    /// <summary>Удаляет начальные данные</summary>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "training_sessions",
            keyColumn: "id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 });

        migrationBuilder.DeleteData(
            table: "trainers",
            keyColumn: "id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

        migrationBuilder.DeleteData(
            table: "members",
            keyColumn: "id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

        migrationBuilder.DeleteData(
            table: "specializations",
            keyColumn: "id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
    }
}