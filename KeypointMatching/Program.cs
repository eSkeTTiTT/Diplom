using Diplom.DAL;
using Diplom.DOMAIN.NewModels;
using KeypointMatching.Infrastructure.Interfaces;
using KeypointMatching.Infrastructure.Realizations;
using KeypointMatching.Recommender.Interfaces;
using KeypointMatching.Recommender.Systems;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<ICVService, CVService>();
builder.Services.AddTransient<IRecommender, ICFSystem>();
builder.Services.AddTransient<IRecommender, UCFSystem>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddDbContext<DiplomDBContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=KeypointMatching}/{action=Index}");

app.Run();
