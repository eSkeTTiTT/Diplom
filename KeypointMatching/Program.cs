using Diplom.DAL;
using KeypointMatching.Infrastructure.Interfaces;
using KeypointMatching.Infrastructure.Realizations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<ICVService, CVService>();
builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=KeypointMatching}/{action=Index}");

app.Run();
