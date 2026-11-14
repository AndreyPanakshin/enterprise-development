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

builder.Services.AddSingleton<IRepository<Member, int>, MemberRepository>();
builder.Services.AddSingleton<IRepository<Trainer, int>, TrainerRepository>();
builder.Services.AddSingleton<IRepository<Specialization, int>, SpecializationRepository>();
builder.Services.AddSingleton<IRepository<TrainingSession, int>, TrainingSessionRepository>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();