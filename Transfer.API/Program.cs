using Application.Interfaces;
using Application.Mappers;
using Application.Mappers.IMappers;
using Application.UseCases;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<TransferContext>(option => option.UseSqlServer(connectionString));


builder.Services.AddScoped<ITransferServices, TransferServices>();
builder.Services.AddScoped<ITransferQuery, TransferQuery>();
builder.Services.AddScoped<ITransferCommand, TransferCommand>();
builder.Services.AddScoped<ITransferMapper, TransferMapper>();



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
