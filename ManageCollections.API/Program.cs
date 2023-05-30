using Infrastructure;
using ManageCollections.API;
using ManageCollections.API.GlobalException;
using ManageCollections.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                   .Enrich.FromLogContext()
                   .ReadFrom.Configuration(builder.Configuration)
                   .CreateLogger();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleware();

app.UseAuthorization();


app.MapControllers();

app.Run();