using Aidelythe.Api._System.Localization;
using Aidelythe.Api._System.Orchestration;
using Aidelythe.Api._System.Validation;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddValidation();
services.AddMediator();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.UseLocalization();
app.Run();