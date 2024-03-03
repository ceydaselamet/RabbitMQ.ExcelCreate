using FileCreateWorkerService;
using FileCreateWorkerService.Models;
using FileCreateWorkerService.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(sp => new ConnectionFactory()
{

	Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")),
	DispatchConsumersAsync = true
});

builder.Services.AddDbContext<AdventureWorksDw2016Context>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
