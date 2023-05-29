using Diplom.Helpers;
using Diplom.ViewModels.Base;

namespace Diplom.ViewModels;

public class ArticleViewModel : BaseViewModel
{
	private string _imageSource;
	public string ImageSource
	{
		get => _imageSource;
		set => SetProperty(ref _imageSource, value);
	}

	private string _firstTextPerson;
	public string FirstTextPerson
	{
		get => _firstTextPerson;
		set => SetProperty(ref _firstTextPerson, value);
	}

	private string _secondTextPerson;
	public string SecondTextPerson
	{
		get => _secondTextPerson;
		set => SetProperty(ref _secondTextPerson, value);
	}

	public ArticleViewModel(int personId)
	{
		Task.Run(() => Init(personId)).Wait();
	}

	private async Task Init(int id)
	{
		var http = new HttpClient();

		var photoResponse = await http.GetAsync(string.Format(DbHelper.URL_GetImageById, id));
		var textResponse = await http.GetAsync(string.Format(DbHelper.URL_GetTextById, id));

		var str = await textResponse.Content.ReadAsStringAsync();
		FirstTextPerson = str[..(str.Length / 2)];
		SecondTextPerson = str[(str.Length / 2)..];
		ImageSource = await photoResponse.Content.ReadAsStringAsync();
	}
}
