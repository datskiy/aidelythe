using Aidelythe.Api._System.Localization;
using Aidelythe.Api._System.Orchestration;
using Aidelythe.Api._System.Validation;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddValidation();
services.AddMediator();
services.AddOpenApi();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseLocalization();
app.MapControllers();
app.MapOpenApi();
app.MapScalarApiReference();
app.Run();