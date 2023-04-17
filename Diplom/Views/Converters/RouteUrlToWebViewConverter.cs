using System.Globalization;

namespace Diplom.Views.Converters;

public class RouteUrlToWebViewConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var url = (string)value;

		return new UrlWebViewSource
		{
			Url = url,
		};
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}

