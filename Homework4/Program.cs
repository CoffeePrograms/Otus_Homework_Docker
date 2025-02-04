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

// ��������� Autofac ��� ��������� �����
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        // ������������ DbContext
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        containerBuilder.Register(c =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new DatabaseContext(optionsBuilder.Options);
        })
        .As<DbContext>()
        .InstancePerLifetimeScope();

        // ������������ ������� � �����������
        containerBuilder.RegisterType<WeatherForecastService>().As<IService<WeatherForecast>>();
        containerBuilder.RegisterType<EfRepository<WeatherForecast>>().As<IRepository<WeatherForecast>>();
    });

//// ������������ DbContext
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<DatabaseContext>(opt =>
//    opt.UseNpgsql(connectionString));
//builder.Services.AddScoped<DbContext>(p => p.GetRequiredService<DatabaseContext>());

builder.Services.AddControllers();

// ��������� Swagger
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

// ��������� ������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // ��������� ������� �� ������ ���������
              .AllowAnyMethod()  // ��������� ��� HTTP-������ (GET, POST, PUT � �.�.)
              .AllowAnyHeader(); // ��������� ��� ���������
    });
});

var app = builder.Build();

// ���������� �������� CORS
app.UseCors("AllowAllOrigins");

// ������������ ��������� HTTP ��������
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
