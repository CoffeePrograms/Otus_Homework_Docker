using Autofac;
using Autofac.Extensions.DependencyInjection;
using Homework4.Database;
using Homework4.Models;
using Homework4.Repositories;
using Homework4.Repositories.Interfaces;
using Homework4.Services;
using Homework4.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем Autofac как поставщик услуг
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        // Регистрируем DbContext
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        containerBuilder.Register(c =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new DatabaseContext(optionsBuilder.Options);
        })
        .As<DbContext>()
        .InstancePerLifetimeScope();

        // Регистрируем сервисы и репозитории
        containerBuilder.RegisterType<WeatherForecastService>().As<IService<WeatherForecast>>();
        containerBuilder.RegisterType<EfRepository<WeatherForecast>>().As<IRepository<WeatherForecast>>();
    });

//// Регистрируем DbContext
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<DatabaseContext>(opt =>
//    opt.UseNpgsql(connectionString));
//builder.Services.AddScoped<DbContext>(p => p.GetRequiredService<DatabaseContext>());

builder.Services.AddControllers();

// Настройка Swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Homework",
        Description = "Otus. Homework. Simple API"
    });
});

// Добавляем сервисы CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Разрешить запросы от любого источника
              .AllowAnyMethod()  // Разрешить все HTTP-методы (GET, POST, PUT и т.д.)
              .AllowAnyHeader(); // Разрешить все заголовки
    });
});

var app = builder.Build();

// Используем политику CORS
app.UseCors("AllowAllOrigins");

// Конфигурация конвейера HTTP запросов
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
