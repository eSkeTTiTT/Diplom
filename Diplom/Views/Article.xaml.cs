namespace Diplom.Views;

public partial class Article : ContentPage
{
	public Article()
	{
		InitializeComponent();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}