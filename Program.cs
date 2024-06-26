using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Application;
using OrderManagementAPI.Application.Contracts;
using OrderManagementAPI.Domain.Services;
using OrderManagementAPI.Domain.Services.Contracts;
using OrderManagementAPI.Infrastructure.DAO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var allOrigins = "MyAllowSpecificOrigins";

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allOrigins,
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration["DbSetting:ConnectionString"]));

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderDomain, OrderDomain>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

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

await app.RunAsync();
