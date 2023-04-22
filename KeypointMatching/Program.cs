using KeypointMatching;
using KeypointMatching.Infrastructure.Interfaces;
using KeypointMatching.Infrastructure.Realizations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<ICVService, CVService>();

var a = DataDesriptorsHelper.Persons;

var app = builder.Build();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=KeypointMatching}/{action=Index}");

app.Run();
