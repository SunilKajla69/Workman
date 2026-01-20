using WORKMAN.Config.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigApplicationServices(builder.Configuration);
var app = builder.Build();

app.UseConfigApi();

app.Run();
