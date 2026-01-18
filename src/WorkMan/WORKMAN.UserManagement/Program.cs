using BuildingBlocks.Common.DependencyInjection;
using WORKMAN.UserManagement.Feature.Users;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DbContext
builder.Services.AddDbContext<UserManagementDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Event Infrastructure
builder.Services.AddEventInfrastructure();

// Register Event Handlers
builder.Services.AddTransient<IEventHandler<UserRegisteredEvent>, UserRegisteredEventHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
