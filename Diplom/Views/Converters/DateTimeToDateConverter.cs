using System.Globalization;

namespace Diplom.Views.Converters
{
	public class DateTimeToDateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
			((DateTime)value)
				.ToString("dd.MM.yyyy");

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
			throw new NotImplementedException();
	}
}
