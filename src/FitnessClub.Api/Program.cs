using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Members;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Services;
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using FitnessClub.Repositories.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DataSeed>();

builder.Services.AddSingleton<IMemberService, MemberService>();
builder.Services.AddSingleton<ITrainerService, TrainerService>();
builder.Services.AddSingleton<ISpecializationService, SpecializationService>();
builder.Services.AddSingleton<
    IApplicationService<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int>,
    PersonalTrainingSessionService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

builder.Services.AddSingleton<IRepository<Member, int>, ClientRepository>();
builder.Services.AddSingleton<IRepository<Trainer, int>, TrainerRepository>();
builder.Services.AddSingleton<IRepository<Specialization, int>, SpecializationRepository>();
builder.Services.AddSingleton<IRepository<TrainingSession, int>, TrainingSessionRepository>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<DataSeed>();
    
    var memberRepo = scope.ServiceProvider.GetRequiredService<IRepository<Member, int>>();
    var trainerRepo = scope.ServiceProvider.GetRequiredService<IRepository<Trainer, int>>();
    var specRepo = scope.ServiceProvider.GetRequiredService<IRepository<Specialization, int>>();
    var sessionRepo = scope.ServiceProvider.GetRequiredService<IRepository<TrainingSession, int>>();

    foreach (var item in seed.Specializations) specRepo.Add(item);
    foreach (var item in seed.Members) memberRepo.Add(item);
    foreach (var item in seed.Trainers) trainerRepo.Add(item);
    foreach (var item in seed.TrainingSessions) sessionRepo.Add(item);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();