var builder = DistributedApplication.CreateBuilder(args);

var mysqlPassword = builder.AddParameter("mysql-password", secret: true);

var database = builder.AddMySql("mysql", password: mysqlPassword)
    .WithImageTag("8.4")
    .WithDataVolume()
    .AddDatabase("fitnessclub");

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();