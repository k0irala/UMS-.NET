using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UMS.Data;
using UMS.Models.Designation;
using UMS.Repositories;
using UMS.Validations.Deisgnation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDapperRepository, DapperRepository>();
builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();    
builder.Services.AddScoped<DesignationRepository>();
builder.Services.AddScoped<IValidator<AddDesignationModel>, AddDesignationValidator>();
builder.Services.AddScoped<IValidator<int>, DeleteDesignationValidator>();
builder.Services.AddScoped<IValidator<UpdateDesignationModel>, UpdateDesignationValidator>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        
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
