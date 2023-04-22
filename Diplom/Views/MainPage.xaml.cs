using Diplom.DAL;
using Diplom.Services.Realizations;
using Microsoft.Extensions.Configuration;

namespace Diplom.Views;

public partial class MainPage : ContentPage
{
	public MainPage(ApplicationDbContext context)
	{
		InitializeComponent();
	}
}

