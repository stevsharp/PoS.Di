using SimpleDIManager.DI;
using SimpleDIManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new SimpleContainerServiceProviderFactory());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Host.ConfigureContainer<ISimpleDIContainer>(c =>
{
    c.Register<SimpleDIManager.Services.ILogger, ConsoleLogger>(Lifetime.Singleton);
    c.Register<IService, Service>(Lifetime.Transient);
});

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
