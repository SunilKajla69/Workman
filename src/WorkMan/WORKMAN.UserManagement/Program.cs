using WORKMAN.UserManagement.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add all UserManagement services (includes JWT authentication)
builder.Services.AddUserManagementServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomMiddlewares(); 

app.Run();
